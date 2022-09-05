
namespace GameProtocol.Base
{
    public enum ParameterCode : byte
    {
        Flag = 0,
        Opcode,
        SenderId,
        ErrorCode,
        ErrorMsg,
        CodeRun
    }
    public enum RegisterTypes : byte
    {
        Announce,
        CashoutItem,
        CashoutHistory,
        ChatDetail,
        CateCharging,
        Package,
        GameConfig,
        TopGame,
        TopCate,
        TopEvent,
        EventEntity,
        TelcoDetail,
        ChargingHistories,
        Mission,
        LuckyWheel,
        DailyMission,
        SpinItem,
        FirstCharging,
        TableInfo,
        DogRacing,
        PlayerDog,
        DogRacingHistory,
        DogRacingHistoryByUser,
        Segment,
        DogSlot,
        DogChat,
    }
}
