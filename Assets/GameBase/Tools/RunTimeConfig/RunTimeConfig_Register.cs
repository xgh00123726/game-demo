using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tools
{
    public partial class RunTimeConfig
    {
        static RunTimeConfig()
        {
            CmdController.SetCmd("++", () =>
            {
                testCircle.r += 1f;
                Debug.Log($"set success, current test circle is -> c:{testCircle.c}, r:{testCircle.r}");
            });
            CmdController.SetCmd("--", () =>
            {
                testCircle.r -= 1f;
                Debug.Log($"set success, current test circle is -> c:{testCircle.c}, r:{testCircle.r}");
            });
        }
    }
}
