namespace OBDReader.Data;

public class NonNumericResponseException : Exception
{
    public NonNumericResponseException(string message) : base("Error reading response: " + message) {
    }
}