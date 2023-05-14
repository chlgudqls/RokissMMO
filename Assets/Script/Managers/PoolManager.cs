using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    // Ǯ�޴����� �ֻ��� �׹ؿ� Ǯ���� �����ֱ⋚���� Ŭ������ �������ְԵ�
    class Pool
    #region Pool
    {
        // �� ������ ���ڱ� ���� �𸣰���
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();

        // Root �� ���θ����� ������ �����ְ� �ٸ� �Լ� Ÿ��� �ڽĵ鸸����ִ�
        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
                Push(Create());
        }

        // ���� �޸𸮿� �ִ��� �޾Ƽ� �̸�����, ������Ʈ �����¿��� 
        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        // �����ָ� �޾Ƽ� �¿� ���� �������ְ� �ڷᱸ���� ����(count��ŭ)
        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            _poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            // �ڷᱸ���� �ִ� �� �ƴϸ� ���θ��� �ϳ� �������� ���� �׸��� Ȱ��ȭ���� ��Ű�� �۾�����
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);

            // parent�� ���̶�°� ��ü�� ����Ʈ������ ���ϴ� parent�� �ƴ϶�¸�
            if(parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            // �Ƹ� ���θ���� ���� ������ �ʿ��ϱ� ����
            poolable.transform.parent = parent;
            poolable.IsUsing = true;

            return poolable;
        }
    }
        #endregion

    // �׸��� Ŭ�������� ��ųʸ� ���·� �����ְ� �����
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;
    public void Init()
    {
        // �ϴ� �ڽĵ��� �޾Ƶ� �θ�
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;

        // ���⼭ ���ʷ� ��ųʸ��� �߰�����
        _pool.Add(original.name, pool);
    }

    // �ϴ� ��Ȯ�� ��𿡾������� �߸�
    // ��ũ��Ʈ�� �����ҰŶ�µ� ó���غ��°�
    // Ǯ��ü�� ��ũ��Ʈ�� �޷�����
    public void Push(Poolable poolable)
    {
        // �̺κ� ���� �ٽû���@@@@@@@@@@@@
        string name = poolable.gameObject.name;
        if (!_pool.ContainsKey(name))
        {
            // �������� ���� �����ϱ�����
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (!_pool.ContainsKey(original.name))
            // ������ �θ����� �ƴ϶� �������ߵȴ�
            CreatePool(original);
        // ��ųʸ��� ���߿� ���� �ҰŰ� �ϴ��� ���εǾ��ִٰ� �����ϰ� �̸� �۾��ϴ°���
        return _pool[original.name].Pop(parent);
    }

    //��ȯ�Ѵٰ���
    public GameObject GetOriginal(string name)
    {
        // �������� ���
        if (!_pool.ContainsKey(name))
            return null;

        return _pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }
}
