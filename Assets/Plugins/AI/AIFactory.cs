namespace GameBase.AI
{
    public enum Type
    {
        Follow,
        MissFollow,
        PartolOnly,
        FollowAttack
    }

    public class AIFactory
    {
        public static BaseAI Get(Type type)
        {
            if (type == Type.Follow)
            {
                return AISys.Instance.NewEntity<AIFollow>();
            }

            if (type == Type.MissFollow)
            {
                return AISys.Instance.NewEntity<AIMissFollow>();
            }

            if (type == Type.PartolOnly)
            {
                return AISys.Instance.NewEntity<AIPartolOnly>();
            }

            if (type == Type.FollowAttack)
            {
                return AISys.Instance.NewEntity<AIFollowAttack>();
            }

            return null;
        }

        public static BaseAI Get(int type)
        {
            return Get((Type)type);
        }
    }
}
