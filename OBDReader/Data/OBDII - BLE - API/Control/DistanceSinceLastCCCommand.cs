namespace OBDReader.Data;

public class DistanceSinceLastCcCommand : ObdCommand
{
    private int _km = 0;
        
    public DistanceSinceLastCcCommand() : base("01 31", true) { }

    protected override void PerformCalculations()
    {
        // Skip first two bytes [0] and [1] as they are the mode and PID.
        var first = IntegerResponse[2] * 256;
        var second = IntegerResponse[3];
        _km = first + second;
    }
    
    public override string GetFormattedResult()
    {
        return UseImperialUnits ? $"{GetMiles()} mi" : $"{_km} km";
    }

    private int GetMiles()
    {
        var miles = _km * 0.621371192;
        return (int) Math.Round(miles);
    }    
}