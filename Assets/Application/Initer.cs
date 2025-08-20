using GameBase.Creatures;
using GameBase.GCamera;
using GameBase.Tools;
using UnityEngine;
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
            var c = CreatureSys.Instance.NewEntity<Creature>(0);
            c.Position = CameraSys.MouseHitPosition;
        });

        Command.Register("generate-enermy", (int id) =>
        {
            var c = CreatureSys.Instance.NewEntity<Creature>(0);
            c.Position = CameraSys.MouseHitPosition;
        });

        Command.Register("generate-many-enermy", (int num) =>
        {
            for (int i = 0; i < num; i++)
            {
                var c = CreatureSys.Instance.NewEntity<Creature>(0);
                c.Position = CameraSys.MouseHitPosition + new Vector3(i, 0, 0);
                c.tag = Tag.CommonCreature;
            }
        });

        Command.Register("remove-all-commonCreature", () =>
        {
            CreatureSys.Instance.RemoveAll(GameBase.Creatures.Tag.CommonCreature);
        });

        Timer.AddTask(1, () =>
        {
            var charater = CreatureSys.Instance.NewEntity<Creature>(0);
            var player = charater.Obj.AddComponent<Player>();
            player.charater = charater;
            charater.Position = new Vector3(-6, -7, 4);
        });
    }

    private void Update()
    {
    }
}
