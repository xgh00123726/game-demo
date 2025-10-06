using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.UI;

namespace Instance
{
    public class EpicBarController : SingletonInstance<EpicBarController>
    {
        private static Creature _playerBarTarget;

        public static EpicBar healthBar;
        public static EpicBar manaBar;

        public EpicBarController()
        {
            healthBar = EpicBarSys.Instance.NewEntity();
            healthBar.Obj.SetActive(false);

            manaBar = EpicBarSys.Instance.NewEntity((EpicBar e) =>
            {
                e.ObjID = 8;
            });
            manaBar.Obj.SetActive(false);
        }

        public static void SetPlayerBarTarget(Creature target)
        {
            _playerBarTarget = target;
            if (_playerBarTarget == null)
            {
                healthBar.Obj.SetActive(false);
                manaBar.Obj.SetActive(false);
            }
            else
            {
                healthBar.Obj.SetActive(true);
                manaBar.Obj.SetActive(true);
            }
        }

        protected override void Update()
        {
            if (_playerBarTarget == null)
            {

            }
            else
            {
                healthBar.CurrHP = _playerBarTarget.Modifyables["currHP"];
                healthBar.MaxHP = _playerBarTarget.Modifyables["maxHP"];

                manaBar.CurrHP = _playerBarTarget.Modifyables["currMana"];
                manaBar.MaxHP = _playerBarTarget.Modifyables["maxMana"];
            }
        }
    }
}
