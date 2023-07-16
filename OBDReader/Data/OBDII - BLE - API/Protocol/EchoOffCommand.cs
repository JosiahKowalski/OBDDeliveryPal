namespace OBDReader.Data;

public class EchoOffCommand : ObdProtocolCommand
{
    public EchoOffCommand() : base("AT E0") { }

    public override string GetFormattedResult()
    {
        // Implement logic here to return the formatted result
        // For this example, let's assume we return the command itself.
        return Command;
    }
}