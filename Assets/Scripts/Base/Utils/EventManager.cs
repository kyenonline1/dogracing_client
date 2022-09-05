using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Utilities;

namespace Base.Utils
{
	public class EventManager : MonoBehaviour {
		public static readonly string LOGIN_TOPIC 					= "LOGIN_TOPIC";
		public static readonly string LOGOUT_TOPIC 					= "LOGOUT_TOPIC";
		public static readonly string ACCOUNT_INFO_TOPIC 			= "ACCOUNT_INFO_TOPIC";
		public static readonly string CONNECT_SUCCESS_TOPIC			= "CONNECT_SUCCESS";
		public static readonly string CONNECT_FAIL_TOPIC			= "CONNECT_FAIL";
		public static readonly string CONNECION_LOST				= "CONNECTION_LOST";
		public static readonly string CHANGE_SOUND_SETTING			= "CHANGE_SOUND_SETTING";
		public static readonly string CHANGE_AVATAR_TOPIC			= "CHANGE_AVATAR_TOPIC";
        public static readonly string CHANGE_NICKNAME_TOPIC =       "CHANGE_NICKNAME_TOPIC";
        public static readonly string CHANGE_NUMBERPHONE_TOPIC =    "CHANGE_NUMBERPHONE_TOPIC";
        public static readonly string CHANGE_LANGUAGE_TOPIC			= "CHANGE_LANGUAGE_TOPIC";
		public static readonly string CHANGE_BALANCE				= "CHANGE_BALANCE";
		public static readonly string LOGIN_FAIL_TOPIC				= "LOGIN_FAIL_TOPIC";

		public static readonly string CHANGE_VIEW_TOPIC				= "CHANGE_VIEW_TOPIC";
		public static readonly string CHANGE_MUSIC_SETTING			= "CHANGE_MUSIC_SETTING";
		public static readonly string CHANGE_USER_BALANCE_TOPIC		= "CHANGE_USER_BALANCE_TOPIC";


        public static readonly string CHANGE_VOLUME_MUSIC_SETTING 	= "CHANGE_VOLUME_MUSIC_SETTING";
        public static readonly string CHANGE_VOLUME_SOUND_SETTING 	= "CHANGE_VOLUME_SOUND_SETTING";

		public static readonly string OPEN_LAST_SCREEN_AT_HOME		= "OPEN_LAST_SCREEN_AT_HOME";

		public static readonly string CLOSE_MINIGAME				= "CLOSE_MINIGAME";

        public static readonly string CHANGE_CASH_TYPE_LOBBY        = "CHANGE_CASH_TYPE_LOBBY";

        public static readonly string DISABLE_MINIGAME              = "DISABLE_MINIGAME";
        public static readonly string ENABLE_MINIGAME = "ENABLE_MINIGAME";

        public static readonly string CLOSE_FEATURE_POPUP = "CLOSE_FEATURE_POPUP";

        public static readonly string SCREEN_SHORT = "SCREEN_SHORT";
        public static readonly string OPEN_EVENT = "OPEN_EVENT";
        public static readonly string CHANGE_MUSIC = "CHANGE_MUSIC";
        public static readonly string OPEN_TAIXIU = "OPEN_TAIXIU";
        public static readonly string OPEN_MINIPOKER = "OPEN_MINIPOKER";
        public static readonly string OPEN_SLOT33HOAPHUNG = "OPEN_SLOT33HOAPHUNG";
        //public static readonly string OPEN_F1SLOT= "OPEN_F1SLOT";
        //public static readonly string OPEN_MYNHAN = "OPEN_MYNHAN";
        public static readonly string OPEN_LUCKYWHEEL = "OPEN_LUCKYWHEEL";
        public static readonly string READINGINBOX = "READINGINBOX";

        public static EventManager Instance
		{
			get;
			private set;
		}

		private Dictionary<string, List<Action>> Topics = new Dictionary<string, List<Action>>();
        

		void Awake()
		{
			if(Instance != null)
			{
				Destroy(gameObject);
				return;
			}
            DontDestroyOnLoad(this.gameObject);
			Instance = this;
            CreateDefaultTopic();
		}

