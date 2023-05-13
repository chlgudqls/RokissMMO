using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    //Define.Scene _sceneType = Define.Scene.UnKnown;

    // get�� public  set�� protected �� ����
    public Define.Scene SceneType { get; protected set; } = Define.Scene.UnKnown; // �̷������� �⺻�� ������ ����
    void Start()
    {
        // @@@@@@@@@@@@@@@@�θ𿡼� init�ص� ����ȴ�
        Init();
    }

    protected virtual void Init()
    {
        // ������ Ÿ���� ������Ʈ�� ����ִ� ������Ʈ�� ã�Ƴ�
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    public abstract void Clear();
}
