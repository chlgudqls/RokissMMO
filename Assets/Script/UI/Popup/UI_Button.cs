using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    // �� ������Ʈ�� ���� type�� Ű�� �������� ������ ��Ÿ������ �������ִٴ°ǰ�

    // �׷��� ��ư�� Ÿ���� ã�ƺ��� UnityEngine.Object[]���� ��ư�� ����Ʈ���� �� ����ִٰ���

    // �̰͵� enum�� �������
    // �ݹ����� ����°� ��ȿ�����̶������ �׷��� ���������Ҽ��ִ°� �ڵ�ε� �Ҽ������� �ڵ����� �Ѵٰ���
    // ������ �ϳ��ϳ� ������Ѽ� ����Ϸ��°ű���
    // �Ѱ��� �ҷ����°�

    // �ϳ��ϳ� �������� ���������ʰ�
    // �Լ��ȿ� �־ �ڵ�ȭ�� �Ѵٰ��� �Լ��� Ʋ�� �����ΰ� �������Ѱܼ� ���ϴ°� ã�ƿ��°Ű�����
    enum Buttons
    {
        // �̰͵��� string���� ����ҿ���
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


        Get<Text>((int)Texts.ScoreText).text = $"���� : {_score} �� ";
    }

    public override void Init()
    {
        base.Init();

        // Ÿ���� �ѱ�°ǰ� ó������ enum Ÿ���� �ϴ� �ѱ�� � ó���� �ϴ°ǰ�����
        // typeof � Ÿ���� typeof�� �ѱ�� Type�� Ÿ������ �����ϴµ���
        // �׳� �ѱ�� enum Ÿ���̰���
        // enum�߿����� ������ �ϱ����ؼ� �̷������� �ѱ�µ���
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));


        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnbuttonClicked);

        // ���� ��ũ��Ʈ�� �������ؼ� ���ӿ�����Ʈ�� �����ѰŶ����
        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        // ������������ �Ѱܼ� ��������Ʈ�ϸ� ���������� ���⼭�� �����ѵ�
        //UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }
}
