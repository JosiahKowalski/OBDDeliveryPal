namespace OBDReader.Data;

public class SelectProtocolCommand : ObdProtocolCommand
{
    public SelectProtocolCommand(char protocol) : base($"AT SP {protocol}")
    {
    }

    public override string GetFormattedResult()
    {
        // Implement the logic here to return the formatted result.
        return Command;
    }
}