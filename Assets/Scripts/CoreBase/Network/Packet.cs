using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using GameProtocol.Protocol;
using AppConfig;
using Utilities;
using GameProtocol.Base;

namespace Network
{

    /// <summary>
    /// Packet. Dữ liệu dạng byte[] trao đổi giữa client/server
    /// </summary>
    internal class Packet
	{
		//private static readonly string TAG = "Packet";
        /// <summary>
        /// Cờ đánh dấu gói tin chủ động - gói tin request - response
        /// </summary>
        internal static readonly byte FLAG_ACTIVE = 0x00;
        /// <summary>
        /// Cờ đánh dấu gói tin bị động - gói tin Server Push
        /// </summary>
        internal static readonly byte FLAG_PASSIVE = 0x01;
        /// <summary>
        /// Cờ đánh dấu gói tin Ping - chỉ dùng để kiểm tra kết nối
        /// </summary>
        internal static readonly byte FLAG_PING = 0x02;

		private static readonly Dictionary<string, string> CODE_RUN_EXTEND = new Dictionary<string, string>() { 
			};

        internal static string DictToString2 (Dictionary<byte, object> dict)
		{
//#if !UNITY_EDITOR
 //           return "Log is disable in EDITOR mode!";
//#else
            string coderun = string.Empty;
			try {
                coderun = (string)dict[(byte)ParameterCode.CodeRun];
                //LogMng.Log("PACKET", "coderun: " + coderun);
                string _clazzName = coderun.Substring(0, 3);
				if(coderun.Contains("_") && coderun != "UDT0_V2")
                    _clazzName = coderun.Substring(coderun.LastIndexOf('_')+1, 3);

                //LogMng.Log("PACKET", "_clazzName: " + _clazzName);

                if (CODE_RUN_EXTEND.ContainsKey(_clazzName)) {
                    coderun = coderun.Replace(_clazzName, CODE_RUN_EXTEND[_clazzName]);
                    _clazzName = CODE_RUN_EXTEND[_clazzName];
                }


                //LogMng.Log("PACKET", "coderun: " + coderun);

                if (coderun.Contains("SBI_TLN"))
                    _clazzName = "SBI";


                string _namespace = "GameProtocol.";
				string enumName = _namespace + _clazzName + "." + _clazzName + "_ParameterCode";
				//LogMng.Log("PACKET", "enumname: " + enumName + " --- " + dict.Count);
				var sb = new StringBuilder ("{");
                
				System.Type enumToCast = System.Type.GetType (enumName);

				foreach (var keyCode in dict.Keys) {
                    //LogMng.Log("PACKET", "FOREACH : KEY: " + keyCode);
					if (keyCode < 10) {
						sb.Append ((ParameterCode)keyCode);
					} else {
                        //LogMng.Log("PACKET", " Enum Parse: " + enumName + " , keyCode : " + keyCode + " ,enumToCast: "+ (enumToCast == null));
						sb.Append (Enum.Parse (enumToCast, keyCode.ToString ()));
                        //LogMng.Log("PACKET", "Parse Done");
					}
					sb.Append (": ");
					var value = dict [keyCode];

                    //LogMng.Log("PACKET", "check type: " + value.GetType());
                    
					if (value == null) {
						sb.Append ("null");
					} else if (value.GetType () == typeof(Dictionary<byte, object>)) {
						sb.Append (DictToString2 ((Dictionary<byte, object>)value));
					} else if (value.GetType () == typeof(string)) {
						sb.Append ("\"" + value + "\"");
					} else if (value.GetType ().IsArray && !IsBasicType (value.GetType ().GetElementType ())) {
						object[] array = (object[])value;
						sb.Append ("[");
						foreach (object obj in array) {
							sb.Append (obj.ToString ());
							sb.Append (", ");
						}
						//xoa dau "," o cuoi xau
						if (array.Length > 0 && sb.Length >= 3)
							sb.Remove (sb.Length - 2, 2);
						sb.Append ("]");
					} else if (value.GetType ().IsArray && IsBasicType (value.GetType ().GetElementType ())) {
						switch (System.Type.GetTypeCode (value.GetType ().GetElementType ())) {
						case TypeCode.Byte:
							sb.Append (ArrayToString<Byte> ((Byte[])value));
							break;
						case TypeCode.Int16:
							sb.Append (ArrayToString<Int16> ((Int16[])value));
							break;
						case TypeCode.Int32:
							sb.Append (ArrayToString<Int32> ((Int32[])value));
							break;
						case TypeCode.Int64:
							sb.Append (ArrayToString<Int64> ((Int64[])value));
							break;
						case TypeCode.Boolean:
							sb.Append (ArrayToString<Boolean> ((Boolean[])value));
							break;
						case TypeCode.UInt16:
							sb.Append (ArrayToString<UInt16> ((UInt16[])value));
							break;
						case TypeCode.UInt32:
							sb.Append (ArrayToString<UInt32> ((UInt32[])value));
							break;
						case TypeCode.UInt64:
							sb.Append (ArrayToString<UInt64> ((UInt64[])value));
							break;
						case TypeCode.Double:
							sb.Append (ArrayToString<Double> ((Double[])value));
							break;
						case TypeCode.Decimal:
							sb.Append (ArrayToString<Decimal> ((Decimal[])value));
							break;
						case TypeCode.String:
							sb.Append (ArrayToString<String> ((String[])value));
							break;
						default:
							sb.Append ("This is array of " + value.GetType ().GetElementType ());
							break;
						}
					} else {
						sb.Append (value);
					}

					sb.Append (", ");
				}

				//xoa dau "," o cuoi xau
				if (sb.Length >= 3)
					sb.Remove (sb.Length - 2, 2);

				sb.Append ("}");
				return sb.ToString ();
			} catch (Exception e) {
				//Logger.LogException (e);
				return "Log Packet " + coderun + " Fail: " + e.Message;
			}

//#endif
        }
		private static string ArrayToString<T> (T[] array)
		{
			try {
				var sb = new StringBuilder ("");

				sb.Append ("[");
				if (typeof(T) == typeof(string)) {
					foreach (T element in array) {
						sb.Append ("\"").Append (element).Append ("\"");
						sb.Append (", ");
					}
				} else {
					foreach (T element in array) {
						sb.Append (element);
						sb.Append (", ");
					}
				}

				//xoa dau "," o cuoi xau
				if (sb.Length >= 3)
					sb.Remove (sb.Length - 2, 2);
				sb.Append ("]");
				return sb.ToString ();
			} catch (Exception e) {
				LogMng.LogException (e);
				return e.Message;
			}
		}

		private static bool IsBasicType (System.Type type)
		{
			switch (System.Type.GetTypeCode (type)) {
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
			case TypeCode.Boolean:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
			case TypeCode.Double:
			case TypeCode.Decimal:
			case TypeCode.String:
				return true;
			}
			return false;
		}
        
	}
}