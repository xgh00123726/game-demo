namespace GameBase.Tools.Transforms
{
    public interface ITask
    {
        void Update();
        bool IsOver { get; }
        TaskResult Result { get; set; }
    }
}
