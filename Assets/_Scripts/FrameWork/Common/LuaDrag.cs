using UnityEngine;
using System.Collections;

namespace MyFrameWork
{
    public class LuaDrag : Base
    {

        protected void OnDragEnd()
        {
            Util.CallMethod("UIMainCtrl", "OnDragEnd");
        }
    }
}
