using UnityEngine;
#if UNITY_IOS
using System.Runtime.InteropServices;
#endif
public class Clipboard
{
    static IBoard _board;
    static IBoard board
    {
        get
        {
            if (_board == null)
            {
#if UNITY_EDITOR
                _board = new EditorBoard();
#elif UNITY_ANDROID
                _board = new AndroidBoard();
#elif UNITY_IOS
                _board = new IOSBoard ();
#endif
            }
            return _board;
        }
    }

    public static void SetText(string str)
    {
        board.SetText(str);
    }

    public static string GetText()
    {
        return board.GetText();
    }
}

interface IBoard
{
    void SetText(string str);
    string GetText();
}

public class EditorBoard : IBoard
{
    public void SetText(string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }

    public string GetText()
    {
        return GUIUtility.systemCopyBuffer;
    }
}

#if UNITY_IOS
public class IOSBoard : IBoard {
    [DllImport("__Internal")]
    static extern void SetText_ (string str);
    [DllImport("__Internal")]
    static extern string GetText_();

    public void SetText(string str){
        if (Application.platform != RuntimePlatform.OSXEditor) {
            SetText_ (str);
        }
    }

    public string GetText(){
        return GetText_();
    }
}
#endif

#if UNITY_ANDROID
public class AndroidBoard : IBoard
{

    AndroidJavaClass cb = new AndroidJavaClass("game.unity.plugin.GIPlugin");

    public void SetText(string text)
    {
        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject Activity = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            using (AndroidJavaClass plugin = new AndroidJavaClass("game.unity.plugin.GIPlugin"))
            {
                plugin.CallStatic("CopyToClipboard", Activity, text);
            }
        }
    }

    public string GetText()
    {
        return cb.CallStatic<string>("GetToClipboard");
    }
}
#endif

