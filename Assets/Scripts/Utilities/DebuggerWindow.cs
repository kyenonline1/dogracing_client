using UnityEngine;
using System.Collections;

public class DebuggerWindow : MonoBehaviour
{
    public KeyCode OpenKey = KeyCode.Menu;
    public bool AutoShow = true;
    public bool ShowWindow = false;
    public GUIStyle TextStyle;
    private string Log;
    private string Trace;
    private Rect Position;
    private Rect CloseButton;
    private Rect ScrollView;
    private Rect RectView;
    private Vector2 ScrollPos;

    private void Awake()
    {
		DontDestroyOnLoad (gameObject);
        Application.logMessageReceived += HandleLog;
        Position = new Rect(Screen.width/2, Screen.height/2, Screen.width-10, Screen.height/2);
        CloseButton = new Rect(0, 0, Position.width/10, Position.height/10);
        ScrollView = new Rect(0, Position.height/10, Position.width, Position.height*0.9f);
        RectView = new Rect(0, 0, ScrollView.width, ScrollView.height);
        ScrollPos = Vector2.zero;        
    }
    private void Update()
    {
        if(Input.GetKeyDown(OpenKey))
        {
            ShowWindow = true;
        }
    }
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
		//if (type == LogType.Warning || type == LogType.Log || type == LogType.Error)
			//return;
		
		Log += "Type " + type + ": " + logString + "\n";
		if (type != LogType.Log) {
			Log += stackTrace + "\n";
		}
        
		if (Log.Length > 15000)
			Log = Log.Substring (Log.Length - 15000, 15000);
        if (AutoShow)
            ShowWindow = true;
    }
    private void OnGUI()
    {
        GUI.contentColor = Color.white;
        float minWidth, maxWidth;
        TextStyle.CalcMinMaxWidth(new GUIContent(Log), out minWidth, out maxWidth);
        float height = GUI.skin.label.CalcHeight(new GUIContent(Log), maxWidth);

        if (maxWidth > RectView.width)
            RectView.width = maxWidth;
        if (height >= RectView.height)
            RectView.height = height;

        if(ShowWindow)
            Position = GUI.Window(100, Position, DrawWindow, "");
    }

    private void DrawWindow(int id)
    {
        if (GUI.Button(CloseButton, "X"))
        {
            ShowWindow = false;
            Log = "";
        }

        ScrollPos = GUI.BeginScrollView(ScrollView, ScrollPos, RectView);
        TextStyle.normal.textColor = Color.white;
        TextStyle.fontSize = 18;
        //GUI.contentColor = Color.white;
        //GUI.color = Color.white;

        GUI.Label(RectView, Log, TextStyle);
        GUI.EndScrollView();

        GUI.DragWindow();
    }
}
