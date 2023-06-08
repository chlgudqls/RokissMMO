using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ������� ���ΰ� �߿��Ѱ͵��� Manager�� �����ļ� ����Ѵٰ���
public class SceneManagerEx
{
    // BaseScene�� �� ã�°��� �ǹ����̳�
    // �̰� ã���� ��� �ϳ�ã�°Ŵ� �̰ǰ� �����
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }
    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type)
    {
        // �ѹ��ߴ��ǵ� �ٽú���
        // �ǹ��� type��ü�� name �� �ƴѰ�
        // enum �� string ���� �������� ������ ���°ų�
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }
    public void Clear()
    {
        CurrentScene.Clear();

    }
}
