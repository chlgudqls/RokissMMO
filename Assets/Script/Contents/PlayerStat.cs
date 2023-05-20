using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공통적인건 상속해주고 플레이만 가질것들은 따로 생각해서
public class PlayerStat : Stat
{
    [SerializeField]
    int _exp;
    [SerializeField]
    int _gold;

    public int Exp { get { return _exp; } set { _exp = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }
}
