namespace GameBase.Animations
{
    public enum AnimType
    {
        None,
        Human,
        Zombie,
    }
    public class AnimControllerFactory
    {
        public static AnimController Get(AnimType type)
        {
            if (type == AnimType.Human)
            {
                return AnimControllerSys.Instance.NewEntity<HumanAnimController>();
            }
            if (type == AnimType.Zombie)
            {
                return AnimControllerSys.Instance.NewEntity<ZombieAnimController>();
            }

            return null;
        }
    }
}
