using GameBase.Creatures;
using GameBase.GCamera;
using GameBase.Projectiles;
using GameBase.Tools;
using GameBase.Modify;
using UnityEngine;
using GameBase.Texts;
using GameBase.AI;
using GameBase.UI;
public class Initer : MonoBehaviour
{
    public bool testTextMgr = true;
    void Start()
    {
        var builder = new BehaviorTreeBuilder();

        builder.Repeat(3)
                    .Sequence()
                        .IF(() => testTextMgr)
                        .Log(TextMgr.Get(0))
                        .Log(TextMgr.Get(1))
                        .Log(TextMgr.Get(2))
                        .Log(TextMgr.Get(3))
                        .Log(TextMgr.Get(4))
                        .Log(TextMgr.Get(5))
                        .Log(TextMgr.Get(6))
                        .Log(TextMgr.Get(7))
                        .Log(TextMgr.Get(8))
                        .Log(TextMgr.Get(9))
                    .Back()
                .End();
        builder.Tree.Tick();

        Physics.gravity = new Vector3(0, -100, 0);

        Command.Register("generate-enermy", () =>
        {
            var c = Constructor.Creatures.Factory.Instance.Get(Constructor.Creatures.Type.Common, 1);
            c.Position = CameraSys.MouseHitPosition;
            c.Modifyables.ModifySet("maxHP", ModifyerSys.Instance.NewEntity((Modifyer e) =>
            {
                e.value = 10000;
                e.type = ModifyType.Once | ModifyType.Forever;
            }));
            c.Modifyables.ModifySet("currHP", ModifyerSys.Instance.NewEntity((Modifyer e) =>
            {
                e.value = 10000;
                e.type = ModifyType.Once | ModifyType.Forever;
            }));
        });

        Command.Register("gen-creature", (int id) =>
        {
            var c = Constructor.Creatures.Factory.Instance.Get(Constructor.Creatures.Type.Common, 1);
            c.Position = CameraSys.MouseHitPosition;

            AIFactory.Get(Type.FollowAttack).AddTo(c);
        });

        Command.Register("gen-ai-creature", (int id, int aiID) =>
        {
            var c = Constructor.Creatures.Factory.Instance.Get(Constructor.Creatures.Type.Common, 1);
            c.Position = CameraSys.MouseHitPosition;

            AIFactory.Get(aiID).AddTo(c);
        });

        Command.Register("gen-many-creature", (int num) =>
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

        var character = Constructor.Creatures.Factory.Instance.Get(Constructor.Creatures.Type.Common, 0);
        var player = character.Obj.AddComponent<Player>();
        player.character = character;
        character.Position = new Vector3(-6, -7, 4);

        ProjectileGizmos.Instance.ToggleShow();
    }

    private void Update()
    {

    }
}
