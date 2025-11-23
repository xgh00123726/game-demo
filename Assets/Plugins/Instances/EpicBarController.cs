using GameBase.Creatures;
using GameBase.Tools;
using GameBase.UI;

namespace Instance
{
    public class EpicBarController : SingletonInstance<EpicBarController>
    {
        private static Creature _playerBarTarget;

        public static EpicBar HealthBar { get; set; }
        public static EpicBar ManaBar { get; set; }
        public static EpicBar ExpBar { get; set; }

        public EpicBarController()
        {
            HealthBar = EpicBarSys.Instance.NewEntity("Prefabs/UI/EpicHealthBar");
            HealthBar.Obj.SetActive(false);

            ManaBar = EpicBarSys.Instance.NewEntity("Prefabs/UI/EpicManaBar");
            ManaBar.Obj.SetActive(false);

            ExpBar = EpicBarSys.Instance.NewEntity("Prefabs/UI/EpicExpBar.prefab");
            ExpBar.Obj.SetActive(false);
        }

        public static void SetPlayerBarTarget(Creature target)
        {
            _playerBarTarget = target;
            if (_playerBarTarget == null)
            {
                HealthBar.Obj.SetActive(false);
                ManaBar.Obj.SetActive(false);
                ExpBar.Obj.SetActive(false);
            }
            else
            {
                HealthBar.Obj.SetActive(true);
                ManaBar.Obj.SetActive(true);
                ExpBar.Obj.SetActive(true);
            }
        }

        protected override void Update()
        {
            if (_playerBarTarget == null)
            {

            }
            else
            {
                HealthBar.CurrHP = _playerBarTarget.Modifyables["currHP"].Value;
                HealthBar.MaxHP = _playerBarTarget.Modifyables["maxHP"].Value;
                HealthBar.Regen = _playerBarTarget.Modifyables["healthRegen"].Value;

                ManaBar.CurrHP = _playerBarTarget.Modifyables["currMana"].Value;
                ManaBar.MaxHP = _playerBarTarget.Modifyables["maxMana"].Value;

                ExpBar.CurrHP = _playerBarTarget.CurrentExp;
                ExpBar.MaxHP = _playerBarTarget.LevelUpExp;
            }
        }
    }
}
