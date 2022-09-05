using UnityEngine;
using System.Collections;

namespace Us.Mobile.CoreBase.Model
{
    public class PlayerChessObj : PlayerObj
    {
        public string[] pieces { get; set; }
        public int color { get; set; }
        public string[] eatenPieces { get; set; }
        public long total_remain_time { get; set; }
        public int turn_remain_time { get; set; }
        public string levelname { get; set; }
        public int level { get; set; }
        public int side { get; set; }
        public int state { get; set; }

        public void SetPlayerObject(string username, string displayname,long cashgold,long cashsilver,int pos_on_server,int state, string avatar,int color, long total_remain_time, int turn_remain_time,string levelname, int level, int side)
        {
            this.username = username;
            this.displayname = displayname;
            this.cashgold = cashgold;
            this.cashsilver = cashsilver;
            this.pos_on_server = pos_on_server;
            this.state = state;
            this.avatar = avatar;
            this.color = color;
            this.total_remain_time = total_remain_time;
            this.turn_remain_time = turn_remain_time;
            this.levelname = levelname;
            this.level = level;
            this.side = side;
        }
        public void SetPlayerObject(string username, string displayname, long cashgold, long cashsilver, int pos_on_server,int state, string avatar,long total_remain_time, string levelname, int level)
        {
            this.username = username;
            this.displayname = displayname;
            this.cashgold = cashgold;
            this.cashsilver = cashsilver;
            this.pos_on_server = pos_on_server;
            this.state = state;
            this.avatar = avatar;
            this.total_remain_time = total_remain_time;
            this.levelname = levelname;
            this.level = level;
        }
        public void SetStatePlayerObject(int state)
        {
            this.state = state;
        }
        #region position convert
        public static int ToServerPosition(PlayerChessObj player, int localPos, int slotCount)
        {
            return (localPos + (player.pos_on_server - player.pos_on_table) + slotCount) % slotCount;
        }

        public static int ToTablePosition(PlayerChessObj player, int serverPos, int slotCount)
        {
            return (serverPos - (player.pos_on_server - player.pos_on_table) + slotCount) % slotCount;
        }
        #endregion
    }
}

