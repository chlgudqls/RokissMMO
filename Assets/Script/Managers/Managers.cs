using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;

    static Managers Instance { get { Init(); return s_instance; } }

    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();
    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
   
    void Start()
    {
        // �̰� �����ΰ� init ȣ���ϴ°��� ������ �̰� ȣ��Ǿ� �ƴѰ�
        Init();
    }

    void Update()
    {
        // �޴����� �Լ��� ����
        // ���� ȣ���ϴºκ��ε� ��ӵ��鼭 üũ��  input �� �ִ���   
        _input.OnUpdate();
        //Resource.Instantiate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("Managers");
            if(go == null)
            {
                go = new GameObject();
                go.name = "@Managers";
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            // Instance ����ϸ� ���ѷ��� ����
            s_instance._sound.Init();
        }
    }
    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
    }
}
