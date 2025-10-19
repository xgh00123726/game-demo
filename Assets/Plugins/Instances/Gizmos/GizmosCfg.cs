using GameBase.Tools;

namespace Instance
{
    public static class GizmosCfg
    {
        public static float drawY = -7;

        public static int drawTimes = 10;

        public static bool isDrawGizmos = false;
        public static bool isDrawCollider = true;
        public static bool isDrawMove = true;
        public static bool isDrawForce = false;
        public static bool isDrawAI = false;
        public static bool isDrawAttackRange = true;
        public static bool isDrawTrigger = true;
        public static bool isDrawGrid = true;
        public static bool isDrawObstacle = true;

        public static float forceLenTimes = 5;
        static GizmosCfg()
        {
            Command.Register("toggle-all-gizmos", () =>
            {
                isDrawGizmos = !isDrawGizmos;
            });
            Command.Register("toggle-draw-force", () =>
            {
                isDrawForce = !isDrawForce;
            });
            Command.Register("toggle-draw-collide", () =>
            {
                isDrawCollider = !isDrawCollider;
            });
            Command.Register("toggle-draw-move", () =>
            {
                isDrawMove = !isDrawMove;
            });
            Command.Register("toggle-draw-attackRange", () =>
            {
                isDrawAttackRange = !isDrawAttackRange;
            });
            Command.Register("toggle-draw-trigger", () =>
            {
                isDrawTrigger = !isDrawTrigger;
            });
            Command.Register("toggle-draw-grid", () =>
            {
                isDrawGrid = !isDrawGrid;
            });
        }
    }
}
