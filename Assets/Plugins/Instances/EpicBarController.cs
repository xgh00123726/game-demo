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
            healthBar = EpicBarSys.Instance.NewEntity("Prefabs/UI/EpicHealthBar");
            healthBar.obj.SetActive(false);

            manaBar = EpicBarSys.Instance.NewEntity("Prefabs/UI/EpicManaBar");
            manaBar.obj.SetActive(false);

            expBar = EpicBarSys.Instance.NewEntity("Prefabs/UI/EpicExpBar.prefab");
            expBar.obj.SetActive(false);
        }

        public static void SetPlayerBarTarget(Creature target)
        {
            _playerBarTarget = target;
            if (_playerBarTarget == null)
            {
                healthBar.obj.SetActive(false);
                manaBar.obj.SetActive(false);
                expBar.obj.SetActive(false);
            }
            else
            {
                healthBar.obj.SetActive(true);
                manaBar.obj.SetActive(true);
                expBar.obj.SetActive(true);
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
