using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// �ϴ� �����Ұ� ���⼭ �븮�ں����� �������� 
// �ٸ������� �����ް� ���� ������ ȣ���ϰ� Invoke���� �ѱ�°ſ��� �Ű������� Ÿ����
// invoke �� �ش��Լ�ȣ���̴ϱ� �ѱ�°� ���� �Ķ����
public class InputManager
{
    // �������� delegate ������ �����̶����
    public Action KeyAction = null;
    // ���׸��� enum�� ���±���
    public Action<Define.MouseEvent> MouseEvent = null;

    bool _pressed = false;
    public void OnUpdate()
    {
        // ���콺�� ���� �͵� ���º�ȭ�� �ν��Ѵ� ���� ���콺�� �߰��ϴµ� �׷��� �̰ɷ� �����ٰ��ϳ�
        //if (Input.anyKey == false)
        //    return;
        // UI Ŭ���ÿ� ����
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey && KeyAction != null)
            // ����� �Լ� ����
            KeyAction.Invoke();

        // update���� ��ӳ��̴ٰ� ����
        if(MouseEvent != null)  
        {
            if(Input.GetMouseButton(0))
            {
                // �����ؼ� �������ϴ°ǰ�  
                // �������� press ���⼭ ���� ����
                // �κ�ũ�� �Լ���°� �����ؾߵ� �Ű������ѱ����  
                MouseEvent.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            // ������Ȳ���� ���
            else
            {
                // ������Ȳ���� �������ϱ� true���ߵ�
                if(_pressed)
                    MouseEvent.Invoke(Define.MouseEvent.Click);

                _pressed = false;
            }
        }
    }
    // ��ǲ�� Ŭ��� �ʿ� ������ �ٸ����ִٰ��ϴµ� Ű�׼��̳� ���콺�׼���
    public void Clear()
    {
        KeyAction = null;
        MouseEvent = null;
    }
} 
