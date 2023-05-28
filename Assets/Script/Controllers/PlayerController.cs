using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// 백터는 두 가지로만 사용됨
// 위치 백터 - 좌표 position  
// 방향 백터 - 회전 rotation  dir 값 normalized, 두 위치 사이의 거리  magnitude




public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill,
    }

    int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster);

    PlayerStat _stat;

    Vector3 _destPos;
 
    // 에니메이션과 상태 둘중하나를 뺴먹는 참사가 발생할수있다고함
    // 그래서 프로퍼티를 통해서 일대일 매칭을 시킴

    [SerializeField]
    PlayerState _state = PlayerState.Idle;

    GameObject _lockTarget;

    public PlayerState State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case PlayerState.Die:
                    //anim.SetBool("attack", false);
                    break;
                case PlayerState.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    //anim.Play("WAIT");
                    //anim.SetFloat("speed", 0);
                    //anim.SetBool("attack", false);
                    break;
                case PlayerState.Moving:
                    anim.CrossFade("RUN", 0.1f);
                    //anim.Play("RUN");
                    //anim.SetFloat("speed", _stat.MoveSpeed);
                    //anim.SetBool("attack", false);
                    break;
                case PlayerState.Skill:
                    anim.CrossFade("Attack", 0.1f, -1, 0);
                    //anim.Play("Attack");
                    //anim.SetBool("attack", true);
                    break;
            }
        }
    }


    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();
        // 저번엔 리스너패턴 이번엔 옵저버패턴
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

        // 그냥 스타트에서 UI소환하면되는거였네 프리팹으로 만들들고나서 소환코드
        //Managers.Resource.Instantiate("UI/UI_Button");

        //UI_Button ui = Managers.UI.ShowPopupUI<UI_Button>();

        //Managers.UI.ClosePopupUI(ui);

        //Managers.UI.ShowSceneUI<UI_Inven>();

    }

    
    void UpdateDie()
    {

    }
    void UpdateMoving()
    {
        // 이동중에
        // 몬스터가 내 사정거리보다 가까우면 공격
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            // 몬스터와 나 사이의 거리
            float distance = (_destPos - transform.position).magnitude;
            // 1 = 사정거리
            if (distance <= 1.5f)
            {
                State = PlayerState.Skill;
                // 더 이상 이동할 필요가 없으니 리턴
                return;
            }
        }
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();

            // 이동값이 저거임 _speed * Time.deltaTime 그대로가 아니라 프레임단위로 쪼개지는값이라서 이게 계속좁혀지다가 magnitude보다 작아져야됨
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);

            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if(!Input.GetMouseButton(0))
                    State = PlayerState.Idle;
                return;
            }

            //  이동뿐만아니라 바라보는 방향도 설정해줘야됨  
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            //transform.position += dir.normalized * moveDist;

        }

    }
    void UpdateIdle()
    {
    }

    void UpdateSkill()
    {
        // 방향지정을 안해줬었네 그래서 바라보고 떄리지않았던건가
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);

            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    // 유니티 애니메이션 이벤트에 function으로 바로 집어넣어서 사용
    // 원복하기 위한 함수 계속 false가 되기때문에? 끝나면 skill을 쓰는것이다
    void OnHitEvent()
    {
        Debug.Log("OnHitEvent");

        if (_stopSkill)
        {
            State = PlayerState.Idle;
        }
        else
        {
            // 포인터를 뗴야만 true가 되기때문에 누르고있으면 스킬이 무한반복이다
            State = PlayerState.Skill;
        }

        // 상태에 따라서 모션을 할수가있음
        //_state = PlayerState.Idle;

    }
    // 근데 이걸 update에서 하네
    void Update()
    {
        //UpdateMouseCursor();

        switch (State)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Skill:
                UpdateSkill();
                break;
        }
        // 조건 만족하면 여기서 실행하는느낌인가 
    }

    // 이동부분과 커서의 표현부분을 따로 둠
    //void UpdateMouseCursor()
    //{

    //}

    // 이 연산자의 의미는 뭐지
    // 어쨋거나 비트상으로 봤을때 01로구분하고 8번째9번째 비트가 0,1인지를 확인하는 듯

    // 아.. 대리자에서 <>안에 들어가는게 인자의 숫자였음 매개변수갯수
    bool _stopSkill = false;

    void OnMouseEvent(Define.MouseEvent evt)
    {
        //if (State == PlayerState.Die)
        //    return;
        // 매개변수로 이제 조리하는거임
        // 근데 클릭이 아니면 리턴
        //if (evt != Define.MouseEvent.Click)
        //    return;

        // 상태에 따라서 마우스이벤트가 달라야되니까
        switch (State)
        {
            // 이런식으로 나눠주면 스킬상태일땐 괜한 로직이 들어가지않는다
            case PlayerState.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Skill:
                {
                    // 스킬을 멈추게함 근데 눌렀을 때 스킬이 나가긴하는지가 의문
                    // 포인터업했을때 스킬을 멈춰주는것
                    if (evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;
        }
    }

    // 마우스 이벤트를 깔끔하게하려는 목적 
    // switch case를 엄청 사용하네
    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        //  그냥 ray를 가져올수가있음
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100, _mask);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    // LayerMask.GetMask("Ground") | LayerMask.GetMask("Monster")
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        // 현재 무빙 상태가 거쳐야만 스킬상태로 넘어갈 수있음 down이후엔 press만 호출되니까 무빙으로 가지않음
                        State = PlayerState.Moving;
                        _stopSkill = false;

                        //Debug.Log($"hit {hit.transform.gameObject.name}");
                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                        _destPos = hit.point;
                }
                break;
            // 마우스Up을 했을때 _lockTarget 을 널로 넣고 있기떄문에 문제발생 
            case Define.MouseEvent.PointerUp:
                //_lockTarget = null;
                _stopSkill = true;
                break;
        }
    }
}
