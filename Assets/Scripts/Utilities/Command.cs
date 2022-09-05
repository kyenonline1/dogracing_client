using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitis.Command
{
    public class Command
    {
        public class ATH_COMMAND
        {
            public static readonly int ATH_0        = 1;
            public static readonly int ATH_2        = 2;
            public static readonly int ATH_3        = 3;
            public static readonly int ATH_1_PUSH   = 4;
            public static readonly int ATH_5        = 5;
            public static readonly int ATH_6        = 6;
            public static readonly int ATH_7        = 9;
            public static readonly int ATH_8        = 11;
            public static readonly int ATH_9        = 25;
        }

        public class UDT_COMMAND
        {
            public static readonly int UDT_0        = 7;
        }

        public class ANN_COMMAND
        {
            public static readonly int ANN_0        = 8;
            public static readonly int ANN_1        = 22;
            public static readonly int ANN_2        = 23;
        }

        public class ACC_COMAND
        {
            public static readonly int ACC_0        = 12;
            public static readonly int ACC_1        = 13;
            public static readonly int ACC_3 = 812;
            public static readonly int ACC_4 = 811;
        }

        public class PAY_COMAND
        {
            public static readonly int PAY_0        = 14;
            public static readonly int PAY_1        = 15;
            public static readonly int PAY_2        = 16;
            public static readonly int PAY_3        = 17;
            public static readonly int PAY_4        = 18;
            public static readonly int PAY_5        = 1110;
        }

        public class COU_COMMAND
        {
            public static readonly int COU_0        = 20;
            public static readonly int COU_1        = 21;
            public static readonly int COU_2        = 26;
        }

        public class TOP_COMMAND
        {
            public static readonly int TOP_0        = 19;
            public static readonly int TOP_1        = 24;
            public static readonly int TOP_2        = 27;
            public static readonly int TOP_3        = 28;
        }

        public class SBI_COMMAND
        {
            public static readonly int SBI_1        = 72;
            public static readonly int SBI_2        = 29;
            public static readonly int MAU_1        = 101;
            public static readonly int TLMB_1       = 301;
            public static readonly int TLMN_1       = 401;
        }

        public class PKR_COMMAND
        {
            public static readonly int PKR_0        = 30;
            public static readonly int PKR_0_PUSH   = 31;
            public static readonly int PKR_1        = 32;
            public static readonly int PKR_2        = 33;
            public static readonly int PKR_3        = 34;
            public static readonly int PKR_4        = 35;
            public static readonly int PKR_5        = 36;
            public static readonly int PKR_5_PUSH   = 37;
            public static readonly int PKR_6        = 38;
            public static readonly int PKR_6_PUSH   = 39;
            public static readonly int PKR_8        = 40;
            public static readonly int PKR_9        = 41;
            public static readonly int PKR_10       = 42;
            public static readonly int PKR_SBI_0    = 43;
            public static readonly int PKR_SBI_2    = 44;
            public static readonly int PKR_SBI_5    = 45;
            public static readonly int PKR_SBI_6    = 46;
            public static readonly int PKR_SBI_6_P  = 47;
            public static readonly int PKR_SBI_7    = 48;
            public static readonly int PKR_11       = 49;
        }

        public class LIE_COMMAND
        {
            public static readonly int LIE_0        = 52;
            public static readonly int LIE_0_PUSH   = 53;
            public static readonly int LIE_1        = 54;
            public static readonly int LIE_2        = 55;
            public static readonly int LIE_3        = 56;
            public static readonly int LIE_4        = 57;
            public static readonly int LIE_5        = 58;
            public static readonly int LIE_5_PUSH   = 59;
            public static readonly int LIE_6        = 60;
            public static readonly int LIE_6_PUSH   = 61;
            public static readonly int LIE_8        = 62;
            public static readonly int LIE_9        = 63;
            public static readonly int LIE_10       = 64;
            public static readonly int LIE_SBI_0    = 65;
            public static readonly int LIE_SBI_2    = 66;
            public static readonly int LIE_SBI_5    = 67;
            public static readonly int LIE_SBI_6    = 68;
            public static readonly int LIE_SBI_6_P  = 69;
            public static readonly int LIE_SBI_7    = 70;
            public static readonly int LIE_11       = 71;
        }

        public class XITO_COMMAND
        {
            public static readonly int XTO_0        = 75;
            public static readonly int XTO_0_PUSH   = 76;
            public static readonly int XTO_1        = 77;
            public static readonly int XTO_2        = 78;
            public static readonly int XTO_3        = 79;
            public static readonly int XTO_4        = 80;
            public static readonly int XTO_5        = 81;
            public static readonly int XTO_5_PUSH   = 82;
            public static readonly int XTO_6        = 83;
            public static readonly int XTO_6_PUSH   = 84;
            public static readonly int XTO_8        = 85;
            public static readonly int XTO_9        = 86;
            public static readonly int XTO_10       = 87;
            public static readonly int XTO_SBI_0    = 88;
            public static readonly int XTO_SBI_2    = 89;
            public static readonly int XTO_SBI_5    = 90;
            public static readonly int XTO_SBI_6    = 91;
            public static readonly int XTO_SBI_6_P  = 92;
            public static readonly int XTO_SBI_7    = 93;
            public static readonly int XTO_11       = 94;
            public static readonly int XTO_1_PUSH   = 95;
            public static readonly int XTO_12       = 96;
        }
        public class PHOM_COMMAN
        {
            public static readonly int PHO_0 = 700;
            public static readonly int PHO_1_PUSH = 701;
            public static readonly int PHO_2 = 702;
            public static readonly int PHO_3_PUSH = 703;
            public static readonly int PHO_4 = 704;
            public static readonly int PHO_5 = 705;
            public static readonly int PHO_6_PUSH = 706;
            public static readonly int PHO_7 = 707;
            public static readonly int PHO_8_PUSH = 708;
            public static readonly int PHO_9_PUSH = 709;
            public static readonly int PHO_10_PUSH = 710;
            public static readonly int PHO_11 = 711;
            public static readonly int PHO_12_PUSH = 712;
            public static readonly int PHO_13_PUSH = 713;
            public static readonly int PHO_14 = 714;
            public static readonly int PHO_15 = 715;
            public static readonly int PHO_16_PUSH = 716;
            public static readonly int PHO_17_PUSH = 717;
            public static readonly int PHO_18_PUSH = 718;
            public static readonly int PHO_19_PUSH = 719;
            public static readonly int PHO_20 = 720;
            public static readonly int PHO_21 = 721;
            public static readonly int PHO_22_PUSH = 722;
        }

        public class CHAT_COMMAND
        {
            public static readonly int MSG_0        = 50;
            public static readonly int MSG_0_push   = 51;
        }

        public class SLOT5_COMMAND
        {
            public static readonly int SLF_2        = 800;
            public static readonly int SLC_0        = 808;
            public static readonly int SLC_4        = 809;
            public static readonly int SLF_3        = 801;
            public static readonly int SLC_1        = 810;
        }

        public class MINIPOKER_COMMAND
        {
            public static readonly int MPK_2       = 826;
            public static readonly int MPK_3       = 828;
        }

        public class TAIXIU_COMMAND
        {
            public static readonly int MBS_0 = 836;
            public static readonly int MBS_1 = 837;
            public static readonly int MBS_2 = 838;
            public static readonly int MBS_3 = 839;
            public static readonly int MBS_4 = 840;
            public static readonly int MBS_5 = 843;

            public static readonly int MSG_1 = 841;
            public static readonly int MSG_2 = 842;
        }

        public class TIENCA_COMMAND
        {
            public static readonly int SLT_2 = 816;
        }
        public class HOAQUA_COMMAND
        {
            public static readonly int SLM_2 = 1816;
        }

        public class RACINGDOG_COMMAND
        {
            public static readonly int DOG_0 = 880;
            public static readonly int DOG_1 = 881;
            public static readonly int DOG_2 = 882;
            public static readonly int DOG_7 = 887;
            public static readonly int DOG_8 = 888;
            public static readonly int DOG_9 = 889;
            public static readonly int DOG_0_PUSH = 860;
            public static readonly int DOG_2_PUSH = 862;
            public static readonly int DOG_3_PUSH = 863;
            public static readonly int DOG_4_PUSH = 864;
            public static readonly int DOG_5_PUSH = 865;
            public static readonly int DOG_6_PUSH = 866;
            public static readonly int DOG_9_PUSH = 869;
        }

        public class DAILY_COMMAND
        {
            public static readonly int DIS_0 = 98;
            public static readonly int DIS_1 = 99;
        }

        public class OTP_COMAND
        {
            public static readonly int OTP_0 = 1000;
        }
    }
}
