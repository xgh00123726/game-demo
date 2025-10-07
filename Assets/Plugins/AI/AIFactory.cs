namespace GameBase.AI
{
    public enum AIType
    {
        None,
        Follow,
        MissFollow,
        PartolOnly,
        FollowAttack
    }

    public class AIFactory
    {
        public static BaseAI Get(AIType type)
        {
            if (type == AIType.Follow)
            {
                return AISys.Instance.NewEntity<AIFollow>();
            }

            if (type == AIType.MissFollow)
            {
                return AISys.Instance.NewEntity<AIMissFollow>();
            }

            if (type == AIType.PartolOnly)
            {
                return AISys.Instance.NewEntity<AIPartolOnly>();
            }

            if (type == AIType.FollowAttack)
            {
                return AISys.Instance.NewEntity<AIFollowAttack>();
            }

            return null;
        }

        public static BaseAI Get(int type)
        {
            return Get((AIType)type);
        }
    }
}
