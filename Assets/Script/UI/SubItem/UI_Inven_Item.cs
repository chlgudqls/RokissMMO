using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
    // � �������� �����ϴϱ� �׳� gameobject�� ��� ����Ѵٰ���
    enum GameObjects
    {
        // �ش� �̸� ����
        ItemIcon,
        ItemNameText,
    }

    string _name;

    //void Start()
    //{
    //    Init();
    //}
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        // ���ӿ�����Ʈ���� text�� �����ؾߵǴϱ� �׷��ſ���
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

        // Ÿ�� ���� �Ƚ��൵�Ǵ°� ���� ȣ���̴ϱ� �ᵵ�ǰ� �Ƚᵵ �Ǵ°�
        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => { Debug.Log( $"������ Ŭ��! {_name}"); });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
