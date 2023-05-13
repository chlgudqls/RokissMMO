using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    // 바인드를 몇개를 
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        // type의 목록들 싹다 꺼내옴
        string[] names = Enum.GetNames(type);
        // T타입으로 가져오는거고

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        // 근데 이러면 일대일 매핑은 아니려나 아맞네 배열이지 objects에는 값이 전부 들어갔나
        _objects.Add(typeof(T), objects);

        // 찾아서 매핑은 어떻게 하는건데
        for (int i = 0; i < names.Length; i++)
        {
            // gameobject 전용으로 만든다고함 게임오브젝트라면 
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                // 네임에 해당하는것들 모두를 찾겠다 매핑해서 집어넣겠다
                // Util.FindChild<T> 얘를 호출하기때문에 얘에 걸려있는 조건을 붙여줘야된다
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind!{names[i]}");
        }
    }

    // 인덱스를 어떻게 보내주는지는 모르겠지만 아무튼
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        // 딕셔너리에서 꺼낸거 지역변수에 넣겠다는거임 
        // 같은 T인건 어떻게 아는거지
        UnityEngine.Object[] objects = null;

        // 여태까지 바인드가 객체의 정보를 찾아서 넣는게 목적이였으면 여기서 꺼내서 거기에 text를 수정하는게 정상적으로 되는지
        // 인덱스는 enum을 int로 만든거네
        // 순서대로 넣었으니 enum을 인덱스로 사용하면 해당하는 enum이 가져와지는거고 이런흐름을 알아야되는데

        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        // 인덱스는 어떻게 알고 가져오는거지
        return objects[idx] as T;
    }
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
   
    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);


        //UI_EventHandler evt = go.GetComponent<UI_EventHandler>();

        //if (evt == null)
        //    evt = go.AddComponent<UI_EventHandler>();

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;

            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }
    }
}
