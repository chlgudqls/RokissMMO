using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// ���ʹ� �� �����θ� ����
// ��ġ ���� - ��ǥ position  
// ���� ���� - ȸ�� rotation  dir �� normalized, �� ��ġ ������ �Ÿ�  magnitude




public class PlayerController : MonoBehaviour
{
    PlayerStat _stat;

    Vector3 _destPos;

    Texture2D _attackIcon;
    Texture2D _handIcon;

    enum CursorType
    {
        None,
        Attack,
        Hand,
    }

    CursorType _cursorType = CursorType.None;

    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();
        // ������ ���������� �̹��� ����������
        Managers.Input.MouseEvent -= OnMouseEvent;
        Managers.Input.MouseEvent += OnMouseEvent;

        // �׳� ��ŸƮ���� UI��ȯ�ϸ�Ǵ°ſ��� ���������� �������� ��ȯ�ڵ�
        //Managers.Resource.Instantiate("UI/UI_Button");

        //UI_Button ui = Managers.UI.ShowPopupUI<UI_Button>();

        //Managers.UI.ClosePopupUI(ui);

        //Managers.UI.ShowSceneUI<UI_Inven>();
        _attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");

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

            // �̵����� ������ _speed * Time.deltaTime �״�ΰ� �ƴ϶� �����Ӵ����� �ɰ����°��̶� �̰� ����������ٰ� magnitude���� �۾����ߵ�
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);

            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                _state = PlayerState.Idle;
                return;
            }

            //  �̵��Ӹ��ƴ϶� �ٶ󺸴� ���⵵ ��������ߵ�  
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


     // �ٵ� �̰� update���� �ϳ�
    void Update()
    {
        UpdateMouseCursor();

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
        // ���� �����ϸ� ���⼭ �����ϴ´����ΰ� 
    }

    // �̵��κа� Ŀ���� ǥ���κ��� ���� ��
    void UpdateMouseCursor()
    {
        if (Input.GetMouseButton(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, _mask))
        {

            //Debug.Log($"hit {hit.transform.gameObject.name}");
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }
            }
            else
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3, 0), CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
            }
        }
    }

    // �� �������� �ǹ̴� ����
    // ��¶�ų� ��Ʈ������ ������ 01�α����ϰ� 8��°9��° ��Ʈ�� 0,1������ Ȯ���ϴ� ��
    int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster);

    GameObject _lockTarget;

    // ��.. �븮�ڿ��� <>�ȿ� ���°� ������ ���ڿ��� �Ű���������
    void OnMouseEvent(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;
        // �Ű������� ���� �����ϴ°���
        // �ٵ� Ŭ���� �ƴϸ� ����
        //if (evt != Define.MouseEvent.Click)
        //    return;

        RaycastHit hit;
        //  �׳� ray�� �����ü�������
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
                        _state = PlayerState.Moving;

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
                if (_lockTarget != null)
                    _destPos = _lockTarget.transform.position;

                else if (raycastHit)
                        _destPos = hit.point;
                    break;
                }
            case Define.MouseEvent.PointerUp:
                _lockTarget = null;
                break;

        }
           
  
    }
}
