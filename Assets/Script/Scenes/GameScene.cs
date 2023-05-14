using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    class Test
    {
        public int id = 0;
    }

    class CoroutineTest : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            // foreach가 이걸 호출로도 보는구나 신기하네@@@@@@@@@@@@ 산하의 함수도 호출해주는 기능이있다는것
            yield return new Test() { id = 1 };
            yield return new Test() { id = 2 };
            yield return new Test() { id = 3 };
            yield return new Test() { id = 4 };
        }
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        //for (int i = 0; i < 5; i++)
        //    Managers.Resource.Instantiate("unitychan");

        CoroutineTest test = new CoroutineTest();
        // var에 마우스대면 무슨타입인지 알수있음@@
        foreach (var t in test)
        {
            Test e = t as Test;
            Debug.Log(e.id);
        }
    }
    public override void Clear()
    {
    }

 

  
}
