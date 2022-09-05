using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Player
{
    public class PlayerModel
    {
        public string nickname { get; set; }
        public long user_id { get; set; }
        public long cash { get; set; }
        public int pos_on_server { get; set; }
        public int pos_on_table { get; set; }
        public string avatar { get; set; }
        public long total_remain_time { get; set; }
        public long turn_remain_time { get; set; }
        public int vip_type { get; set; }

    }
}
