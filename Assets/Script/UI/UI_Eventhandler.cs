using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// �巡���ϱ����� ���������� begin, �巡���ϴ���
public class UI_EventHandler : MonoBehaviour, IPointerClickHandler , IDragHandler
{
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnClickHandler = null;


    //EventSystems �� ���ؼ� ����Ҽ��ִ°�
    // Ŭ���̳� �巡�׷� ȣ��Ǵ� ��Ŀ������ �����ִ� �����Լ� ȣ���� Ư���ൿ���� ���ֱ⶧���� �Լ����븸 �ۼ��ؼ� �ѱ�� �Ǵ±���, invoke���ؼ� 
    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            // ȣ���Ҷ� ���ڰ� �����ϴ� ��Ŀ����
            OnClickHandler.Invoke(eventData);
    }

    //eventData�� �巡���ϴ� ��ġ���� ��������
    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
    }


} 
