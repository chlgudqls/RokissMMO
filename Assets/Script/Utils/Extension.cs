using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        return Util.GetOrAddComponent<T>(go);
    }

    // Ȯ�������Լ��� ���⿡ �־��ְ� ������� Ȯ������ٰŶ���� ���ӿ�����Ʈ�� 
    // �׳ɿ���� �Ѱ��ִ°ͻ��̰� Ȯ���Ҽ��ִ� �׳� ���������ΰ� �� static Ŭ������ ��������ν� �ݹ��� Ȯ���Ҽ��ִٴ°���
    public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(go, action, type);
    }
    
    public static bool IsValid(this GameObject go)
    {
        return go != null && go.activeSelf;
    }
}
