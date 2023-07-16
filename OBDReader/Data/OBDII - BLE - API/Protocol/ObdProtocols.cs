namespace OBDReader.Data;

// public static class ObdProtocols
// {
//     public static Dictionary<string, char> Protocols = new()
//     {
//         { "AUTO", '0' },
//         { "SAE_J1850_PWM", '1' },
//         { "SAE_J1850_VPW", '2' },
//         { "ISO_9141_2", '3' },
//         { "ISO_14230_4_KWP", '4' },
//         { "ISO_14230_4_KWP_FAST", '5' },
//         { "ISO_15765_4_CAN", '6' },
//         { "ISO_15765_4_CAN_B", '7' },
//         { "ISO_15765_4_CAN_C", '8' },
//         { "ISO_15765_4_CAN_D", '9' },
//         { "SAE_J1939_CAN", 'A' },
//         { "USER1_CAN", 'B' },
//         { "USER2_CAN", 'C' },
//     };
// }

public enum ObdProtocols
{
    AUTO = '0',

    SAE_J1850_PWM = '1',

    SAE_J1850_VPW = '2',

    ISO_9141_2 = '3',

    ISO_14230_4_KWP = '4',

    ISO_14230_4_KWP_FAST = '5',

    ISO_15765_4_CAN = '6',

    ISO_15765_4_CAN_B = '7',

    ISO_15765_4_CAN_C = '8',

    ISO_15765_4_CAN_D = '9',

    SAE_J1939_CAN = 'A',

    USER1_CAN = 'B',

    USER2_CAN = 'C'
}