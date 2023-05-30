using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum State
    {
        Die,
        Moving,
        Idle,
        Skill,
    }
public enum Layer
    {
        Monster = 8,
        Ground,
        Block,
    }
    // 씬에 대한 정의
    public enum Scene
    {
        UnKnown,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    // 유아이의 이벤트
    public enum UIEvent
    {
        Click,
        Drag,
    }
    // 마우스이벤트 press, click 타입 어떤 상황에 설정
    // 어떤 상황이면 누른채로 이동, 다른상황은 click한번 으로이동
    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }
    public enum CameraMode
    {
        QuaterView
    }
}
