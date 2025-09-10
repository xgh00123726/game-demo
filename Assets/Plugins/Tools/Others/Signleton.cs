namespace GameBase.Tools
{
    public class Signleton<T> where T : new()
    {
        private static T _instance = new();
        public static T Instance => _instance;
    }
}
