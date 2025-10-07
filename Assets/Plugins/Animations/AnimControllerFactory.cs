namespace GameBase.Animations
{
    public enum AnimType
    {
        None,
        Human,
    }
    public class AnimControllerFactory
    {
        public static AnimController Get(AnimType type)
        {
            if (type == AnimType.Human)
            {
                return AnimControllerSys.Instance.NewEntity<HumanAnimController>();
            }

            return null;
        }
    }
}
