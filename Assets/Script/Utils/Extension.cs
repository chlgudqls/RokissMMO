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

    // 확장해줄함수를 여기에 넣어주고 어떤식으로 확장시켜줄거라고함 게임오브젝트를 
    // 그냥여기로 넘겨주는것뿐이고 확장할수있는 그냥 발판정도인가 이 static 클래스를 사용함으로써 콜백을 확장할수있다는건지
    public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(go, action, type);
    }
    
    public static bool IsValid(this GameObject go)
    {
        return go != null && go.activeSelf;
    }
}
