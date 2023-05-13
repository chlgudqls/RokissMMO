using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    //Define.Scene _sceneType = Define.Scene.UnKnown;

    // get은 public  set은 protected 로 설정
    public Define.Scene SceneType { get; protected set; } = Define.Scene.UnKnown; // 이런식으로 기본값 설정이 가능
    void Start()
    {
        // @@@@@@@@@@@@@@@@부모에서 init해도 적용된다
        Init();
    }

    protected virtual void Init()
    {
        // 지정한 타입의 컴포넌트를 들고있는 오브젝트를 찾아냄
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    public abstract void Clear();
}
