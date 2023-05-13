using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    // �ֻ������� �� �Ʒ��ڽ��� ã�°���

    // recursive �̰ɷ� ��͸� �Ἥ �ڽ��� �ڽĵ� ã�������ִٰ���

    // ȣ���ϴ°Ű�
    // �̰� true�� ������ ����ϴ°ų� 
    // T�� ã����� ������Ʈ
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
            // �Ѵܰ�Ʒ��ڽ��̰� 
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                // �ش� �ڽĵ��� ������ŭ ���� ã���� ������Ʈ������
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
            // go�� �ڽ� ������Ʈ���� �ϴ� �����´� ����ҽ���
            // GetComponentsInChildren �̰� ��ü�� ����ε�
            // ��� �ڽ� �������°��ε�
            // ���ʿ� �̰� ����ص��Ǵµ� ��� �˻��ϳ���¼��
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                //name�� null�̰ų� empty�� �̰��ϳ� �����Ѵٴ°���  ã�����³����� �ȳѱ��쿣 �׳ɳѱ�
                // ������ ����Ѵٴ°ų� null�ε��� ù����ã����
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component; 
            }
        }


            return null;
    }
}
