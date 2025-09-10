using GameBase.Creatures;
using GameBase.GCamera;
using GameBase.Projectiles;
using GameBase.Tools;
using GameBase.Modify;
using UnityEngine;
using Instance.Shops;
using GameBase.Shops;
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
            c.ModifyableContainer["maxHP"].AddModify(ModifyerSys<float>.Instance.NewEntity((Modifyer<float> e) =>
            {
                e.ModifyFunc = ConvientModifyerFunc.FloatFixedValue(10000);
                e.type = ModifyType.Once | ModifyType.Forever;
            }));
            c.ModifyableContainer["currHP"].AddModify(ModifyerSys<float>.Instance.NewEntity((Modifyer<float> e) =>
            {
                e.ModifyFunc = ConvientModifyerFunc.FloatFixedValue(10000);
                e.type = ModifyType.Once | ModifyType.Forever;
            }));
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

        var character = Constructor.Creatures.Factory.Instance.Get(Constructor.Creatures.Type.Common, 0);
        var player = character.Obj.AddComponent<Player>();
        player.character = character;
        character.Position = new Vector3(-6, -7, 4);

        ProjectileGizmos.Instance.ToggleShow();

        var shop = ShopSys.Instance.NewEntity();
        shop.Obj.transform.position = character.Position + new Vector3(3, 0, 0);
        var shopView = new ShopView();
        shop.shopView = shopView;
        shop.newrView = new NearView();
        shop.shoper = player;
        shop.interactive = shopView;
        shop.model = new ShopModel();
        shop.GoodNum = 5;
    }

    private void Update()
    {

    }
}
