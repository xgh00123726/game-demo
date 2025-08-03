using GameBase.GCamera;
using GameBase.Move;
using GameBase.Projectile;
using GameBase.Spell;
using GameBase.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SysIniter : MonoBehaviour
{
    private void Awake()
    {
        var textSys = TextSys.Instance;
        var spellSys = SpellSys.Instance;
        var projectileSys = ProjectileSys.Instance;
        var rotateSys = RotateSys.Instance;
        var moveSys = MoveSys.Instance;
        var creatureSys = CreatureSys.Instance;
        var healthBarSys = HealthBarSys.Instance;
    }
}
