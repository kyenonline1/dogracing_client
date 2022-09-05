
namespace Client.Photon
{
    internal enum GpType : byte
    {
        Unknown = (byte)0,
        Null = (byte)42,
        Dictionary = (byte)68,
        StringArray = (byte)97,
        Byte = (byte)98,
        Custom = (byte)99,
        Double = (byte)100,
        EventData = (byte)101,
        Float = (byte)102,
        Hashtable = (byte)104,
        Integer = (byte)105,
        Short = (byte)107,
        Long = (byte)108,
        IntegerArray = (byte)110,
        Boolean = (byte)111,
        OperationResponse = (byte)112,
        OperationRequest = (byte)113,
        String = (byte)115,
        ByteArray = (byte)120,
        Array = (byte)121,
        ObjectArray = (byte)122,
    }
}