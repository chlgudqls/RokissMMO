using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        for (int i = 0; i < 10; i++)
            Managers.Resource.Instantiate("unitychan");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 이게 다 string 대신에 enum사용하려고 했던건가
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }
    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }

}
