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
   public Dictionary<int, Stat> StatDict { get; private set; } = new Dictionary<int, Stat>();

    // ����Ʈ�� �����ִ� ��ųʸ��� ���� Ŭ�����ϳ��� ��°�� �����һ��� 
    public void Init()
    {
        StatDict = LoadJson<StatData, int, Stat>("StatData").MakeDict();
    }

    // �δ��� ��� Ŭ������ �����ؼ� ����
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        // ����Ʈ ���������ϸ� �˾Ƽ� Ŭ����Ÿ�� from ���̽�����  ������ ���̽��ؽ�Ʈ�� ����Ʈ�� �������
        //Loader data = 
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
