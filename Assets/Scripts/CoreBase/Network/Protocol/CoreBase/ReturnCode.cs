namespace GameProtocol.Protocol
{
    public enum ReturnCode : short
    {
        OK = 0,
        ERROR = -1,
        TABLE_ERROR = 13,// chet ban
        CANCEL_CASHIN = 2,
        OUT_OF_MONEY = 5,
    }
}
