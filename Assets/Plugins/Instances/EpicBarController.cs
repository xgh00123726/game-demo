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
        public static EpicBar expBar;

        public EpicBarController()
        {
            healthBar = EpicBarSys.Instance.NewEntity();
            healthBar.Obj.SetActive(false);

            manaBar = EpicBarSys.Instance.NewEntity((e) => e.ObjID = 8);
            manaBar.Obj.SetActive(false);

            expBar = EpicBarSys.Instance.NewEntity((e) => e.ObjID = 47);
            expBar.Obj.SetActive(false);
        }

        public static void SetPlayerBarTarget(Creature target)
        {
            _playerBarTarget = target;
            if (_playerBarTarget == null)
            {
                healthBar.Obj.SetActive(false);
                manaBar.Obj.SetActive(false);
                expBar.Obj.SetActive(false);
            }
            else
            {
                healthBar.Obj.SetActive(true);
                manaBar.Obj.SetActive(true);
                expBar.Obj.SetActive(true);
            }
        }

        protected override void Update()
        {
            if (_playerBarTarget == null)
            {

            }
            else
            {
                healthBar.CurrHP = _playerBarTarget.modifyables["currHP"].Value;
                healthBar.MaxHP = _playerBarTarget.modifyables["maxHP"].Value;
                healthBar.regen = _playerBarTarget.modifyables["healthRegen"].Value;

                manaBar.CurrHP = _playerBarTarget.modifyables["currMana"].Value;
                manaBar.MaxHP = _playerBarTarget.modifyables["maxMana"].Value;

                expBar.CurrHP = _playerBarTarget.currentExp;
                expBar.MaxHP = _playerBarTarget.levelUpExp;
            }
        }
    }
}
