using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
    // 어떤 이유인지 간단하니까 그냥 gameobject로 묶어서 사용한다고함
    enum GameObjects
    {
        // 해당 이름 매핑
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

        // 게임오브젝트에서 text에 접근해야되니까 그런거였음
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

        // 타입 굳이 안써줘도되는거 였지 호출이니까 써도되고 안써도 되는것
        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => { Debug.Log( $"아이템 클릭! {_name}"); });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
