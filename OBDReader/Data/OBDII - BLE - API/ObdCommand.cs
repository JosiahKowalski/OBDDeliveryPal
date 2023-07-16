using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Debug = System.Diagnostics.Debug;

namespace OBDReader.Data;

public abstract class ObdCommand
{
    protected string Command { get; set; }
    protected bool WaitForResponse { get; set; }
    protected StringBuilder Response { get; set; } = new();
    protected string RawData { get; set; }
    protected List<int> IntegerResponse { get; set; } = new();
    public bool UseImperialUnits { get; set; } = false;
    
    private static readonly Regex WhitespacePattern = new Regex(@"\s");
    private static readonly Regex BusInitPattern = new Regex("(BUS INIT)|(BUSINIT)|(\\.)");
    private static readonly Regex SearchingPattern = new Regex("SEARCHING");
    private static Regex digitsLettersPattern = new Regex("([0-9A-F])+");

    protected ObdCommand(string command, bool waitForResponse)
    {
        Command = command;
        WaitForResponse = waitForResponse;
    }

    public abstract string GetFormattedResult();
    
    protected abstract void PerformCalculations();

    public virtual string GetName() => this.GetType().Name;

    protected virtual byte[] GetBytes() => Encoding.ASCII.GetBytes(Command + "\r");
    
    private TaskCompletionSource<bool> _tcs;

    
    public async Task Run(ICharacteristic characteristic)
    {
        //_tcs = new TaskCompletionSource<bool>();
        try
        {
            if (characteristic.CanWrite is false || (characteristic.CanUpdate is false && WaitForResponse))
                throw new InvalidOperationException("Cannot write to characteristic");
            await characteristic.WriteAsync(GetBytes());
            // test
            if (!WaitForResponse) return;
            _tcs = new TaskCompletionSource<bool>();
            characteristic.ValueUpdated += ObdCharacteristic_ValueUpdated;
            await characteristic.StartUpdatesAsync();
            await Task.WhenAny(_tcs.Task, Task.Delay(TimeSpan.FromSeconds(2)));
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw;
        }
    }
    
    private void ObdCharacteristic_ValueUpdated(object sender, CharacteristicUpdatedEventArgs e)
    {
        try
        {
            var characteristic = (ICharacteristic)sender;
            var result = characteristic.StringValue;
            // read string until >
            foreach (var c in result.TakeWhile(c => c != '>'))
            {
                Response.Append(c);
            }

            // if it doesn't contain > then it is not the end of response, wait for next update
            if (!result.Contains('>')) return;
            // if (!WaitForResponse)
            // {
            //     characteristic.ValueUpdated -= ObdCharacteristic_ValueUpdated;
            //     characteristic.StopUpdatesAsync();
            //     _tcs.SetResult(true);
            //     return;
            // } // done with task
            characteristic.ValueUpdated -= ObdCharacteristic_ValueUpdated;
            characteristic.StopUpdatesAsync();
            RawData = RemoveAll(SearchingPattern, Response.ToString());
            RawData = RemoveAll(WhitespacePattern, RawData);
            RawData = RemoveAll(BusInitPattern, RawData);
            
            if (!digitsLettersPattern.IsMatch(RawData)) {
                throw new NonNumericResponseException(RawData);
            }
            
            IntegerResponse.Clear();
            var begin = 0;
            const int len = 2;
            while (begin + len <= RawData.Length)
            {
                var hex = RawData.Substring(begin, len);
                IntegerResponse.Add(Convert.ToInt32(hex, 16));
                begin += len;
            }
            PerformCalculations();
            _tcs.SetResult(true);
            
            // done with task
            
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);
            throw;
        }
    }
    
    private static string RemoveAll(Regex pattern, string input)
    {
        return pattern.Replace(input, "");
    }
    

    // Add other methods shared by all commands
}