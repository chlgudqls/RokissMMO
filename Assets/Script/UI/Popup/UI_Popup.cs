using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    // start���� �ٷ� �ϴ°ͺ��� init �Լ��� ������ �Ѵٴµ�
    // start�� �ڽĿ��� �Ѱ����� �����ؾ� �����°Ͱ��� ������ 
    // �ᱹ �� ������Ʈ�� ����ϱ⶧���� ���⼭ setcanvas���� �ڽ��� �Ѱܼ� �����ϴ°���
    public override void Init()
    {
        // �˾��� �������Ѵ�
        Managers.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }
}

