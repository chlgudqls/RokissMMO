using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// ���ʹ� �� �����θ� ����
// ��ġ ���� - ��ǥ position  
// ���� ���� - ȸ�� rotation  dir �� normalized, �� ��ġ ������ �Ÿ�  magnitude




public class PlayerController : BaseController
{
    int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster);

    PlayerStat _stat;
    bool _stopSkill = false;


    public override void Init()
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
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    
    //void UpdateDie()
    //{

    //}
    protected override void UpdateMoving()
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
                State = Define.State.Skill;
                // �� �̻� �̵��� �ʿ䰡 ������ ����
                return;
            }
        }
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
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
                    State = Define.State.Idle;
                return;
            }

            //  �̵��Ӹ��ƴ϶� �ٶ󺸴� ���⵵ ��������ߵ�  
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            //transform.position += dir.normalized * moveDist;

        }

    }
    //void UpdateIdle()
    //{
    //}

    protected override void UpdateSkill()
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
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();

            int damage = Mathf.Max(0, _stat.Attack - targetStat.Defence);

            Debug.Log(damage);

            targetStat.Hp -= damage;
         }
        //Debug.Log("OnHitEvent");

        if (_stopSkill)
        {
            State = Define.State.Idle;
        }
        else
        {
            // �����͸� ��߸� true�� �Ǳ⶧���� ������������ ��ų�� ���ѹݺ��̴�
            State = Define.State.Skill;
        }

        // ���¿� ���� ����� �Ҽ�������
        //_state = Define.State.Idle;

    }
    // �ٵ� �̰� update���� �ϳ�
    

    // �̵��κа� Ŀ���� ǥ���κ��� ���� ��
    //void UpdateMouseCursor()
    //{

    //}

    // �� �������� �ǹ̴� ����
    // ��¶�ų� ��Ʈ������ ������ 01�α����ϰ� 8��°9��° ��Ʈ�� 0,1������ Ȯ���ϴ� ��

    // ��.. �븮�ڿ��� <>�ȿ� ���°� ������ ���ڿ��� �Ű���������

    void OnMouseEvent(Define.MouseEvent evt)
    {
        //if (State == Define.State.Die)
        //    return;
        // �Ű������� ���� �����ϴ°���
        // �ٵ� Ŭ���� �ƴϸ� ����
        //if (evt != Define.MouseEvent.Click)
        //    return;

        // ���¿� ���� ���콺�̺�Ʈ�� �޶�ߵǴϱ�
        switch (State)
        {
            // �̷������� �����ָ� ��ų�����϶� ���� ������ �����ʴ´�
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
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
                        State = Define.State.Moving;
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
