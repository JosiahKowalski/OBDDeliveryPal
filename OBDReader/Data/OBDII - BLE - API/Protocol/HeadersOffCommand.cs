namespace OBDReader.Data;

public class HeadersOffCommand : ObdProtocolCommand
{
    public HeadersOffCommand() : base("ATH0")
    {
    }

    public override string GetFormattedResult()
    {
        // Implement the logic here to return the formatted result.
        return Command;
    }
}