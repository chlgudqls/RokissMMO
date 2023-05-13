using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 이게 어떤 목적의 클래스인지를 생각부터 하게됨
public class ResourceManager
{
    // path를 받아서 여기서 받는 path는 폴더 이름 까지포함 하는경로
    // object 를 상속받은 것만 받음
    // 받으면 load 
    // T 가 오브젝트인것만  
    // 
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            // 제일끝에있는 / 가있는 인덱스를 찾는거같은데
            int index = name.LastIndexOf("/");
            if (index >= 0)
                // 이다음부터 짤라서 출력
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path);
    }
    // load하면 그경로안의 data를 가져오고 변수에 넣고 object의 instantiate 함수호출

    // 일단변수넣어보고 널체크하고 제대로된거면 제대로리턴
    public GameObject Instantiate(string path, Transform parent = null)
    {
        // 그냥 string 으로 폴더/ 파일이름 가져오면 됨
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        { 
            Debug.Log($"Failed to load Prefab : {path}");
            return null;
        }

        if(original.GetComponent<Poolable>() != null)


        // 프리팹생성될떄 clone을 없애려고함
        GameObject go = Object.Instantiate(original, parent);

        // prefab 을 원본취급
        go.name = original.name;
        // 문자열을 찾아서 그 인덱스를 반환
        //int index = go.name.IndexOf("(Clone)");
        // 발견했다 // 이런식으로 읽는방법을 바꿔야됨
        //if (index > 0)
        // 문자열의 원하는 인덱스 잘라서 다시붙임
        //go.name = go.name.Substring(0, index);

        // 잘 찾았다면
        // 재귀함수를 쓰는건가 object 의 instance 함수를 호출 매개변수는 같음
        // 재귀가 아니라 object의 함수
        //return Object.Instantiate(prefab, parent);
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}
