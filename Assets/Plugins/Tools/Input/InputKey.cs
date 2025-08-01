using System.Collections.Generic;
using UnityEngine;
 
namespace GameBase.Tools
{
    public enum KeyFunction
    {
        None,
        Up, Down, Left, Right,
        Stop, Aim,
        Attack, MoveTo,
        Interaction,
        Spell1, Spell2, Spell3, Spell4, Spell5, Spell6, Spell7,
        Blink,
        LockCamHeight, LockView,
        SettingPitch, SettingYawClockWise, SettingYawAntiClockWise,
        SettingYawClockWiseFaster, SettingYawAntiClockWiseFaster,
        ResetView,
        ToggleCmd, CmdConfirm, CmdDelete,
        Cancel, MouseConfirm,
        TestPosKey,
        ToggleAttrPanel,
        SummonEnemy, SummonAllies,
    }

    internal struct InputKey
    {
        public List<KeyCode> keyCodes;          // 触发功能的按键组合
        public List<KeyFunction> keyFuncs;   // 按键组合可触发的功能列表
    }
}