        private void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Input.multiTouchEnabled = false;
#if UNITY_STANDALONE || UNITY_EDITOR
            QualitySettings.vSyncCount = 2;
            Application.targetFrameRate = 100;
#elif UNITY_WEBGL
			Application.targetFrameRate = 80;
#elif UNITY_WP8 || UNITY_WP_8_1 || UNITY_WSAPlayer || UNITY_WSA
			Application.targetFrameRate = 30;
			QualitySettings.vSyncCount = 0;
#elif UNIY_ANDROID
            Application.targetFrameRate = 60;
			QualitySettings.vSyncCount = 0;
#else
			Application.targetFrameRate = 60;
			QualitySettings.vSyncCount = 0;
#endif
        }

        #region API
        public void CreateTopic(string TopicName)
		{
			if(Topics.ContainsKey(TopicName))
				return;

			Topics.Add(TopicName, new List<Action>());
		}

		public void SubscribeTopic(string TopicName, Action Callback)
		{
			if(!Topics.ContainsKey(TopicName))
			{
				Topics[TopicName] = new List<Action>();
			}

			var subscribers = Topics[TopicName];
			if(subscribers == null)
			{
				subscribers = new List<Action>();
				Topics[TopicName] = subscribers;
			}

			subscribers.Add(Callback);
		}


		public void UnSubscribeTopic(string TopicName, Action subscriber)
		{
			if(!Topics.ContainsKey(TopicName))
				return;

			var subscribers = Topics[TopicName];
			if(subscribers == null)
			{
				subscribers = new List<Action>();
				Topics[TopicName] = subscribers;
				return;
			}

			if(Topics[TopicName].Remove(subscriber))
			{
				LogMng.Log("EVENT MANAGER","REMOVE SUBSCRIBER SUCCESS : " + subscriber.Method.Name);
			}
			else
			{
                LogMng.Log("EVENT MANAGER", "REMOVE SUBSCRIBER FAIL : " + subscriber.Method.Name);
			}
		}

		public void RaiseEventInTopic(string TopicName)
		{
			if(!Topics.ContainsKey(TopicName))
				return;

			var subscribers = Topics[TopicName];
			if(subscribers == null)
			{
				subscribers = new List<Action>();
				Topics[TopicName] = subscribers;
				return;
			}

			int subscribersCount = subscribers.Count;

			List<Action> RemovableSubscribers = new List<Action>();

			for(int i = 0; i < subscribersCount; i++)
			{
				var subscriber = subscribers[i];
				try
				{
					if(subscriber != null)
						subscriber();
				}
				catch(Exception e)
				{
                    LogMng.Log("EVENT MANAGER", String.Format("Exception when call subscriber for topic {0} : {1} - {2}", TopicName, e.Message, e.StackTrace));
					RemovableSubscribers.Add(subscriber);
				}
			}

			int RemovableSubscriberCount = RemovableSubscribers.Count;
			for(int i = 0; i < RemovableSubscriberCount; i++)
			{
				UnSubscribeTopic(TopicName, RemovableSubscribers[i]);
			}
		}
        #endregion

        #region Processor
        private void CreateDefaultTopic()
        { 
			CreateTopic(CHANGE_SOUND_SETTING); 
            CreateTopic(CHANGE_VOLUME_SOUND_SETTING);
			CreateTopic (CHANGE_LANGUAGE_TOPIC);
			CreateTopic (CHANGE_BALANCE);
        }
        #endregion

#if UNITY_WEBGL
    public bool IsWebFocus = true;

    public void OnResize(string param)
    {
        var str = param.Split('|');
        Screen.SetResolution(int.Parse(str[0]), int.Parse(str[1]), Screen.fullScreen);
    }

    public void OnWebTabFocus(int isWebFocus)
    {
        IsWebFocus = (isWebFocus == 1);
    }

    private void Update()
    {
        if (!Application.runInBackground)
        {
            Application.runInBackground = true;
        }
    }
#endif
    }
}