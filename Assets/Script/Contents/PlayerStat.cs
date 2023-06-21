using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공통적인건 상속해주고 플레이만 가질것들은 따로 생각해서
public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;

    public int Exp 
    {
        get { return _exp; } 
        set 
        { 
            _exp = value; 
        } 
    }

    public int Gold { get { return _gold; } set { _gold = value; } }


    private void Start()
    {
        _level = 1;
        _exp = 0;
        _defence = 5;
        _moveSpeed = 5.0f;
        _gold = 0;

        SetStat(_level);
    }

    public void SetStat(int _level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[_level];

        _hp = stat.maxHp;
        _maxHp = stat.maxHp;
        _attack = stat.attack;
    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");
    }
}
