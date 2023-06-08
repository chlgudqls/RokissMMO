using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 선봉대는 놔두고 중요한것들은 Manager를 따로파서 사용한다고함
public class SceneManagerEx
{
    // BaseScene을 왜 찾는거지 의문점이네
    // 이걸 찾으면 어쩃든 하나찾는거다 이건가 현재씬
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }
    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type)
    {
        // 한번했던건데 다시보기
        // 의문점 type자체가 name 이 아닌가
        // enum 을 string 으로 가져오기 때문에 쓰는거네
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }
    public void Clear()
    {
        CurrentScene.Clear();

    }
}
