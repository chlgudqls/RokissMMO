using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// 백터는 두 가지로만 사용됨
// 위치 백터 - 좌표 position  
// 방향 백터 - 회전 rotation  dir 값 normalized, 두 위치 사이의 거리  magnitude




public class PlayerController : MonoBehaviour
{
    PlayerStat _stat;

    Vector3 _destPos;

    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();
        // 저번엔 리스너패턴 이번엔 옵저버패턴
        Managers.Input.MouseEvent -= OnMouseClicked;
        Managers.Input.MouseEvent += OnMouseClicked;

        // 그냥 스타트에서 UI소환하면되는거였네 프리팹으로 만들들고나서 소환코드
        //Managers.Resource.Instantiate("UI/UI_Button");

        //UI_Button ui = Managers.UI.ShowPopupUI<UI_Button>();

        //Managers.UI.ClosePopupUI(ui);

        //Managers.UI.ShowSceneUI<UI_Inven>();
    }

    enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill,
    }

    PlayerState _state = PlayerState.Idle;

    void UpdateDie()
    {

    }
    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            _state = PlayerState.Idle;
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
                _state = PlayerState.Idle;
                return;
            }

            //  이동뿐만아니라 바라보는 방향도 설정해줘야됨  
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
            //transform.position += dir.normalized * moveDist;

        }

        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _stat.MoveSpeed);
    }
    void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }


     // 근데 이걸 update에서 하네
    void Update()
    {
        switch (_state)
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
        }
        // 조건 만족하면 여기서 실행하는느낌인가 
    }

    // 이동부분과 커서의 표현부분을 따로 둠
    void UpdateMouseCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, _mask))
        {

            //Debug.Log($"hit {hit.transform.gameObject.name}");
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                Texture2D tex = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
            }
            else
            {
                Texture2D tex = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");

            }
        }
    }

    // 이 연산자의 의미는 뭐지
    // 어쨋거나 비트상으로 봤을때 01로구분하고 8번째9번째 비트가 0,1인지를 확인하는 듯
    int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster);

    // 아.. 대리자에서 <>안에 들어가는게 인자의 숫자였음 매개변수갯수
    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;
        // 매개변수로 이제 조리하는거임
        // 근데 클릭이 아니면 리턴
        //if (evt != Define.MouseEvent.Click)
        //    return;


        //  그냥 ray를 가져올수가있음
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        // LayerMask.GetMask("Ground") | LayerMask.GetMask("Monster")
        if (Physics.Raycast(ray , out hit, 100, _mask))
        {
            _destPos = hit.point;

            _state = PlayerState.Moving;

            //Debug.Log($"hit {hit.transform.gameObject.name}");
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                Debug.Log("Monster Click!");
            }
            else
            {
                Debug.Log("Ground Click!");
            }
        }
    }
}
