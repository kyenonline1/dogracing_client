using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssetBundles
{
    public class TagAssetBundle
    {

        public class Tag_Scene
        {
            public static readonly string TAG_SCENE_KHUNGGAME = "scene_khunggame";

            public static readonly string TAG_SCENE_POKER = "scene/poker";
        }

        public class SceneName
        {
            public static readonly string HOME_SCENE = "HomeScene";
            public static readonly string LOBBY_SCENE = "LobbyScene";

            public static readonly string TLMNSL_SCENE = "TLMNSolo";
            public static readonly string POKER_SCENE = "PokerScene";
            public static readonly string POKER_REVIEW_SCENE = "ReviewPokerScene";
            public static readonly string XITO_SCENE = "XitoScene";
            public static readonly string DUACHO = "DuaChoScene";
        }

        public class Tag_UI
        {
            public static readonly string TAG_UI_KHUNGGAME = "ui_khunggame";
            public static readonly string TAG_UI_COMMON = "ui_common";
        }

        public class AtlasName
        {
            public static readonly string LOBBY_ATLAS = "Lobby";
            public static readonly string LOBBY_AVATAR = "AvatarAtlas";
        }

        public class Tag_Prefab
        {
            public static readonly string TAG_PREFAB_KHUNGGAME = "prefab_khunggame";
            public static readonly string TAG_PREFAB_COMMON = "prefab_common";

        }
    }
}
