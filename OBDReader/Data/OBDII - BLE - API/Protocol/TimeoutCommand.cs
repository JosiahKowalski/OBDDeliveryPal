namespace OBDReader.Data;

public class TimeoutCommand : ObdProtocolCommand
{
    public TimeoutCommand(int timeout) : base($"AT ST {timeout & 0xFF:X}")
    {
    }

    public override string GetFormattedResult()
    {
        // Implement the logic here to return the formatted result.
        return Command;
    }
}