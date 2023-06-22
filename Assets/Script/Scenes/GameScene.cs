using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    //class Test
    //{
    //    public int id = 0;
    //}

    //class CoroutineTest : IEnumerable
    //{
    //    public IEnumerator GetEnumerator()
    //    {
    //        // foreach�� �̰� ȣ��ε� ���±��� �ű��ϳ�@@@@@@@@@@@@ ������ �Լ��� ȣ�����ִ� ������ִٴ°�
    //        yield return new Test() { id = 1 };
    //        yield return new Test() { id = 2 };
    //        yield return new Test() { id = 3 };
    //        yield return new Test() { id = 4 };
    //    }
    //}

    //Coroutine co;
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        //for (int i = 0; i < 5; i++)
        //    Managers.Resource.Instantiate("unitychan");

        //CoroutineTest test = new CoroutineTest();
        //// var�� ���콺��� ����Ÿ������ �˼�����@@
        //foreach (var t in test)
        //{
        //    Test e = t as Test;
        //    Debug.Log(e.id);
        //}
        //co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
        //StartCoroutine("CoStopExplode", 2.0f);
        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "unitychan");

        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        //Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");

        GameObject go = new GameObject() { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);
    }
    //IEnumerator CoStopExplode(float seconds)
    //{
    //    Debug.Log("Stop Enter");
    //    yield return new WaitForSeconds(seconds);
    //    Debug.Log("Stop Excute!!!");
    //    if (co != null)
    //    {
    //        StopCoroutine(co);
    //        co = null;
    //    }
    //}

    //IEnumerator ExplodeAfterSeconds(float seconds)
    //{
    //    Debug.Log("Explode Enter");
    //    yield return new WaitForSeconds(seconds);
    //    Debug.Log("Explode Excute!!!");
    //    co = null;
    //}

    public override void Clear()
    {
    }

 

  
}
