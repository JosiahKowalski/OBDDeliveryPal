namespace OBDReader.Data;

public class ObdProtocolCommand : ObdCommand
{
    public ObdProtocolCommand(string command) : base(command, false) { }
    
    public override string GetFormattedResult()
    {
        return Command;
    }
    
    protected override void PerformCalculations()
    {
        // No calculations to perform
    }
}