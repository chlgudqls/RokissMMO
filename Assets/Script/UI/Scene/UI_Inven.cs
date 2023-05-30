using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 씬을 상속받는 오브젝트하나를 관리하기위한 스크립트 
public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }
    //void Start()
    //{
    //    Init();
    //}

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < 8; i++)
        {
            //GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item");
            // = null 이런게 있으면 힌트도 만들수있나봄
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(parent: gridPanel.transform).gameObject;

            //item.transform.SetParent(gridPanel.transform);

            // 익스텐션 사용 살짝 헷갈림 생략을 하는 이점이있는데 
            UI_Inven_Item invenInfo = item.GetOrAddComponent<UI_Inven_Item>();

            invenInfo.SetInfo($"집행검{i}번");
        }
    }

}
