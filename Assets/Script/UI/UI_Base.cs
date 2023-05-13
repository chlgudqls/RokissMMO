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

    // ���ε带 ��� 
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        // type�� ��ϵ� �ϴ� ������
        string[] names = Enum.GetNames(type);
        // TŸ������ �������°Ű�

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        // �ٵ� �̷��� �ϴ��� ������ �ƴϷ��� �Ƹ³� �迭���� objects���� ���� ���� ����
        _objects.Add(typeof(T), objects);

        // ã�Ƽ� ������ ��� �ϴ°ǵ�
        for (int i = 0; i < names.Length; i++)
        {
            // gameobject �������� ����ٰ��� ���ӿ�����Ʈ��� 
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                // ���ӿ� �ش��ϴ°͵� ��θ� ã�ڴ� �����ؼ� ����ְڴ�
                // Util.FindChild<T> �긦 ȣ���ϱ⶧���� �꿡 �ɷ��ִ� ������ �ٿ���ߵȴ�
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind!{names[i]}");
        }
    }

    // �ε����� ��� �����ִ����� �𸣰����� �ƹ�ư
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        // ��ųʸ����� ������ ���������� �ְڴٴ°��� 
        // ���� T�ΰ� ��� �ƴ°���
        UnityEngine.Object[] objects = null;

        // ���±��� ���ε尡 ��ü�� ������ ã�Ƽ� �ִ°� �����̿����� ���⼭ ������ �ű⿡ text�� �����ϴ°� ���������� �Ǵ���
        // �ε����� enum�� int�� ����ų�
        // ������� �־����� enum�� �ε����� ����ϸ� �ش��ϴ� enum�� ���������°Ű� �̷��帧�� �˾ƾߵǴµ�

        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        // �ε����� ��� �˰� �������°���
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
