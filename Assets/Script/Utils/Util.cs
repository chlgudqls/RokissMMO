using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    // 최상위에서 그 아래자식을 찾는건지

    // recursive 이걸로 재귀를 써서 자식의 자식도 찾을수가있다고함

    // 호출하는거고
    // 이걸 true로 받으면 재귀하는거네 
    // T는 찾고싶은 컴포넌트
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();

        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }
    //public static T GetOrAddComponents<T>(GameObject go) where T : UI_EventHandler
    //{
    //    T component = go.GetComponent<T>();
    //    //if (component = null)
    //    //    component = go.AddComponent<UI_EventHandler>();

    //    return component;
    //}
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);

        if (transform == null)
            return null;

        return transform.gameObject;
    }


    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;


        if (!recursive)
        {
            // 한단계아래자식이고 
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                // 해당 자식들의 갯수만큼 돌고 찾으면 컴포넌트가져옴
                if(string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            // go의 자식 컴포넌트들을 싹다 가져온다 재귀할시임
            // GetComponentsInChildren 이거 자체가 재귀인듯
            // 모든 자식 가져오는거인듯
            // 애초에 이거 사용해도되는데 모두 검사하나어쩌나
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                //name이 null이거나 empty면 이거하나 리턴한다는거지  찾으려는네임을 안넘긴경우엔 그냥넘김
                // 뭔가를 뱉긴한다는거네 null인데도 첫번쨰찾은걸
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component; 
            }
        }


            return null;
    }
}
