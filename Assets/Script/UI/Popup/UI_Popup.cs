using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    // start에서 바로 하는것보다 init 함수로 관리를 한다는데
    // start를 자식에서 한곳에서 관리해야 먹히는것같은 느낌임 
    // 결국 이 컴포넌트를 사용하기때문에 여기서 setcanvas한후 자신을 넘겨서 솔팅하는거임
    public override void Init()
    {
        // 팝업은 솔팅을한다
        Managers.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }
}

