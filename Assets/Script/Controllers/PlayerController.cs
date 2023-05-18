using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// ���ʹ� �� �����θ� ����
// ��ġ ���� - ��ǥ position  
// ���� ���� - ȸ�� rotation  dir �� normalized, �� ��ġ ������ �Ÿ�  magnitude




public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    Vector3 _destPos;

    void Start()
    {
        // ������ ���������� �̹��� ����������
        Managers.Input.MouseEvent -= OnMouseClicked;
        Managers.Input.MouseEvent += OnMouseClicked;

        // �׳� ��ŸƮ���� UI��ȯ�ϸ�Ǵ°ſ��� ���������� �������� ��ȯ�ڵ�
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
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);

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
        anim.SetFloat("speed", _speed);
    }
    void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }


     // �ٵ� �̰� update���� �ϳ�
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
        // ���� �����ϸ� ���⼭ �����ϴ´����ΰ� 
    }

    // ��.. �븮�ڿ��� <>�ȿ� ���°� ������ ���ڿ��� �Ű���������
    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;
        // �Ű������� ���� �����ϴ°���
        // �ٵ� Ŭ���� �ƴϸ� ����
        //if (evt != Define.MouseEvent.Click)
        //    return;


        //  �׳� ray�� �����ü�������
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray , out hit, 100, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;

            _state = PlayerState.Moving;

            //Debug.Log($"hit {hit.transform.gameObject.name}");
        }
    }
}
