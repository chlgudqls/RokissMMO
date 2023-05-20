using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
   public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

    // 리스트를 갖고있는 딕셔너리를 만들어서 클래스하나를 통째로 관리할생각 
    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
    }

    // 로더가 모든 클래스를 수용해서 가능
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        // 리스트 변수선언만하면 알아서 클래스타고 from 제이슨으로  지정한 제이슨텍스트를 리스트에 집어넣음
        //Loader data = 
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
