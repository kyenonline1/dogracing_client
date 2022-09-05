#if FB
using UnityEngine;
using System.Collections.Generic;

//using SimpleJSON;
using System;
using Facebook.Unity;
using LitJson;
using SimpleJSON;

public class FacebookHelper : MonoBehaviour
{

	public static FacebookHelper Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject go = new GameObject();
				instance = go.AddComponent<FacebookHelper>();
				go.name = "FacebookHelper";
				if (!string.IsNullOrEmpty(controllersHolder))
				{
					GameObject parent = GameObject.Find(controllersHolder) as GameObject;
					if (parent != null)
					{
						go.transform.parent = parent.transform;
					}
				}
			}
			return instance;
		}
	}

	public bool IsLoggedIn
	{
		get { return FB.IsLoggedIn; }
	}

	public bool IsInitialized
	{
		get { return FB.IsInitialized; }
	}

	private static FacebookHelper instance;
	private static string controllersHolder = "FacebookHelper";

	private Dictionary<string, string> profile;
	private string userName;
	private string email;

	public string UserName
	{
		set
		{
			userName = value;
		}
		get
		{
			return userName;
		}
	}

	// Event Handler, all Delegate must be set at Start(), not Awake()
	//	public delegate void SetInitEventHandler ();
	//	public event SetInitEventHandler SetInitListener;
	//
	//	public delegate void HideUnityEventHandler (bool isGameShown);
	//	public event HideUnityEventHandler HideUnityListener;

	//	public delegate void LogInEventHandler (FBResult result);
	//	public event LogInEventHandler LogInListener;

	//	public delegate void InviteFriendEventHandler (FBResult result);
	//	public event InviteFriendEventHandler InviteFriendListener;


	//	public delegate void GetFriendsCountEventHandler (FBResult result);
	//	public event InviteFriendEventHandler GetFriendsCountListener;

	//	public delegate void PostFeedEventHandler (FBResult result);
	//	public event PostFeedEventHandler PostFeedListener;

	//	public delegate void CheckPermissionHanler (bool passAble);
	//	public event CheckPermissionHanler CheckPermissionListener;

	void Awake()
	{
		//don't destroy this
		DontDestroyOnLoad(transform.gameObject);
		if (FB.IsInitialized)
		{
			FB.ActivateApp();
		}
		else
		{
			CallFBInit();
		}
	}

	#region FB INIT

	private void CallFBInit()
	{
		//Debug.Log("FBHelper: CallFBInit");
		FB.Init(SetInit, OnHideUnity);
	}

	// The Callback called after finishing FB.Init()
	private void SetInit()
	{
		Debug.Log("FBHelper: SetInit");
		//		if (SetInitListener != null)
		//			SetInitListener ();
		//OnLogoutFBClick ();//For Testing
		//				if (FB.IsLoggedIn) {                                                                                        
		//						Util.Log ("Already logged in");                                                
		//						OnLoggedIn ();                                                                        
		//				}        

		FB.ActivateApp();
	}

	// The Callback called when FB dialog is showing,
	// used for disabling unity's input, etc...
	private void OnHideUnity(bool isGameShown)
	{
		//Debug.Log("FBHelper: OnHideUnity");
		if (!isGameShown)
		{
			// pause the game - we will need to hide                                             
			Time.timeScale = 0;
		}
		else
		{
			// start the game back up - we're getting focus again                                
			Time.timeScale = 1;
		}
		//		if (HideUnityListener != null)
		//			HideUnityListener (isGameShown);
	}

	#endregion

	#region FB LOGIN

	public void LoginFB(List<string> permissions, Action<ILoginResult> callback)
	{
		if (permissions != null)
			FB.LogInWithReadPermissions(permissions, (result) => {
				if (callback != null)
					callback(result);
			});
		else
			FB.LogInWithReadPermissions(new string[] { "email", "public_profile" }, (result) => {
				if (callback != null)
					callback(result);
			});
	}

	public void LoginFB(Action<ILoginResult> callback)
	{
		//FB.LogInWithReadPermissions(new string[] { "email", "public_profile" }, (result) => {
		//    if (callback != null)
		//        callback(result);
		//});
		FB.LogInWithReadPermissions(new string[] { "email", "public_profile" }, (result) =>
		{
			Debug.Log("LogInWithReadPermissions: " + result.Error);
			Debug.Log("LogInWithReadPermissions: result " + result);
			Debug.Log("LogInWithReadPermissions: AccessToken " + result.AccessToken);
			if (result.AccessToken != null)
				Debug.Log("LogInWithReadPermissions: Permissions " + result.AccessToken.Permissions);
			if (callback != null)
				callback(result);
		});
	}


	#endregion

	#region FB API

	public void API(string query, HttpMethod method, FacebookDelegate<IGraphResult> callback)
	{
		FB.API(query, method, callback);
	}

	#endregion

	#region FB LOGOUT

	public void LogoutFB()
	{
		FB.LogOut();
	}

	#endregion

	#region FB INVITE

	// Note: only send invite to mobile user, facebook on pc browser won't show the invitation
	public void InviteFBFriend(string msg, Action<IAppRequestResult> callback)
	{
		if (msg == null)
			msg = "Come play this great game!";
		FB.AppRequest(
			to: null,
			message: msg,
			callback: (result) => {
				if (callback != null)
					callback(result);
			}
		);
	}

	public void InviteFBFriend(string msg, string[] toID, Action<IAppRequestResult> callback)
	{
		if (msg == null)
			msg = "Come play this great game!";

		FB.AppRequest(
			msg,
			toID,
			null,
			null,
			null,
			null,
			null,
			(result) => {
				if (callback != null)
				{
					callback(result);
				}
			});
	}

	#endregion

	//	#region FB GET FRIEND COUNTS
	//	private void GetFriendsCountCallBack (FBResult result)
	//	{
	//		if (GetFriendsCountListener != null)
	//			GetFriendsCountListener (result);
	//	}
	//
	//	public void GetFriendsCount ()
	//	{
	////		FB.API ("/me?fields=friends", HttpMethod.GET, GetFriendsCountCallBack);
	//		FB.API ("/me?fields=friends", HttpMethod.GET, GetFriendsCountCallBack);
	//	}
	//
	//	#endregion

	//		//Called on Scene Change. Clean up Event 
	//void OnLevelWasLoaded ()
	//{
	//	//		SetInitListener = null;
	//	//		HideUnityListener = null;
	//	//		LogInListener = null;
	//	//		InviteFriendListener = null;
	//	//		CheckPermissionListener = null;
	//	//		GetFriendsCountListener = null;
	//	//		PostFeedListener = null;
	//}


	#region FB POST FEED

	public string FeedToId = "";
	public string FeedLink = "http://www.youtube.com";
	public string FeedLinkName = "COTUONG";
	public string FeedLinkCaption = "A Best game of the...worsts";
	public string FeedLinkDescription = "There is only one worst game, you know?";
	public string FeedPicture = "http://fptshop.com.vn/Uploads/images/9256366.jpg";
	public string FeedMediaSource = "";
	public string FeedActionName = "";
	public string FeedActionLink = "";
	public string FeedReference = "";
	public bool IncludeFeedProperties = false;
	//private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]> ();

	private void FBFeed(Action<IResult> callback)
	{
		//Dictionary<string, string[]> feedProperties = null;
		//if (IncludeFeedProperties) {
		//	feedProperties = FeedProperties;
		//}
		FB.FeedShare(
			link: new Uri(FeedLink),
			linkName: FeedLinkName,
			linkCaption: FeedLinkCaption,
			linkDescription: FeedLinkDescription,
			picture: new Uri(FeedPicture)
		);
	}

	public void OpenFBFeedDialog(Action<IResult> callback)
	{
		FBFeed(callback);
	}

	#endregion

	#region FB CheckPermissions

	public void CheckFriendPermission(Action<bool> CheckPermissionListener)
	{
		FB.API("/me/permissions", HttpMethod.GET, (result) => {
			if (!string.IsNullOrEmpty(result.Error))
			{
				Debug.LogError("result is NullEmpty!: " + result.Error);
				if (CheckPermissionListener != null)
					CheckPermissionListener(false);
			}
			else
			{
				// Debug.LogError("Get user's permissions was successful!");
				// Debug.LogError("Result: " + result.RawResult);

				JsonData N = JsonMapper.ToObject(result.RawResult);

				Debug.LogError("CheckFriendPermission data lenght: " + N["data"].Count);

				//JSONNode N = JSON.Parse(result.RawResult);
				//JSONArray Arr = N["data"].AsArray;

				bool hasPermission = false;
				for (int i = 0; i < N["data"].Count; i++)
				{
					string permissions = N["data"][i]["permission"].ToString();
					//Debug.LogError ("permissions: " + permissions);
					//Debug.LogError ("permissions11111111: " + N ["data"] [i] ["permission"].ToString ());
					//if (permissions.Equals ("user_friends")) {
					if (permissions.Equals("public_profile"))
					{
						hasPermission = true;
						string status = N["data"][i]["status"].ToString();
						//Debug.LogError ("status: " + permissions);
						//Debug.LogError ("status11111111111: " + N ["data"] [i] ["status"].ToString ());
						if (!status.Equals("granted"))
						{
							if (CheckPermissionListener != null)
								CheckPermissionListener(false);
						}
						else
						{
							if (CheckPermissionListener != null)
								CheckPermissionListener(true);
						}
						break;
					}
					else if (permissions.Equals("gaming_profile"))
					{
						hasPermission = true;
						string status = N["data"][i]["status"].ToString();
						//Debug.LogError ("status: " + permissions);
						//Debug.LogError ("status11111111111: " + N ["data"] [i] ["status"].ToString ());
						if (!status.Equals("granted"))
						{
							if (CheckPermissionListener != null)
								CheckPermissionListener(false);
						}
						else
						{
							if (CheckPermissionListener != null)
								CheckPermissionListener(true);
						}
						break;
					}
				}
				if (!hasPermission)
				{
					if (CheckPermissionListener != null)
						CheckPermissionListener(false);
				}
			}
		});
	}

	#endregion

	#region Get Invitable Friends

	public void LoadInvitableFriends(Action<bool, List<FacebookInvitableFriend>> onLoadFriendInvitationCompletedAction)
	{
		FB.API("/me/invitable_friends?limit=5000", HttpMethod.GET, (result) => {
			if (result.Error != null)
			{
				//Debug.LogError ("FBHelper: " + result.Error);
				onLoadFriendInvitationCompletedAction(false, null);
			}
			else
			{
				//var d = result.GetField("data") as WWW;
				var listFriendInvitation = new List<FacebookInvitableFriend>();
				var data = result.RawResult;
				Debug.Log("friendInvitableData:: " + data);
				if (string.IsNullOrEmpty(data))
				{
					//Debug.Log("----> " + this + ":LoadFriendInvitation: result.Text is null");
					return;
				}
				//JSONNode N = JSON.Parse(result.RawResult);
				//JSONArray Arr = N["data"].AsArray;

				//JsonData N = JsonMapper.ToObject (result.RawResult);
				//            JsonData Arr = N["data"];
				JSONNode N = JSON.Parse(result.RawResult);
				JSONArray Arr = N["data"].AsArray;
				//Debug.LogError ("LoadInvitableFriend data: " + Arr.ToString());
				int totalInvitableFriends = Arr.Count;
				if (totalInvitableFriends == 0)
				{
					onLoadFriendInvitationCompletedAction(false, null);
					return;
				}
				for (int i = 0; i < totalInvitableFriends; i++)
				{
					var facebookFriendInfo = new FacebookInvitableFriend().Parse(Arr[i]);
					listFriendInvitation.Add(facebookFriendInfo);
				}
				onLoadFriendInvitationCompletedAction(true, listFriendInvitation);
			}
		});
	}


	public void LoadFBFriends(Action<bool, List<FacebookInvitableFriend>> onLoadFriendInvitationCompletedAction)
	{
		FB.API("/me/friends?limit=5000", HttpMethod.GET, (result) => {
			if (result.Error != null)
			{
				//Debug.Log("FBHelper: " + result.Error);
				onLoadFriendInvitationCompletedAction(false, null);
			}
			else
			{
				//var d = result.GetField("data") as WWW;
				//var listFriendInvitation = new List<FacebookInvitableFriend> ();
				var data = result.RawResult;
				//Debug.Log("LoadFBFriends: " + data);
				if (string.IsNullOrEmpty(data))
				{
					//Debug.Log("----> " + this + ":LoadFriendInvitation: result.Text is null");
					return;
				}
				//JsonData N = JsonMapper.ToObject (result.RawResult);

				//Debug.LogError ("LoadFbFriend data: " + N ["data"].ToString ());

				//JSONNode N = JSON.Parse(result.RawResult);
				//JSONArray Arr = N["data"].AsArray;
				//int totalInvitableFriends = Arr.Count;
				//if (totalInvitableFriends == 0)
				//{
				//    onLoadFriendInvitationCompletedAction(false, null);
				//    return;
				//}
				//for (int i = 0; i < totalInvitableFriends; i++)
				//{
				//    var facebookFriendInfo = new FacebookInvitableFriend().Parse(Arr[i]);
				//    listFriendInvitation.Add(facebookFriendInfo);
				//}
				//onLoadFriendInvitationCompletedAction(true, listFriendInvitation);
			}
		});
	}

	public class FacebookInvitableFriend
	{
		public string Id;
		public string FacebookName;
		public string PictureLink;

		public FacebookInvitableFriend Parse(JSONNode N)
		{
			Id = N["id"];
			FacebookName = N["name"];
			PictureLink = N["picture"]["data"]["url"];
			return this;
		}
	}

	#endregion

	public string GetAccessToken()
	{
		return AccessToken.CurrentAccessToken.TokenString;
	}
}
#endif