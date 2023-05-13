using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 드래그하기전에 눌렀을떄가 begin, 드래그하는중
public class UI_EventHandler : MonoBehaviour, IPointerClickHandler , IDragHandler
{
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnClickHandler = null;


    //EventSystems 를 통해서 사용할수있는것
    // 클릭이나 드래그로 호출되는 매커니즘을 갖고있는 내장함수 호출은 특정행동으로 해주기때문에 함수내용만 작성해서 넘기면 되는구조, invoke통해서 
    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            // 호출할때 인자값 전달하는 매커니즘
            OnClickHandler.Invoke(eventData);
    }

    //eventData이 드래그하는 위치값을 전달해줌
    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
    }


} 
