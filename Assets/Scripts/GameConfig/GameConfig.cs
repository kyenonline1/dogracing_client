using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameconfig
{
    public class ClientGameConfig
    {
        public static string Vesion = "V1.0.1";
        public static string AssetBundler;
        public static string UrlSaveUserInfo;
        public static int BlindMission;
        public static int TotalSpin;
        public static string productIDiOSRemoveAds;
        public static string LinkStoreiOS;
        public static string videoAdsID;
        public static string interestialID;
        public static string gameIDAds;
        public static bool isAutoReconnect;

        private static int curLanguage = 0;

        public class ConstantValue
        {
            public const int NUM_THREAD_DATA = 5;
        }

        public class RequestCode
        {
            public static readonly string ATH = "ATH";
            public static readonly string ACC = "ACC";

            public static readonly string CRT = "CRT";
            public static readonly string PNG = "PNG";
            public static readonly string UDT = "UDT";
            public static readonly string LGI = "LGI";
            public static readonly string LOB = "LOB";
            public static readonly string MSG = "MSG";
            public static readonly string CMN = "CMN";
            public static readonly string SBI = "SBI";
            public static readonly string COT = "COT";
            public static readonly string TRY = "TRY";
            public static readonly string GME = "GME";
            public static readonly string FRI = "FRI";

            public static readonly string PKR = "PKR";
            public static readonly string SBI_PKR = "SBI_PKR";
            public static readonly string DOG = "DOG";

        }
        public class GAMEID
        {
            public static string CURRENT_GAME_ID = "POKER";

            public static readonly string TAMQUOC = "TAM_QUOC";
            public static readonly string MANHTHU = "MANH_THU";
            public static readonly string HAITAC = "HAI_TAC";
            public static readonly string MINIPOKER = "MINI_POKER";
            public static readonly string SLOT3 = "SLOT3";
            public static readonly string TAIXIU = "MINI_TAIXIU";
            public static readonly string CAOTHAP = "CAO_THAP";
            public static readonly string POKER = "POKER";
            public static readonly string MAU_BINH = "MAU_BINH";
            public static readonly string XITO = "XI_TO";
            public static readonly string TLMNSL = "TLMNSL";
            public static readonly string TLMN = "TLMN";
            public static readonly string PHOM = "PHOM";
            public static readonly string LIENG = "LIENG";
            public static readonly string DOG_RACING = "DOG";
        }


        public static int Language
        {
            get
            {
                return PlayerPrefs.GetInt("Language", -1);
            }
            set
            {
                curLanguage = value;
                PlayerPrefs.SetInt("Language", curLanguage);
                PlayerPrefs.Save();
            }
        }

        public static bool IsHavingRemoveAd
        {
            get
            {

                return PlayerPrefs.GetInt("IsHaveRemoveAd", 0) == 1;

            }

            set
            {
                int param = value == true ? 1 : 0;

                PlayerPrefs.SetInt("IsHaveRemoveAd", param);
                PlayerPrefs.Save();
            }
        }


        public class LOBBY_TYPE
        {
            public enum LobbyType
            {
                MAIN,
                LOBBY,
                SPINUP,
                TOURNAMENT
            }

            public enum LobbySpinTour
            {
                TOUR = -1,
                SPIN,
                MTT,
                NONE
            }
            public static LobbyType Lobby_type = LobbyType.MAIN;

            private static long mincashin;

            public static long MinCashIn
            {
                get
                {
                    return mincashin;
                }
                set
                {
                    //Debug.Log("Set MinCashIn: " + value);
                    mincashin = value;
                }
            }

            public static LobbySpinTour lobbySpinTour = LOBBY_TYPE.LobbySpinTour.NONE;

            //public static LobbyType Curent_Lobby_type = LobbyType.MAIN;
        }

        public class PokerTableInfo
        {
            public static int Maxplayer;
            public static int Blind;

            public static int TourId;
            /// <summary>
            /// -1: Normal,  0: Spin, 1: Tour,
            /// </summary>
            public static int PokerType;

            public static void ClearPokerTableInfo()
            {
                Maxplayer = 0;
                Blind = 0;
                PokerType = -2;
                TourId = -1;
            }
        }

        public class PokerHistoryInfo
        {
            public static long TableId;
            public static long Gamesession;
            public static string GameId;
        }

        public class CHANEL_TABLE
        {
            public enum ChanleTable
            {
                DANTHUONG,
                DANCHOI,
                VIP
            }
            public static ChanleTable chanleTable = ChanleTable.DANTHUONG;
        }

        public class ANNOUCEMENT_TYPE
        {
            public enum Annoucement_Type
            {
                PERSONAL,
                ANNOUNCEMENT,

            }
            public static Annoucement_Type Annoucement_type = Annoucement_Type.ANNOUNCEMENT;
            public static int countTotal, countSystem, countAnn;
        }

        public class EXCHANGE_TYPE
        {
            public enum View_Type
            {
                CARD,
                HISTORY
            }
            public static View_Type view_type = View_Type.CARD;

            public static float rateMomo;
        }

        public class PAY_TYPE
        {
            public enum Pay_Type
            {
                GOOGLE,
                APPLE,
                CARD,
                NONE,
            }
            public static Pay_Type pay_type = Pay_Type.NONE;
        }
        public class KEY_DATADISPATCHER
        {
            public static readonly string SCENE_LOBBY = "LobbyScene";
            public static readonly string KEY_TABLEID = "TableId";
            public static readonly string KEY_GAMEID = "GameId";


        }

        public class CASH_TYPE
        {
            public enum CASH
            {
                NONE,
                GOLD,
                SILVER,
                FLASH,
            }
            public static CASH CASHTYPE = CASH.GOLD;
        }

        public class APPFUNCTION
        {
            public static bool IsAppFullFunction = false;
            public static bool IsAppCharging = false;
            public static bool verifyFromClient = false;
            public static string UrlFanpage = "https://www.facebook.com/";
            public static string UrlMessenger = "https://m.me/123132132132";
            public static string UrlTelegram = "https://t.me/123132132132";
            public static string UrlTelegramBOT = "https://t.me/telebot";
            public static string Hotline = "https://t.me/telebot";
        }
        public static string GetTableName(byte bytetbn, string gameid)
        {
            string tlbname = "Biết chơi thì vào";
            string[] tablenames = new string[0];
            switch (gameid)
            {
                case "POKER":
                    tablenames = new string[]
                    {
                    "All in !!!!",
                    "Biết chơi thì vào",
                    "Call hay fold ?",
                    "Check hay raise ?",
                    "Có tiền nhào vô",
                    "Dealer xinh đẹp",
                    "Master Quí Nguyễn"
                    };
                    break;
                case "LIENG":
                    tablenames = new string[]
                    {
                    "Bao nhiêu cũng chơi",
                    "Có tiền thì vào",
                    "Liêng rồi !!!",
                    "Tổng 9 hehe",
                    "Sáp rồi !!!",
                    };
                    break;
                case "XITO":
                    tablenames = new string[]
                   {
                    "Biết chơi thì vào",
                    "Có tiền thì chơi",
                    "Thùng phá sảnh rồi",
                    "Tứ quý rồi",
                    "Vua xì tố",
                   };
                    break;
                case "MAU_BINH":
                    tablenames = new string[]
                  {
                    "Ba cái thùng",
                    "Ba cái sảnh",
                    "Cù lũ chi 2",
                    "Nhiều tiền thì vào",
                    "Lục phé bôn",
                    "Sảnh rồng nè",
                    "Sám chi 3",
                  };
                    break;
                case "TLMNSL":
                case "TLMN":
                    tablenames = new string[]
                 {
                    "Bốn đôi thông nè",
                    "Đừng để thối 2",
                    "Nhiều tiền thì vào",
                    "Thắng trắng luôn",
                    "Sảnh rồng nè",
                    "Vui là chính",
                 };
                    break;
                case "XOC_DIA":
                    tablenames = new string[]
                 {
                    "Chẵn Lẻ",
                    "Nhà cái cân tất",
                    "Không chẵn thì lẻ :)",
                    "Xóc Xóc Xóc",
                 };
                    break;
                case "SAM":
                    tablenames = new string[]
                 {
                    "Bài đẹp báo sâm",
                    "Biết chơi thì vô",
                    "Chuyên gia bắt sâm",
                    "Đừng để thối 2",
                    "Tứ quý nè",
                 };
                    break;
                case "PHOM":
                    tablenames = new string[]
                 {
                    "Biết chơi thì vào",
                    "Có tiền nhào vô",
                    "Mình xin cây chốt",
                    "Móm nhăn răng",
                    "Vào cái Ù luôn",
                    "2 phỏm chờ Ù",
                 };
                    break;
            }
            if (bytetbn < tablenames.Length)
            {
                tlbname = tablenames[bytetbn];
            }
            return tlbname;
        }
        public static byte GetByteTableName(string gameid)
        {
            byte tbname = 0;
            switch (gameid)
            {
                case "POKER":
                    tbname = (byte)Random.Range(0, 7);

                    break;
                case "LIENG":
                    tbname = (byte)Random.Range(0, 5);

                    break;
                case "XITO":
                    tbname = (byte)Random.Range(0, 5);
                    break;
                case "MAU_BINH":
                    tbname = (byte)Random.Range(0, 7);
                    break;
                case "TLMNSL":
                case "TLMN":
                    tbname = (byte)Random.Range(0, 6);
                    break;
                case "XOC_DIA":
                    tbname = (byte)Random.Range(0, 4);
                    break;
                case "SAM":
                    tbname = (byte)Random.Range(0, 5);
                    break;
                case "PHOM":
                    tbname = (byte)Random.Range(0, 6);
                    break;
            }
            return tbname;
        }

        public class BlindWheel{
            public static bool ISFREE_SLOTFULL;
            public static int BLIND_MNPOKER;
            public static int BLIND_MN_SLOT3;
            public static int BLIND_SLOTFULL;
            public static int BLIND_TAMQUOC;
            public static int BLIND_HAITAC;
            public static int BLIND_MANHTHU;
        }

        public class FreeSpinWheel
        {
            public static int FREESPIN_MNPOKER;
            public static int FREESPIN_MN_SLOT3;
            public static int FREESPIN_SLOTFULL;
            public static int FREESPIN_TAMQUOC;
            public static int FREESPIN_HAITAC;
            public static int FREESPIN_MANHTHU;
        }
    }

}
