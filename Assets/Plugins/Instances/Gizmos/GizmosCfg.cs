using GameBase.Tools;

namespace Instance
{
    public static class GizmosCfg
    {
        public static float drawY = -7;

        public static int drawTimes = 10;

        public static bool IsDrawGizmos { get; set; } = false;
        public static bool IsDrawCollider { get; set; } = true;
        public static bool IsDrawMove { get; set; } = true;
        public static bool IsDrawForce { get; set; } = false;
        public static bool IsDrawAI { get; set; } = false;
        public static bool IsDrawAttackRange { get; set; } = true;
        public static bool IsDrawTrigger { get; set; } = true;
        public static bool IsDrawGrid { get; set; } = true;
        public static bool IsDrawObstacle { get; set; } = true;

        public static float ForceLenTimes { get; set; } = 5;
        static GizmosCfg()
        {
            Command.RegisterCommand("toggle-all-gizmos").SetAction(result =>
            {
                IsDrawGizmos = !IsDrawGizmos;
            });
            Command.RegisterCommand("toggle-draw-force").SetAction(result =>
            {
                IsDrawForce = !IsDrawForce;
            });
            Command.RegisterCommand("toggle-draw-collide").SetAction(result =>
            {
                IsDrawCollider = !IsDrawCollider;
            });
            Command.RegisterCommand("toggle-draw-move").SetAction(result =>
            {
                IsDrawMove = !IsDrawMove;
            });
            Command.RegisterCommand("toggle-draw-attackRange").SetAction(result =>
            {
                IsDrawAttackRange = !IsDrawAttackRange;
            });
            Command.RegisterCommand("toggle-draw-trigger").SetAction(result =>
            {
                IsDrawTrigger = !IsDrawTrigger;
            });
            Command.RegisterCommand("toggle-draw-grid").SetAction(result =>
            {
                IsDrawGrid = !IsDrawGrid;
            });
        }
    }
}
