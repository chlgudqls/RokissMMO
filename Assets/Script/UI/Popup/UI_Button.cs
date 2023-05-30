using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    // 저 오브젝트가 뭔데 type이 키고 유아이의 정보를 저타입으로 가질수있다는건가

    // 그래서 버튼의 타입을 찾아보면 UnityEngine.Object[]에는 버튼의 리스트들을 다 들고있다고함

    // 이것도 enum을 사용했음
    // 콜백으로 만드는게 비효율적이라고했음 그래서 엔진으로할수있는건 코드로도 할수있으니 코딩으로 한다고함
    // 변수에 하나하나 저장시켜서 사용하려는거구나
    // 한개씩 불러오는거

    // 하나하나 수동으로 매핑하지않고
    // 함수안에 넣어서 자동화를 한다고함 함수로 틀을 만들어두고 변수만넘겨서 원하는값 찾아오는거같은데
    enum Buttons
    {
        // 이것들을 string으로 사용할예정
        PointButton,
    }
    enum Texts
    {
        PointText,
        ScoreText,
    }
    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        ItemIcon,
    }
    //private void Start()
    //{
    //    Init();
    //}

   
    int _score = 0;
    public void OnbuttonClicked(PointerEventData data)
    {
        _score++;


        Get<Text>((int)Texts.ScoreText).text = $"점수 : {_score} 점 ";
    }

    public override void Init()
    {
        base.Init();

        // 타입을 넘기는건가 처음부터 enum 타입을 일단 넘기고 어떤 처리를 하는건가본데
        // typeof 어떤 타입의 typeof를 넘기면 Type의 타입으로 전달하는듯함
        // 그냥 넘기면 enum 타입이겠지
        // enum중에서도 구별을 하기위해서 이런식으로 넘기는듯함
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));


        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnbuttonClicked);

        // 얘의 스크립트를 쓰기위해서 게임오브젝트에 접근한거라고함
        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        // 무슨이유인지 넘겨서 겟컴포넌트하면 못가져오네 여기서는 가능한데
        //UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }
}
