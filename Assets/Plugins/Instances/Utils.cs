using UnityEngine;

namespace Instance
{
    public static class Utils
    {
        public static void DrawRect(this LineRenderer lineRenderer, Rect rect, float drawY)
        {
            float xMin = rect.xMin;
            float zMin = rect.yMin;
            float xMax = rect.xMax;
            float zMax = rect.yMax;
            lineRenderer.SetPosition(0, new Vector3(xMin, drawY, zMin));
            lineRenderer.SetPosition(1, new Vector3(xMin, drawY, zMax));
            lineRenderer.SetPosition(2, new Vector3(xMax, drawY, zMax));
            lineRenderer.SetPosition(3, new Vector3(xMax, drawY, zMin));
        }
    }
}
