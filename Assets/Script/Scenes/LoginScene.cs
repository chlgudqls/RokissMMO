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
            // �̰� �� string ��ſ� enum����Ϸ��� �ߴ��ǰ�
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }
    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }

}
