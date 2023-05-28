using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// ���ʹ� �� �����θ� ����
// ��ġ ���� - ��ǥ position  
// ���� ���� - ȸ�� rotation  dir �� normalized, �� ��ġ ������ �Ÿ�  magnitude




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
 
    // ���ϸ��̼ǰ� ���� �����ϳ��� ���Դ� ���簡 �߻��Ҽ��ִٰ���
    // �׷��� ������Ƽ�� ���ؼ� �ϴ��� ��Ī�� ��Ŵ

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
        // ������ ���������� �̹��� ����������
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

        // �׳� ��ŸƮ���� UI��ȯ�ϸ�Ǵ°ſ��� ���������� �������� ��ȯ�ڵ�
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
        // �̵��߿�
        // ���Ͱ� �� �����Ÿ����� ������ ����
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            // ���Ϳ� �� ������ �Ÿ�
            float distance = (_destPos - transform.position).magnitude;
            // 1 = �����Ÿ�
            if (distance <= 1.5f)
            {
                State = PlayerState.Skill;
                // �� �̻� �̵��� �ʿ䰡 ������ ����
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

            // �̵����� ������ _speed * Time.deltaTime �״�ΰ� �ƴ϶� �����Ӵ����� �ɰ����°��̶� �̰� ����������ٰ� magnitude���� �۾����ߵ�
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);

            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if(!Input.GetMouseButton(0))
                    State = PlayerState.Idle;
                return;
            }

            //  �̵��Ӹ��ƴ϶� �ٶ󺸴� ���⵵ ��������ߵ�  
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            //transform.position += dir.normalized * moveDist;

        }

    }
    void UpdateIdle()
    {
    }

    void UpdateSkill()
    {
        // ���������� ��������� �׷��� �ٶ󺸰� �������ʾҴ��ǰ�
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);

            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    // ����Ƽ �ִϸ��̼� �̺�Ʈ�� function���� �ٷ� ����־ ���
    // �����ϱ� ���� �Լ� ��� false�� �Ǳ⶧����? ������ skill�� ���°��̴�
    void OnHitEvent()
    {
        Debug.Log("OnHitEvent");

        if (_stopSkill)
        {
            State = PlayerState.Idle;
        }
        else
        {
            // �����͸� ��߸� true�� �Ǳ⶧���� ������������ ��ų�� ���ѹݺ��̴�
            State = PlayerState.Skill;
        }

        // ���¿� ���� ����� �Ҽ�������
        //_state = PlayerState.Idle;

    }
    // �ٵ� �̰� update���� �ϳ�
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
        // ���� �����ϸ� ���⼭ �����ϴ´����ΰ� 
    }

    // �̵��κа� Ŀ���� ǥ���κ��� ���� ��
    //void UpdateMouseCursor()
    //{

    //}

    // �� �������� �ǹ̴� ����
    // ��¶�ų� ��Ʈ������ ������ 01�α����ϰ� 8��°9��° ��Ʈ�� 0,1������ Ȯ���ϴ� ��

    // ��.. �븮�ڿ��� <>�ȿ� ���°� ������ ���ڿ��� �Ű���������
    bool _stopSkill = false;

    void OnMouseEvent(Define.MouseEvent evt)
    {
        //if (State == PlayerState.Die)
        //    return;
        // �Ű������� ���� �����ϴ°���
        // �ٵ� Ŭ���� �ƴϸ� ����
        //if (evt != Define.MouseEvent.Click)
        //    return;

        // ���¿� ���� ���콺�̺�Ʈ�� �޶�ߵǴϱ�
        switch (State)
        {
            // �̷������� �����ָ� ��ų�����϶� ���� ������ �����ʴ´�
            case PlayerState.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Skill:
                {
                    // ��ų�� ���߰��� �ٵ� ������ �� ��ų�� �������ϴ����� �ǹ�
                    // �����;������� ��ų�� �����ִ°�
                    if (evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;
        }
    }

    // ���콺 �̺�Ʈ�� ����ϰ��Ϸ��� ���� 
    // switch case�� ��û ����ϳ�
    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
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
                        // ���� ���� ���°� ���ľ߸� ��ų���·� �Ѿ ������ down���Ŀ� press�� ȣ��Ǵϱ� �������� ��������
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
            // ���콺Up�� ������ _lockTarget �� �η� �ְ� �ֱ⋚���� �����߻� 
            case Define.MouseEvent.PointerUp:
                //_lockTarget = null;
                _stopSkill = true;
                break;
        }
    }
}
