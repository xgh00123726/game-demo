using GameBase.Creatures;
using GameBase.GCamera;
using GameBase.Projectiles;
using GameBase.Tools;
using Instance.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Analytics;
public class Initer : MonoBehaviour
{
    void Start()
    {

        var builder = new BehaviorTreeBuilder();

        builder.Repeat(3)
                    .Sequence()
                        .Log("this is test for behavior tree builder")
                    .Back()
                .End();
        builder.Tree.Tick();

        Physics.gravity = new Vector3(0, -100, 0);

        Command.Register("generate-enermy", () =>
        {
            var c = Constructor.Creatures.Factory.Instance.Get(Constructor.Creatures.Type.Common, 1);
            c.Position = CameraSys.MouseHitPosition;
        });

        Command.Register("generate-enermy", (int id) =>
        {
            var c = Constructor.Creatures.Factory.Instance.Get(Constructor.Creatures.Type.Common, 1);
            c.Position = CameraSys.MouseHitPosition;
        });

        Command.Register("generate-many-enermy", (int num) =>
        {
            for (int i = 0; i < num; i++)
            {
                var c = Constructor.Creatures.Factory.Instance.Get(Constructor.Creatures.Type.Common, 1);
                c.Position = CameraSys.MouseHitPosition + new Vector3(i, 0, 0);
            }
        });

        Command.Register("remove-all-commonCreature", () =>
        {
            CreatureSys.Instance.RemoveAll(GameBase.Creatures.Tag.CommonCreature);
        });

        var charater = Constructor.Creatures.Factory.Instance.Get(Constructor.Creatures.Type.Common, 0);
        var player = charater.Obj.AddComponent<Player>();
        player.charater = charater;
        charater.Position = new Vector3(-6, -7, 4);

        ProjectileGizmos.Instance.ToggleShow();
        for (int i = 0; i < 10; ++i)
        {
            var e = InventoryUI.Instance;
        }
    }

    private void Update()
    {
        if (Inputs.GetKeyDown(KeyFunction.ToggleAttrPanel))
        {
            InventoryUI.Instance.Toggle();
        }
    }
}
