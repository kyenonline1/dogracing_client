using System.Collections;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AppConfig;
using Languages;

namespace Utilites
{
    public class MoneyHelper
    {
        //public static readonly string TAG = "MONEYHELPER";
        public static readonly string UNIT_CASH = "";
        //private static readonly string PATTERN_US = "{0:#,###,###.##}";
        //private static readonly string PATTERN_VN = "{0:#.###.###,##}";
        private static readonly string PATTERN_FORMAT = "#,###.##";


        //https://msdn.microsoft.com/en-us/library/ee825488(v=cs.20).aspx
        private static readonly string CULTURE_VN = "vi-VN";
        private static readonly string CULTURE_US = "en-US";

        private static Dictionary<Languages.Languages, string[]> MONEY_CHARACTER = new Dictionary<Languages.Languages, string[]>() {
                                                            { Languages.Languages.vn, new string[] { "", "K", "M", "B" } },
                                                            { Languages.Languages.en, new string[] { "", "K", "M", "B" } } };

        //private static Dictionary<string, string[]> MONEY_CHARACTER = new Dictionary<string, string[]>() {
        //                                                    { ClientConfig.Language.VN, new string[] { "", " N", " Tr", " Ty" } },
        //                                                    { ClientConfig.Language.EN, new string[] { "", " K", " M", " B" } },
        //                                                    { ClientConfig.Language.TH, new string[] { "", " K", " M", " B" } },
        //                                                    { ClientConfig.Language.CN, new string[] { "", " K", " M", " B" } },
        //                                                    { ClientConfig.Language.ID, new string[] { "", " K", " M", " B" } },
        //                                                    { ClientConfig.Language.TW, new string[] { "", " K", " M", " B" } }};


        /// <summary>
        /// convert number to pattern #,###
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string FormatNumberAbsolute(long number)
        {
            //return String.Format(PATTERN_US, cash) + " " + UNIT_CASH;
            CultureInfo cul = CultureInfo.GetCultureInfo(GetCulture(Language.LANG));   // try with "en-US"
            string a = number.ToString(PATTERN_FORMAT, cul.NumberFormat);
            return a == null || a.Length == 0 ? "0" : a;
        }

        /// <summary>
        /// convert cash to pattern #,### Xu
        /// </summary>
        /// <param name="cash"></param>
        /// <returns></returns>
        public static string FormatAbsoluteWithoutUnit(long cash)
        {
            return FormatNumberAbsolute(cash);
        }

        /// <summary>
        /// convert cash to pattern #,### Xu
        /// </summary>
        /// <param name="cash"></param>
        /// <returns></returns>
        public static string FormatAbsolute(long cash)
        {
            return FormatNumberAbsolute(cash) + " " + UNIT_CASH;
        }

   

        /// <summary>
        /// conver cash to pattern #,###.##
        /// </summary>
        /// <param name="cash"></param>
        /// <returns></returns>
        private static string FormatFloat(float cash)
        {
            //do khi lam tron 0.005 se bi lam tron len 0.01 nen kiem tra truoc
            if ((Convert.ToInt64(cash * 1000) % 10) >= 5)
                cash = cash - 0.005f;

            //return String.Format(PATTERN_US, cash);
            CultureInfo cul = CultureInfo.GetCultureInfo(GetCulture(Language.LANG));   // try with "en-US"
            string a = cash.ToString(PATTERN_FORMAT, cul.NumberFormat);
            return a == null || a.Length == 0 ? "0" : a;
        }

        /// <summary>
        /// convert cash to pattern #,### [N, Tr, Ty...] Xu 
        /// </summary>
        /// <param name="cash"></param>
        /// <returns></returns>
        public static string FormatRelatively(long cash)
        {
            return FormatRelativelyWithoutUnit(cash);// + " " + UNIT_CASH;
        }
        

        /// <summary>
        /// convert cash to pattern #,### [N, Tr, Ty...]
        /// </summary>
        /// <param name="cash"></param>
        /// <returns></returns>
        public static string FormatRelativelyWithoutUnit(long cash)
        {
            if (cash < 1000)
                return cash + "";
            if (cash < 1000000)
                return FormatFloat((float)cash / 1000) + MONEY_CHARACTER[Language.LANG][1];
            if (cash < 1000000000)
                return FormatFloat((float)(cash / 1000) / 1000) + MONEY_CHARACTER[Language.LANG][2];
            return FormatFloat((float)(cash / 1000000) / 1000) + MONEY_CHARACTER[Language.LANG][3];
        }

        public static long FormatRelatively(string cash)
        {
            string CASH_CHAN = "";
            string CASH_LE = "";

            cash = cash.Replace(UNIT_CASH, "");
            for (int i = 0; i < cash.Length; i++)
            {
                if (cash[i].ToString() == "," || cash[i].ToString() == ".")
                {
                    CASH_CHAN = cash.Substring(0, i);
                    CASH_LE = cash.Substring(i + 1);
                    CASH_LE = CASH_LE.Replace(" ", "");

                }
            }
            cash = cash.Replace(",", "");
            cash = cash.Replace(".", "");
            cash = cash.Replace(" ", "");
            if (cash.EndsWith("N"))
            {
                if (CASH_CHAN != "" && CASH_LE != "")
                {
                    double pow = double.Parse((CASH_LE.Length - 1).ToString());
                    long a = long.Parse(Math.Pow(10, pow).ToString());
                    return (long.Parse(CASH_CHAN) * 1000) + (long.Parse(CASH_LE.Substring(0, CASH_LE.Length - 1)) * (1000 / a));
                }
                return long.Parse(cash.Substring(0, cash.Length - 1)) * 1000;
            }
            if (cash.EndsWith("Tr"))
            {
                if (CASH_CHAN != "" && CASH_LE != "")
                {
                    double pow = double.Parse((CASH_LE.Length - 2).ToString());
                    long a = long.Parse(Math.Pow(10, pow).ToString());
                    return (long.Parse(CASH_CHAN) * 1000000) + (long.Parse(CASH_LE.Substring(0, CASH_LE.Length - 2)) * (1000000 / a));
                }
                return long.Parse(cash.Substring(0, cash.Length - 2)) * 1000000;
            }
            if (cash.EndsWith("Ty"))
            {
                if (CASH_CHAN != "" && CASH_LE != "")
                {
                    double pow = double.Parse((CASH_LE.Length - 2).ToString());
                    long a = long.Parse(Math.Pow(10, pow).ToString());
                    return (long.Parse(CASH_CHAN) * 1000000000) + (long.Parse(CASH_LE.Substring(0, CASH_LE.Length - 2)) * (1000000000 / a));
                }
                return long.Parse(cash.Substring(0, cash.Length - 2)) * 1000000000;
            }
            return long.Parse(cash);
        }

        private static string GetCulture(Languages.Languages lang)
        {
            if (lang == Languages.Languages.vn)
            {
                return CULTURE_VN;
            }
            return CULTURE_US;
        }

        internal static string FormatNumberAbsolute(object money)
        {
            throw new NotImplementedException();
        }
    }
}
