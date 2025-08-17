using GameBase.Creatures;
using GameBase.Flyings;
using GameBase.GCamera;
using GameBase.Instance;
using GameBase.Math;
using GameBase.Spells;
using GameBase.Tools;
using Instance.Creatures;
using System;
using UnityEngine;
using UnityEngine.TextCore.Text;
using XLua;
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
            var c = CreatureSys.Instance.NewEntity<GameCreature>(0);
            c.genPos = CameraSys.MouseHitPosition;
            c.ObjID = 0;
        });

        Command.Register("generate-enermy", (int id) =>
        {
            var c = CreatureSys.Instance.NewEntity<GameCreature>(0);
            c.genPos = CameraSys.MouseHitPosition;
            c.ObjID = id;
        });

        Command.Register("generate-many-enermy", (int num) =>
        {
            for (int i = 0; i < num; i++)
            {
                var c = CreatureSys.Instance.NewEntity<GameCreature>(0);
                c.genPos = CameraSys.MouseHitPosition + new Vector3(i, 0, 0);
                c.ObjID = 1;
            }
        });

        Command.Register("remove-all-commonCreature", () =>
        {
            CreatureSys.Instance.RemoveAll(GameBase.Creatures.Tag.CommonCreature);
        });

        Timer.AddTask(1, () =>
        {
            var charater = CreatureSys.Instance.NewEntity<GameCreature>(0);
            charater.ObjID = 27;
            charater.tag = GameBase.Creatures.Tag.Player;
            charater.AfterInstantiateObj += () =>
            {
                var player = charater.Obj.AddComponent<Player>();
                player.charater = charater;
                player.transform.position = new Vector3(-6, -7, 4);
            };
        });
    }

    private void Update()
    {
    }
}
