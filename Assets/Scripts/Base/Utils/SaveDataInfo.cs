using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataInfo
{
	private const string enableSoundKey = "enableSound";
	private const string enableMusicKey = "enableMusic";
	private const string enableNotificationKey = "enableNotification";
	private const string volumeSoundKey = "enableSoundVolume";
	private const string volumeMusicKey = "enableMusicVolume";
	private const string enableVibrationKey = "enableVibration";
	private const string languageKey = "language";
	private const string openAppCounter = "openAppCounter";
	private const string ishint = "hint";
	private const string enableCollapseKey = "enableCollapse";
	private const string autoLoginKey = "autoLogin";

	public static float VolumeSound {
		get {
			if (PlayerPrefs.HasKey (volumeSoundKey))
				return PlayerPrefs.GetFloat (volumeSoundKey);
			else
				return 1f;
		}
		set {
			PlayerPrefs.SetFloat (volumeSoundKey, value);
		}
	}

	public static float VolumeMusic {
		get {
			if (PlayerPrefs.HasKey (volumeMusicKey))
				return PlayerPrefs.GetFloat (volumeMusicKey);
			else
				return 1f;
		}
		set {
			PlayerPrefs.SetFloat (volumeMusicKey, value);
		}
	}


	public static bool EnableSound {
		get {
			if (PlayerPrefs.HasKey (enableSoundKey)) {
				var temp = PlayerPrefs.GetInt (enableSoundKey);
				return temp == 1;
			} else {
				PlayerPrefs.SetInt (enableSoundKey, 1);
				return true;
			}
		}
		set {
			PlayerPrefs.SetInt (enableSoundKey, value ? 1 : 0);
		}
	}

	public static bool EnableVibration {
		get {
			if (PlayerPrefs.HasKey (enableVibrationKey)) {
				var temp = PlayerPrefs.GetInt (enableVibrationKey);
				return temp == 1;
			} else {
				PlayerPrefs.SetInt (enableVibrationKey, 1);
				return true;
			}
		}
		set {
			PlayerPrefs.SetInt (enableVibrationKey, value ? 1 : 0);
		}
	}


	public static bool EnableMusic {
		get {
			if (PlayerPrefs.HasKey (enableMusicKey)) {
				var temp = PlayerPrefs.GetInt (enableMusicKey);
				return temp == 1;
			} else {
				PlayerPrefs.SetInt (enableMusicKey, 1);
				return true;
			}
		}
		set {
			PlayerPrefs.SetInt (enableMusicKey, value ? 1 : 0);
		}
	}

	public static bool EnableNotification {
		get {
			if (PlayerPrefs.HasKey (enableNotificationKey)) {
				var temp = PlayerPrefs.GetInt (enableNotificationKey);
				return temp == 1;
			} else {
				PlayerPrefs.SetInt (enableNotificationKey, 1);
				return true;
			}
		}
		set {
			PlayerPrefs.SetInt (enableNotificationKey, value ? 1 : 0);
		}
	}


	// public static Languages Language {
	// 	get {
	// 		if (PlayerPrefs.HasKey (languageKey)) {
	// 			string lang = PlayerPrefs.GetString (languageKey);
	// 			switch (lang) {
	// 			case "en":
	// 				return Languages.en;
	// 			case "vn":
	// 				return Languages.vn;
	// 			default:
	// 				return Languages.vn;
	// 			}
	// 		} else
	// 			return Languages.vn;
	// 	}
	// 	set {
	// 		PlayerPrefs.SetString (languageKey, value.ToString ());
	// 	}
	// }

	/// <summary>
	/// Gets or sets the count open app.
	/// </summary>
	/// <value>The count open app.</value>
	public static int AppOpeningCounter {
		get {
			if (PlayerPrefs.HasKey (openAppCounter))
				return PlayerPrefs.GetInt (openAppCounter);
			else
				return 0;
		}
		set {
			PlayerPrefs.SetInt (openAppCounter, value);
		}
	}

	public static bool EnableHint {
		get {
			if (PlayerPrefs.HasKey (ishint)) {
				var temp = PlayerPrefs.GetInt (ishint);
				return temp == 0;
			} else {
				PlayerPrefs.SetInt (ishint, 0);
				return true;
			}
		}
		set {
			PlayerPrefs.SetInt (ishint, value ? 0 : 1);
		}
	}

	public static bool EnableCollapse {
		get {
			if (PlayerPrefs.HasKey (enableCollapseKey)) {
				var temp = PlayerPrefs.GetInt (enableCollapseKey);
				return temp == 1;
			} else {
				PlayerPrefs.SetInt (enableCollapseKey, 0);
				return false;
			}
		}
		set {
			PlayerPrefs.SetInt (enableCollapseKey, value ? 1 : 0);
		}
	}

	public static int numberAutoLogin {
		get {
			if (PlayerPrefs.HasKey (autoLoginKey))
				return PlayerPrefs.GetInt (autoLoginKey);
			else
				return 0;
		}
		set {
			PlayerPrefs.SetInt (autoLoginKey, value);
		}
	}
}
