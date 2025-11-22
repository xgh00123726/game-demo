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
            Command.Register("toggle-all-gizmos", () =>
            {
                IsDrawGizmos = !IsDrawGizmos;
            });
            Command.Register("toggle-draw-force", () =>
            {
                IsDrawForce = !IsDrawForce;
            });
            Command.Register("toggle-draw-collide", () =>
            {
                IsDrawCollider = !IsDrawCollider;
            });
            Command.Register("toggle-draw-move", () =>
            {
                IsDrawMove = !IsDrawMove;
            });
            Command.Register("toggle-draw-attackRange", () =>
            {
                IsDrawAttackRange = !IsDrawAttackRange;
            });
            Command.Register("toggle-draw-trigger", () =>
            {
                IsDrawTrigger = !IsDrawTrigger;
            });
            Command.Register("toggle-draw-grid", () =>
            {
                IsDrawGrid = !IsDrawGrid;
            });
        }
    }
}
