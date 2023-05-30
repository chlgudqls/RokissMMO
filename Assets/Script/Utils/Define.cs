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
    // ���� ���� ����
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

    // �������� �̺�Ʈ
    public enum UIEvent
    {
        Click,
        Drag,
    }
    // ���콺�̺�Ʈ press, click Ÿ�� � ��Ȳ�� ����
    // � ��Ȳ�̸� ����ä�� �̵�, �ٸ���Ȳ�� click�ѹ� �����̵�
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
