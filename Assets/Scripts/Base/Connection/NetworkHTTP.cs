using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using AppConfig;
using System;

public class NetworkHTTP : MonoBehaviour {
#if SERVER_DEV
			 public static readonly string Domain ="http://35.220.249.57:80/";//
#else
    public static readonly string Domain = "http://35.220.249.57:13579/";//
    //public static readonly string Domain = "http://35.241.68.157:80/";
#endif



    public static class KEYNAME_INGAME
    {
        public static readonly string INGAME_VINHDANH = "games/top_game/top?gameid={0}&blind={1}";
        public static readonly string LOBBY_READINGINBOX = "lobby/read_mail?id={0}";
        public static readonly string LOBBY_COUNTINBOX = "lobby/count_newann?userid={0}";
        public static readonly string LOBBY_CHARGINGHISTORY = "lobby/charging_histories?user_id={0}";
        public static readonly string INGAME_HISTORY_PK_LIE_XT = "games/histories/his_raise?gameid={0}&user_id={1}";
        public static readonly string INGAME_HISTORY_CARD = "games/histories/his_card?gameid={0}&user_id={1}";
        public static readonly string INGAME_HISTORY_XOC = "games/histories/his_xoc?gameid={0}&user_id={1}";
        public static readonly string INGAME_HISTORY_SLOT3 = "games/slot3/histories?id={0}";
        public static readonly string INGAME_HISTORY_SLOT5 = "games/slot5/histories?id={0}&gameid={1}";
    }


	public static NetworkHTTP Instance 
	{
		get;
		private set;
	}

    public  delegate void Handler(string data);

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
        DontDestroyOnLoad(gameObject);
	}

    /// <summary>
    /// Request the specified url, handler and TAG.
    /// </summary>
    /// <param name="url">URL</param>
    /// <param name="handler">Handler</param>
    /// <param name="TAG">Name use for debugging</param>
    public void Request(string url, Handler handler, string TAG = "")
	{
        StartCoroutine(RequestProcessor(url, handler, TAG));
	}


    private IEnumerator RequestProcessor(string url, Handler handler, string TAG = "")
	{
        Debug.LogFormat("{0} - REQUEST : {1}", TAG, url);
        WWW www = new WWW (url);
        yield return www;
        string error = www.error;
        if(string.IsNullOrEmpty(error)){
            Dictionary<string, string> responseheader = www.responseHeaders;
            Debug.Log("Response header keys: " + responseheader.Keys.ToString());
            foreach (string s in responseheader.Keys)
            {
                Debug.Log("Key: " + s + " - Data: " + responseheader[s]);
            }

            if (responseheader.ContainsKey("SET-COOKIE"))
            {
                //string[] cookie = responseheader["SET-COOKIE"].Split(';');
                //if(cookie.Length >1)
                //ClientConfig.UserInfo.COOKIE_HEADER = cookie[0] +";"+ cookie[1].Replace("Path=/,","");
            }
            string response = www.text;
            Debug.LogFormat ("{0} - RESPONSE : {1}", TAG, response);
            JSONNode data = JSONNode.Parse (response);
            if (handler != null)
            {
                handler(data);
            }
            else
            {
                Debug.LogErrorFormat("{0} - HANDLER WAS NULL", TAG);
            }
        }else{
            Debug.LogErrorFormat("{0} - REQUEST ERROR : {1}", TAG, error);
        }
	}
    /// <summary>
    /// Hàm xử lý Request WWWForm
    /// </summary>
    /// <param name="url"></param>
    /// <param name="handler"></param>
    /// <param name="TAG"></param>
    public void RequestWWForm(string url, Handler handler, string TAG = "", Action callbackerror = null)
    {
        StartCoroutine(RequestProcessorFrom(url, handler, TAG, callbackerror));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="handler"></param>
    /// <param name="TAG"></param>
    /// <returns></returns>
    private IEnumerator RequestProcessorFrom(string url, Handler handler, string TAG = "", Action callbackerror = null)
    {
		Debug.LogFormat("{0} - REQUEST : {1}", TAG, url);
        //Debug.LogError("Cookie : " + ClientConfig.UserInfo.COOKIE_HEADER);
        //Dictionary<string,string> headers = new Dictionary<string, string>();
        //headers.Add(KEYNAME_LOG_RES.COOKIE, ClientConfig.UserInfo.COOKIE_HEADER);
        //Debug.LogError("HEADER : " + headers[KEYNAME_LOG_RES.COOKIE]);
        WWW www = new WWW(url);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            try
            {
                Debug.LogFormat("{0} - RESPONSE : {1}", TAG, www.text);
                //JSONNode data = JSONNode.Parse(response);
                string data = www.text;// ConverStringToJson(www.text);
                if (handler != null)
                {
                    handler(data);
                }
                else
                {
                    Debug.LogErrorFormat("{0} - HANDLER WAS NULL", TAG);
                }
            }catch(Exception e)
            {
                Debug.LogError("Exception : " + e.StackTrace);
            }
        }
        else
        {
            //TODO: Show Popup Error
            if (callbackerror != null) callbackerror();
            Debug.LogFormat("{0} - Error : {1}", TAG, www.error);
        }
    }

    public string ConverStringToJson(string str)
    {
        string strConvert = str.Replace(@"\", "").Remove(0, 1);
        strConvert = strConvert.Remove(strConvert.Length - 1);
        return strConvert;
    }

    #region Processor
    private void SaveCookies(string data)
    {
        /*/
        cookie="2|1:0|10:1489678553|6:cookie|44:YzE3NjUxZWUyYjhmYjI4NDZmNGRkMzVkNDgyMjhhMDM=|452075c34f8c8a1f3c40a4c1e2a6d74bb35902dc7bf92e16b2bf89b05e23c52b"; expires=Sat, 15 Apr 2017 15:35:53 GMT; Path=/,fid="2|1:0|10:1489678553|3:fid|24:dTk4MjA1NTg0MDU4ODQ0MzY=|058bb0a9bf4a3d0b586df55b1859e1beacbe86d48a620ed2cc88cb503d2599b5"; expires=Sat, 15 Apr 2017 15:35:53 GMT; Path=/
        //*/
        if (data.Contains("cookie"))
        {
            int index = data.IndexOf("\"");
            int dataLenght = data.Length;
            data = data.Substring(index + 1, data.Length - index - 1);
            index = data.IndexOf("\"");
            data = data.Substring(0, index);

            //ClientConfig.UserInfo.COOKIE_HEADER = data;
            //LoggerUtils.Log("COOKIES : " + data);
        }
    }
    #endregion
}
