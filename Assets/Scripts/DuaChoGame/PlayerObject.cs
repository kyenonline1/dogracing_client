using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject  {

    private string username;
    private long money;
    private string avatar;
    private int pos;
    private int vip;

    public void InitData(string uname, long money, string avatar, int vip = 0)
    {
        this.username = uname;
        this.money = money;
        this.avatar = avatar;
        this.vip = vip;
    }

    public string UNAME
    {
        get
        {
            return username;
        }
    }

    public long MONEY
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
        }
    }

    public string AVATAR
    {
        get
        {
            return avatar;
        }
    }

    public int POSITION
    {
        get
        {
            return pos;
        }
        set
        {
            pos = value;
        }
    }

    public int VIP
    {
        get
        {
            return vip;
        }
        set
        {
            vip = value;
        }
    }
}
