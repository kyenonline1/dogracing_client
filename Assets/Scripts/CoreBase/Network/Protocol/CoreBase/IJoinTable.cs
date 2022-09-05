using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GameProtocol.Protocol
{
    public interface IJoinTable
    {
        string GameId { get; set; }

        string TableId { get; set; }


        

        // jointype = 0: vào bàn bình thường
        // jointype = 1: mời vào bàn
        // jointype = 2: chơi ngay
        int JoinType { get; set; }
        
        string Password { get; set; }
    }

    public interface IJoinTableDetail
    {
        string GameId { get; set; }

        string TableId { get; set; }
    }
}
