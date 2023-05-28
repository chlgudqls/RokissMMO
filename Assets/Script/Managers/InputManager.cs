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
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;

    float _pressedTime = 0;
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
        if(MouseAction != null)  
        {
            if(Input.GetMouseButton(0))
            {
                if (!_pressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    // �����ð��� ����
                    _pressedTime = Time.time;
                }
                // �����ؼ� �������ϴ°ǰ�  
                // �������� press ���⼭ ���� ����
                // �κ�ũ�� �Լ���°� �����ؾߵ� �Ű������ѱ����  
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            // ������Ȳ���� ���
            else
            {
                // ������Ȳ���� �������ϱ� true���ߵ�
                if (_pressed)
                {
                    // 0.2�� �����ð�
                    if (Time.time < _pressedTime + 0.2f)
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    MouseAction.Invoke(Define.MouseEvent.PointerUp);
                }

                _pressed = false;
                _pressedTime = 0;
            }
        }
    }
    // ��ǲ�� Ŭ��� �ʿ� ������ �ٸ����ִٰ��ϴµ� Ű�׼��̳� ���콺�׼���
    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
} 
