using GameBase.Move;
using GameBase.Tools;
using UnityEngine;
namespace Instance
{
    public class GridGizmos : MonoBehaviour
    {
        private void DrawGrid()
        {
            if (!GizmosCfg.isDrawGrid)
            {
                return;
            }
            var grid = CollideSys.Instance.grid;
            Gizmos.color = Color.white;
            for (int i = 0; i < grid.HorizontalNum; i++)
            {
                float x = grid.XMin + i * grid.BoxSize.x;
                float y = GizmosCfg.drawY;
                Vector3 from = new Vector3(x, y, grid.YMin);
                Vector3 to = new Vector3(x, y, grid.YMax);
                Gizmos.DrawLine(from, to);
            }
            for (int i = 0; i < grid.VerticalNum; i++)
            {
                float z = grid.YMin + i * grid.BoxSize.y;
                float y = GizmosCfg.drawY;
                Vector3 from = new Vector3(grid.XMin, y, z);
                Vector3 to = new Vector3(grid.XMax, y, z);
                Gizmos.DrawLine(from, to);
            }
        }

        private void DrawObstacle()
        {
            if (!GizmosCfg.isDrawObstacle)
            {
                return;
            }

            var grid = CollideSys.Instance.grid;
            for (int i = 0; i < grid.HorizontalNum; i++)
            {
                for (int j = 0; j < grid.VerticalNum; j++)
                {
                    if (grid[i][j] > grid.weightFactor)
                    {
                        float xMin = grid.XMin + i * grid.BoxSize.x;
                        float xMax = xMin + grid.BoxSize.x;
                        float yMin = grid.YMin + j * grid.BoxSize.y;
                        float yMax = yMin + grid.BoxSize.y;
                        Gizmos.color = Color.red;
                        Vector3 from = new Vector3(xMin, GizmosCfg.drawY, yMin);
                        Vector3 to = new Vector3(xMax, GizmosCfg.drawY, yMax);
                        Gizmos.DrawLine(from, to);
                        from = new Vector3(xMin, GizmosCfg.drawY, yMax);
                        to = new Vector3(xMax, GizmosCfg.drawY, yMin);
                        Gizmos.DrawLine(from, to);
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (!GizmosCfg.isDrawGizmos)
            {
                return;
            }

            DrawGrid();
            DrawObstacle();
        }
    }
}
