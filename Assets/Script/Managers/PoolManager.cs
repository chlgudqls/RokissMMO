using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    // 풀메니저가 최상위 그밑에 풀들을 갖고있기떄문에 클래스로 관리해주게됨
    class Pool
    #region Pool
    {
        // 이 변수들 갑자기 뭔진 모르겠음
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();

        // Root 를 새로만든후 네임을 정해주고 다른 함수 타고들어감 자식들만들어주는
        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
                Push(Create());
        }

        // 단지 메모리에 있던거 받아서 이름변경, 컴포넌트 던지는역할 
        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        // 던져주면 받아서 걔에 대한 설정해주고 자료구조에 저장(count만큼)
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
            // 자료구조에 있는 것 아니면 새로만들어서 하나 꺼내오게 만듦 그리고 활성화까지 시키는 작업까지
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);

            // parent가 널이라는거 자체가 돈디스트로이의 산하는 parent가 아니라는말
            if(parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            // 아마 새로만든건 따로 설정이 필요하기 떄문
            poolable.transform.parent = parent;
            poolable.IsUsing = true;

            return poolable;
        }
    }
        #endregion

    // 그리고 클래스들을 딕셔너리 형태로 물고있게 만들것
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;
    public void Init()
    {
        // 일단 자식들을 달아둘 부모
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

        // 여기서 최초로 딕셔너리에 추가했음
        _pool.Add(original.name, pool);
    }

    // 일단 정확히 어디에쓰는지는 잘모름
    // 스크립트로 구분할거라는데 처음해보는거
    // 풀객체는 스크립트가 달려있음
    public void Push(Poolable poolable)
    {
        // 이부분 뭐냐 다시생각@@@@@@@@@@@@
        string name = poolable.gameObject.name;
        if (!_pool.ContainsKey(name))
        {
            // 예외적인 경우라서 짐작하기힘듬
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (!_pool.ContainsKey(original.name))
            // 없으면 널리턴이 아니라 만들어줘야된다
            CreatePool(original);
        // 딕셔너리에 나중에 저장 할거고 일단은 매핑되어있다고 가정하고 미리 작업하는거임
        return _pool[original.name].Pop(parent);
    }

    //반환한다고함
    public GameObject GetOriginal(string name)
    {
        // 없을떄도 대비
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
