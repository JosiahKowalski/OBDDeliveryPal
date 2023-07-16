namespace OBDReader.Data;

public class LineFeedOffCommand : ObdProtocolCommand
{
    public LineFeedOffCommand() : base("AT L0") { }

    public override string GetFormattedResult()
    {
        // Implement the logic here to return the formatted result.
        return Command;
    }
}