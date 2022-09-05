
using Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace AppConfig
{
    public class ClientConfig
    {
        public class UserInfo
        {
            public enum LoginType
            {
                GAME = 0,
                FACEBOOK,
                TRIAL
            }
            private static string username = string.Empty;
            private static string password = string.Empty;
            private static string nickname = string.Empty;
            private static string email = string.Empty;
            private static string session = string.Empty;
            private static byte viptype = 0;
            private static string vipname = string.Empty;
            private static string phone = string.Empty;
            private static long id = 0;
            private static string avatar_url = string.Empty;
            private static long gold = 0;
            private static long silver = 0;
            private static long gold_safe = 0;
            private static long flash = 0;
            private static int curvip = 0;
            private static int maxvip = 0;
            private static int level = 0;
            private static int exp_level_up = 0;
            private static int cur_exp_level = 0;
            private static string facebook_id = string.Empty;
            private static LoginType login_type = LoginType.GAME;
            private static bool isLogin = false;
            private static bool isX2Nap = false;


            private static string goldChip = "Chip";
            private static string silverChip = "Chip Bạc";

            public static void InitUserInfo()
            {
                UserInfo.username = PlayerPrefs.GetString("uname", string.Empty);
                UserInfo.password = PlayerPrefs.GetString("password", string.Empty);
                UserInfo.nickname = PlayerPrefs.GetString("nickname", string.Empty);
                UserInfo.email = PlayerPrefs.GetString("email", string.Empty);
                UserInfo.session = PlayerPrefs.GetString("session", "");
                //UserInfo.viptype = 0;
                //UserInfo.vipname = string.Empty;
                UserInfo.id = 0;
                UserInfo.phone = string.Empty;
                UserInfo.avatar_url = PlayerPrefs.GetString("avatar_url", string.Empty);
                //UserInfo.level = 0;
                //UserInfo.cur_exp_level = 0;
                //UserInfo.exp_level_up = 0;
                UserInfo.facebook_id = PlayerPrefs.GetString("facebook_id", string.Empty);
                UserInfo.login_type = (LoginType)PlayerPrefs.GetInt("login_type", (int)LoginType.GAME);
                UserInfo.isLogin = false;
            }

            public static string GOLD_TYPE
            {
                get
                {
                    return goldChip;
                }
            }

            public static string SILVER_TYPE
            {
                get
                {
                    return silverChip;
                }
            }

            #region USER INFO
            public static string UNAME
            {
                get
                {
                    return username;
                }
                set
                {
                    username = value;
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
                    PlayerPrefs.SetString("uname", username);
                    PlayerPrefs.Save();
#endif
                }
            }
            public static string PASSWORD
            {
                get
                {
                    return password;
                }
                set
                {
                    password = value;
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
                    PlayerPrefs.SetString("password", password);
                    PlayerPrefs.Save();
#endif
                }
            }
            public static string NICKNAME
            {
                get
                {
                    return nickname;
                }
                set
                {
                    nickname = value;
                }
            }

            public static string EMAIL
            {
                get
                {
                    return email;
                }
                set
                {
                    email = value;
                }
            }

            public static string SESSION
            {
                get
                {
                    return session;
                }
                set
                {
                    session = value;
                    PlayerPrefs.SetString("session", session);
                    PlayerPrefs.Save();
                }
            }
            
            public static byte VIPTYPE
            {
                get
                {
                    return viptype;
                }
                set
                {
                    viptype = value;
                }
            }

            public static string VIPNAME
            {
                get
                {
                    return vipname;
                }
                set
                {
                    vipname = value;
                }
            }

            public static string PHONE
            {
                get
                {
                    return phone;
                }
                set
                {
                    phone = value;
                }
            }

            public static long ID
            {
                get
                {
                    return id;
                }
                set
                {
                    id = value;
                }
            }
            public static int CURVIP
            {
                get
                {
                    return curvip;
                }
                set
                {
                    curvip = value;
                }
            }
            public static int MAXVIP
            {
                get
                {
                    return maxvip;
                }
                set
                {
                    maxvip = value;
                }
            }
            public static string AVATAR
            {
                get
                {
                    return avatar_url;
                }
                set
                {
                    avatar_url = value;
                }
            }
            public static long GOLD
            {
                get
                {
                    return gold;
                }
                set
                {
                    gold = value;
                }
            }
            public static long SILVER
            {
                get
                {
                    return silver;
                }
                set
                {
                    silver = value;
                }
            }

            public static long GOLD_SAFE
            {
                get
                {
                    return gold_safe;
                }
                set
                {
                    gold_safe = value;
                }
            }

            public static long FLASH
            {
                get
                {
                    return flash;
                }
                set
                {
                    flash = value;
                }
            }

            public static string FACEBOOK_ID
            {
                get
                {
                    return facebook_id;
                }
                set
                {
                    facebook_id = value;
                }
            }
            public static LoginType LOGIN_TYPE
            {
                get
                {
                    return login_type;
                }
                set
                {
                    login_type = value;
                }
            }

            public static bool IS_LOGIN
            {
                get
                {
                    return isLogin;
                }
                set
                {
                    isLogin = value;
                }
            }

            public static bool IS_EVENTX2
            {
                get
                {
                    return isX2Nap;
                }
                set
                {
                    isX2Nap = value;
                }
            }


            public static void SetUserInfo(string uname, string nickname, string passord, long id, string session, string avatar, long gold, long silver, int curvip, string vipname, int maxvip, byte _viptype, LoginType type)
            {
                UNAME = uname;
                NICKNAME = nickname;
                PASSWORD = passord;
                ID = id;
                SESSION = session;
                GOLD = gold;
                SILVER = silver;
                AVATAR = avatar;
                LOGIN_TYPE = type;
                VIPNAME = vipname;
                CURVIP = curvip;
                MAXVIP = maxvip;
                VIPTYPE = _viptype;
            }

            public static void SetUserInfo(string uname, string passord, long id, string avatar, long cash, int level, LoginType type)
            {
                UNAME = uname;
                PASSWORD = passord;
                ID = id;
                GOLD = cash;
                AVATAR = avatar;
                LOGIN_TYPE = type;
            }
            public static void CacheUserInfo(string uname, string password, string id, string avatar, long cash, int level, LoginType type)
            {
                PlayerPrefs.SetString("uname", uname);
                PlayerPrefs.SetString("password", password);
                PlayerPrefs.SetString("id", id);
                PlayerPrefs.SetFloat("cash", cash);
                PlayerPrefs.SetString("avatar", avatar);
                PlayerPrefs.SetInt("level", level);
                PlayerPrefs.SetInt("login_type", (int)type);
                PlayerPrefs.Save();
            }
            public static void ClearUserInfo()
            {
                SESSION = string.Empty;
                PASSWORD = string.Empty;
                ID = 0;
                GOLD = 0;
                CURVIP = 0;
                MAXVIP = 0;
                AVATAR = string.Empty;
                NICKNAME = string.Empty;
            }
            #endregion
        }

        public class SoftWare
        {
            private const string KEY_PREF_DBTOR = "cnf_dbtor_pref";
            private const string KEY_PREF_UTM_MEDIUM = "cnf_utm_medium_pref";

            /// <summary>
            /// software
            /// </summary>
            private static string version = "1.1.4";
            private static string dbtor = "p70";
            private static string utm_medium = "";

            public static void InitSoftWare()
            {
                //init software info
                SoftWare.VERSION = "1.0.1";
                SoftWare.DBTOR = PlayerPrefs.GetString(KEY_PREF_DBTOR, "p70");
                SoftWare.UTM_MEDIUM = PlayerPrefs.GetString(KEY_PREF_UTM_MEDIUM, "");
            }

            #region SOFTWARE
            public static string VERSION
            {
                get
                {
                    return version;
                }
                private set
                {
                    version = value;
                }
            }

            public static string DBTOR
            {
                get
                {
                    return dbtor;
                }
                set
                {
                    dbtor = value;
                    PlayerPrefs.SetString(KEY_PREF_DBTOR, dbtor);
                    PlayerPrefs.Save();
                }
            }

            public static string UTM_MEDIUM
            {
                get
                {
                    return utm_medium;
                }
                set
                {
                    utm_medium = value;
                    PlayerPrefs.SetString(KEY_PREF_UTM_MEDIUM, utm_medium);
                    PlayerPrefs.Save();
                }
            }
            #endregion

        }

        public class HardWare
        {
            /// <summary>
            /// hardware
            /// </summary>
            private static string imei = string.Empty;
            private static string cellid = string.Empty;
            private static string mnc = string.Empty;
            private static string mcc = string.Empty;
            private static string lac = string.Empty;
            private static string platform = string.Empty;
            private static string device = string.Empty;
            private static string macaddress = string.Empty;

            public static void InitHardWare()
            {
                //init hardware info
                HardWare.IMEI = SystemInfo.deviceUniqueIdentifier;

                HardWare.CELLID = "000000";
                HardWare.MNC = "04";
                HardWare.MCC = "452";
                HardWare.LAC = "0";
                //HardWare.PLATFORM = Application.platform.ToString();
#if UNITY_ANDROID
                HardWare.PLATFORM = RuntimePlatform.Android.ToString();
#elif UNITY_IOS
                 HardWare.PLATFORM = RuntimePlatform.IPhonePlayer.ToString();
#elif UNITY_STANDALONE_OSX
                 HardWare.PLATFORM = RuntimePlatform.OSXPlayer.ToString();
#elif UNITY_STANDALONE_WIN
                 HardWare.PLATFORM = RuntimePlatform.WindowsPlayer.ToString();
#elif UNITY_WEBGL
                HardWare.PLATFORM = Application.platform.ToString();
#endif
                HardWare.DEVICE = SystemInfo.deviceUniqueIdentifier;
            }

            #region HARDWARE
            public static string IMEI
            {
                get
                {
                    return imei;
                }
                private set
                {
                    imei = value != null ? value : "";
                }
            }

            public static string CELLID
            {
                get
                {
                    return cellid;
                }
                set
                {
                    cellid = value != null ? value : "";
                }
            }

            public static string MNC
            {
                get
                {
                    return mnc;
                }
                set
                {
                    mnc = value != null ? value : "";
                }
            }

            public static string MCC
            {
                get
                {
                    return mcc;
                }
                set
                {
                    mcc = value != null ? value : "";
                }
            }

            public static string LAC
            {
                get
                {
                    return lac;
                }
                set
                {
                    lac = value != null ? value : "";
                }
            }

            public static string PLATFORM
            {
                get
                {
                    return platform;
                }
                private set
                {
                    platform = value != null ? value : "";
                    if (platform.ToLower().Contains("iphone"))
                        platform = "iphone";
                    if (platform.ToLower().Contains("android"))
                        platform = "android";
                    if (platform.ToLower().Contains("windows"))
                        platform = "standalone";
                }
            }

            public static string DEVICE
            {
                get
                {
                    return device;
                }
                private set
                {
                    device = value != null ? value : "";
                }
            }

            public static string MACADDRESS
            {
                get
                {
                    return macaddress;
                }
                set
                {
                    macaddress = value != null ? value : "";
                }
            }
            #endregion
        }

        public class Setting
        {

            private static float valuesound = 1;
            private static float valuemusic = 1;

            public static void InitSetting()
            {
                valuesound = PlayerPrefs.GetFloat("valuesound", 1f);
                valuemusic = PlayerPrefs.GetFloat("valuemusic", 1f);
                
            }

            public static float VOLUM_SOUND
            {
                get
                {
                    return valuesound;
                }
                set
                {
                    valuesound = value;
                    PlayerPrefs.SetFloat("valuesound", valuesound);
                    PlayerPrefs.Save();
                }
            }
            public static float VOLUM_MUSIC
            {
                get
                {
                    return valuemusic;
                }
                set
                {
                    valuemusic = value;
                    PlayerPrefs.SetFloat("valuemusic", valuemusic);
                    PlayerPrefs.Save();
                }
            }
        }

        #region Sound
        public class StringValueAttribute : Attribute
        {
            public string Value { get; set; }
        }
        public class Sound
        {
            static SoundId curentBackgroundSound = SoundId.Background_Home;

            public enum SoundId
            {
                [StringValue(Value = "Sounds/BackgroundHome")]
                Background_Home,

                [StringValue(Value = "Sounds/ButtonClick")]
                ButtonClick,

                [StringValue(Value = "Sounds/Slot/background_slot_53")]
                Background_SlotFull,

                [StringValue(Value = "Sounds/Slot/item_move")]
                Slot_ItemMoving,

                [StringValue(Value = "Sounds/Slot/item_moved")]
                Slot_ItemMoved,
                [StringValue(Value = "Sounds/Slot/win_lines")]
                Slot_WinLines,

                [StringValue(Value = "Sounds/Slot/line_win")]
                Slot_WinLine,
                [StringValue(Value = "Sounds/Slot33/background_slot_33")]
                Background_Slot33,
                [StringValue(Value = "Sounds/Slot33/line_win")]
                Slot33_line_win,
                [StringValue(Value = "Sounds/Slot33/Win_Jackpot")]
                Slot33_win_jackpot,
                [StringValue(Value = "Sounds/Slot33/win_lines")]
                Slot33_win_lines,
                [StringValue(Value = "Sounds/Slot33/Win_mega")]
                Slot33_win_mega,
                
                [StringValue(Value = "Sounds/Slot/mega_win")]
                Slot_MegaWin,

                [StringValue(Value = "Sounds/DuaCho/background_dua_cho")]
                DuaCho_background_bet,
                [StringValue(Value = "Sounds/DuaCho/background_dua_cho_2")]
                DuaCho_background_racing,
                [StringValue(Value = "Sounds/DuaCho/bip0")]
                DuaCho_background_bip0,
                [StringValue(Value = "Sounds/DuaCho/bip1")]
                DuaCho_background_bip1,
                [StringValue(Value = "Sounds/DuaCho/sound_popper_party")]
                DuaCho_background_poper_party,
            }

            //static AudioSource[] Sounds = new AudioSource[100];
            //static List<AudioSource> Sounds = new List<AudioSource>(100);
            static Dictionary<SoundId, AudioSource> Sounds = new Dictionary<SoundId, AudioSource>();
            //static SoundId[] AutoPlaySounds = new SoundId[] { };
            //static SoundId AutoPlayLoopSounds = SoundId.Background_Home;

            /// <summary>
            /// co danh dau am thanh duoc mo hay ko, 0: disable, 1: enable
            /// </summary>
            private static int enable = 1;

            private static int enable_bgsound = 1;

            public static bool is_init_done = false;


            static SoundHelper soundHelper;

            internal static void InitSound()
            {
                enable = PlayerPrefs.GetInt("sound_enable", 1);
                enable_bgsound = PlayerPrefs.GetInt("bgsound_enable", 1);
            }

            internal static void InitSound(SoundHelper SoundHelper)
            {
                soundHelper = SoundHelper;
                enable = PlayerPrefs.GetInt("sound_enable", 1);
                enable_bgsound = PlayerPrefs.GetInt("bgsound_enable", 1);
                //Debug.LogError("InitSound, enable= " + enable);
                //Debug.LogError("InitSound, enable_bgsound= " + enable_bgsound);

                //load file trong thu muc resource
                Dictionary<string, SoundId> Maps = new Dictionary<string, SoundId>();
                foreach (var field in typeof(SoundId).GetFields())
                {
                    var attribute = (StringValueAttribute[])field.GetCustomAttributes(typeof(StringValueAttribute), false);
                    if (attribute.Length == 0) continue;
                    Maps[attribute[0].Value] = (SoundId)field.GetValue(null);
                    //Debug.LogError("------------------------------ InitSOund------------------------" + Maps[attribute[0].Value] + " ------ " + attribute[0].Value);
                }
                LoadAllSoundFromConfig(soundHelper.gameObject, Maps);

                is_init_done = true;
            }

            private static void GetSoundFilesFromDirectory(GameObject gameObject, Dictionary<string, SoundId> Maps, string dirpath)
            {
                DirectoryInfo dir = new DirectoryInfo(dirpath);
                FileInfo[] info = dir.GetFiles("*.*");
                DirectoryInfo[] dinfo = dir.GetDirectories();
                foreach (FileInfo f in info)
                {
                    if (f.Extension == ".ogg" || f.Extension == ".mp3" || f.Extension == ".wav")
                    {
                        string tempName = (dirpath + "/" + f.Name).Replace("Assets/Resources/", "");
                        string path = tempName.Substring(0, tempName.IndexOf('.'));
                        if (!Maps.ContainsKey(path))
                        {
                            //Debug.Log("Config has not key: " + path + ", " + Maps.ContainsKey(path));
                            continue;
                        }
                        LoadSoundFromBundle(gameObject, Maps[path], path);
                    }
                    else if (f.Extension != ".meta")
                    {
                        Debug.Log("Unknown Extension: " + (dirpath + "/" + f.Name));
                    }
                }

                foreach (DirectoryInfo d in dinfo)
                {

                    string tempName = d.Name;
                    GetSoundFilesFromDirectory(gameObject, Maps, dirpath + "/" + tempName);
                }
            }

            private static void LoadAllSoundFromConfig(GameObject gameObject, Dictionary<string, SoundId> Maps, UnityAction onLoadSoundDone = null)
            {
                Dictionary<string, SoundId>.KeyCollection.Enumerator enu = Maps.Keys.GetEnumerator();
                while (enu.MoveNext())
                {
                    string path = enu.Current;
                    LoadSound(gameObject, Maps[path], path);
                }
                if (onLoadSoundDone != null) onLoadSoundDone();
            }


            private static void LoadSound(GameObject gameObject, SoundId id, string path)
            {
                if (soundHelper != null)
                {
                    if (soundHelper.MapSound.ContainsKey(id))
                    {

                        AudioSource asrc = gameObject.AddComponent<AudioSource>();
                        asrc.clip = soundHelper.MapSound[id];

                        Sounds[id] = asrc;
                    }
                }
            }

            private static void LoadSoundFromBundle(GameObject gameObject, SoundId id, string path)
            {
                if (soundHelper != null)
                {
                    if (soundHelper.MapSound.ContainsKey(id))
                    {
                        AudioSource asrc = gameObject.AddComponent<AudioSource>();
                        asrc.clip = soundHelper.MapSound[id];
                        if (id == SoundId.ButtonClick)
                            asrc.volume = 0.3f;
                        Sounds[id] = asrc;
                    }
                    else
                    {
                        Debug.LogError("NOT BUNDLE : " + id);
                    }
                }
                else
                {
                    Debug.LogError("SoundAssetBundleHelper.Instance nulllll");
                }
            }

            public static bool ENABLE
            {
                get
                {
                    return enable == 1;
                }
                set
                {
                    int temp = value ? 1 : 0;

                    enable = temp;
                    PlayerPrefs.SetInt("sound_enable", enable);
                    PlayerPrefs.Save();
                }
            }

            public static bool ENABLE_BGSOUND
            {
                get
                {
                    return enable_bgsound == 1;
                }
                set
                {
                    int temp = value ? 1 : 0;
                    enable_bgsound = temp;
                    //if (!value) StopBackgroundSound();
                    //else
                    //{
                    //    if (curentBackgroundSound != SoundId.Background_Home) PlayLoopSound(curentBackgroundSound);
                    //}

                    PlayerPrefs.SetInt("bgsound_enable", enable_bgsound);
                    PlayerPrefs.Save();
                }
            }

            public static void ChangeBackgroundSound(SoundId bgsoundid)
            {
                return;
                //StopBackgroundSound();
                //AutoPlayLoopSounds[0] = bgsoundid;
                //if (ENABLE_BGSOUND)
                //    PlayLoopSound(AutoPlayLoopSounds[0]);
            }

            /// <summary>
            /// play 1 sound trong khoang thoi gian duration, goi ngoai mainthread
            /// </summary>
            /// <param name="soundId">tham so sound co the sua tuy theo api</param>
            /// <param name="duration">tham so thoi gian co the sua tuy vao api</param>
            public static void PlaySound(SoundId soundId)
            {
                
                try
                {
                    if (ENABLE && Sounds[soundId] != null)
                    {
                        Sounds[soundId].volume = Setting.VOLUM_MUSIC;
                        Sounds[soundId].loop = false;
                        Sounds[soundId].Play();
                    }
                }
                catch (Exception exception)
                {
                    Debug.Log("PlaySound error: " + exception.Message);
                }
            }

            public static void PlayLoopSound(SoundId soundId)
            {
                try
                {
                    if (ENABLE_BGSOUND && Sounds[soundId] != null)
                    {
                        //play sound
                        Sounds[soundId].volume = Setting.VOLUM_SOUND;
                        Sounds[soundId].loop = true;
                        Sounds[soundId].Play();

                        curentBackgroundSound = soundId;
                    }
                }
                catch (Exception exception)
                {
                    Debug.Log("PlayLoopSound error: " + exception.Message);

                }
            }


            /// <summary>
            /// sStop all sound
            /// </summary>
            public static void StopSound(SoundId soundId)
            {
                try
                {
                    if (Sounds[soundId] != null)
                    {
                        Sounds[soundId].Stop();
                    }
                }
                catch (Exception exception)
                {
                    Debug.Log("StopSound error: " + exception.Message);

                }
            }

            public static void StopAllSound()
            {
                Dictionary<SoundId, AudioSource>.Enumerator elements = Sounds.GetEnumerator();
                while (elements.MoveNext())
                    if (elements.Current.Value != null)
                        elements.Current.Value.Stop();
            }


            public static void StopBackgroundSound()
            {
                try
                {
                    Sounds[curentBackgroundSound].Stop();

                }
                catch (Exception exception)
                {
                    Debug.Log("StopBackgroundSound error: " + exception.Message);

                }
            }
            public static void StopBackgroundSound(SoundId soundId)
            {
                try
                {
                    Sounds[soundId].Stop();
                }
                catch (Exception exception)
                {
                    Debug.Log("StopBackgroundSound error: " + exception.Message);

                }
            }

            public static void PauseSound(SoundId soundId)
            {
                try
                {
                    if (Sounds[soundId] != null)
                    {
                        Sounds[soundId].Pause();
                    }
                }
                catch (Exception exception)
                {
                    Debug.Log("PauseSound error: " + exception.Message);

                }
            }
        }

        #endregion
        public static void InitClient()
        {
            Languages.Language.LoadLanguageFile(Languages.Language.LANG);
            Sound.InitSound();
            UserInfo.InitUserInfo();
            SoftWare.InitSoftWare();
            HardWare.InitHardWare();
            Setting.InitSetting();
        }
    }
}
