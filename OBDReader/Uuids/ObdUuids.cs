namespace OBDReader.Uuids;

public class ObdUuids
{
    public static Guid ObdServiceUuid = new Guid("0000ffe0-0000-1000-8000-00805f9b34fb"); //  00000000-0000-0000-0000-88c255b98e7d
    public static Guid ObdCharacteristicUuid = new Guid("0000ffe1-0000-1000-8000-00805f9b34fb");
    //public static Guid ObdCharacteristicWriteUuid = new Guid("0000ffe1-0000-1000-8000-00805f9b34fb");
    public static Guid[] ObdServiceUuids = new Guid[]
    {
        ObdServiceUuid // i think this is the one... test
    };
}