using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �̰� � ������ Ŭ���������� �������� �ϰԵ�
public class ResourceManager
{
    // path�� �޾Ƽ� ���⼭ �޴� path�� ���� �̸� �������� �ϴ°��
    // object �� ��ӹ��� �͸� ����
    // ������ load 
    // T �� ������Ʈ�ΰ͸�  
    // 
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            // ���ϳ����ִ� / ���ִ� �ε����� ã�°Ű�����
            int index = name.LastIndexOf("/");
            if (index >= 0)
                // �̴������� ©�� ���
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path);
    }
    // load�ϸ� �װ�ξ��� data�� �������� ������ �ְ� object�� instantiate �Լ�ȣ��

    // �ϴܺ����־�� ��üũ�ϰ� ����εȰŸ� ����θ���
    public GameObject Instantiate(string path, Transform parent = null)
    {
        // �׳� string ���� ����/ �����̸� �������� ��
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        { 
            Debug.Log($"Failed to load Prefab : {path}");
            return null;
        }

        if(original.GetComponent<Poolable>() != null)


        // �����ջ����ɋ� clone�� ���ַ�����
        GameObject go = Object.Instantiate(original, parent);

        // prefab �� �������
        go.name = original.name;
        // ���ڿ��� ã�Ƽ� �� �ε����� ��ȯ
        //int index = go.name.IndexOf("(Clone)");
        // �߰��ߴ� // �̷������� �д¹���� �ٲ�ߵ�
        //if (index > 0)
        // ���ڿ��� ���ϴ� �ε��� �߶� �ٽú���
        //go.name = go.name.Substring(0, index);

        // �� ã�Ҵٸ�
        // ����Լ��� ���°ǰ� object �� instance �Լ��� ȣ�� �Ű������� ����
        // ��Ͱ� �ƴ϶� object�� �Լ�
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
