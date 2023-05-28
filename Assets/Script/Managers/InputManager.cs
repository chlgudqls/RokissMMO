using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// 일단 생각할거 여기서 대리자변수를 생성했음 
// 다른곳에서 구독받고 조건 맞으면 호출하고 Invoke에서 넘기는거였음 매개변수로 타입을
// invoke 가 해당함수호출이니까 넘기는게 맞음 파라미터
public class InputManager
{
    // 전파해줄 delegate 리스너 패턴이라고함
    public Action KeyAction = null;
    // 제네릭에 enum도 들어가는구나
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;

    float _pressedTime = 0;
    public void OnUpdate()
    {
        // 마우스를 떼는 것도 상태변화로 인식한다 이제 마우스도 추가하는데 그래서 이걸로 씹힌다고하네
        //if (Input.anyKey == false)
        //    return;
        // UI 클릭시엔 리턴
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey && KeyAction != null)
            // 저장된 함수 실행
            KeyAction.Invoke();

        // update에서 계속널이다가 들어옴
        if(MouseAction != null)  
        {
            if(Input.GetMouseButton(0))
            {
                if (!_pressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    // 누른시간이 들어옴
                    _pressedTime = Time.time;
                }
                // 지정해서 실행을하는건가  
                // 눌렀을때 press 여기서 뭔가 보냄
                // 인보크가 함수라는걸 인지해야됨 매개변수넘긴거임  
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            // 누른상황빼고 모두
            else
            {
                // 누른상황에서 뗐을떄니까 true여야됨
                if (_pressed)
                {
                    // 0.2는 지난시간
                    if (Time.time < _pressedTime + 0.2f)
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    MouseAction.Invoke(Define.MouseEvent.PointerUp);
                }

                _pressed = false;
                _pressedTime = 0;
            }
        }
    }
    // 인풋도 클리어가 필요 씬마다 다를수있다고하는데 키액션이나 마우스액션이
    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
} 
