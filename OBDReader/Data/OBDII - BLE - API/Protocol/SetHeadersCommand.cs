namespace OBDReader.Data;

public class SetHeadersCommand : ObdProtocolCommand
{
    public SetHeadersCommand(string header) : base($"AT SH {header}")
    {
    }

    public override string GetFormattedResult()
    {
        // Implement the logic here to return the formatted result.
        return Command;
    }
}