using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    public override void Init()
    {
        // 씬은 솔팅이없다
        Managers.UI.SetCanvas(gameObject, false);
    }
}
