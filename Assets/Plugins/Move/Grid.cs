using GameBase.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Move
{
    public class Grid
    {
        public int weightFactor = 100;

        internal int horizontalNum;
        internal int verticalNum;
        internal float centerX;
        internal float centerY;
        internal float gridW;
        internal float gridH;
        internal float xMin;
        internal float xMax;
        internal float yMin;
        internal float yMax;
        internal float boxW;
        internal float boxH;
        
        public List<List<int>> map;

        public int HorizontalNum => horizontalNum;
        public int VerticalNum => verticalNum;
        public Vector2 Center => new Vector2(centerX, centerY);
        public Vector2 GridSize => new Vector2(gridW, gridH);
        public Vector2 BoxSize => new Vector2(boxW, boxH);
        public float XMin => xMin;
        public float XMax => xMax;
        public float YMin => yMin;
        public float YMax => yMax;
        public List<int> this[int i]
        {
            get => map[i];
        }
        public int this[Coord coord]
        {
            get
            {
                if (map.Count == 0 || map[0].Count == 0)
                {
                    return weightFactor;
                }
                if (coord.x >= map.Count || coord.y >= map[0].Count || coord.x < 0 || coord.y < 0)
                {
                    return weightFactor;
                }
                return map[coord.x][coord.y];
            }
        }

        public int GetXIndex(float x)
        {
            return (int)((x - xMin) / boxW);
        }
        public int GetYIndex(float y)
        {
            return (int)((y - yMin) / boxH);
        }
        public float GetXPos(int xIndex)
        {
            return xMin + (xIndex + 0.5f) * boxW;
        }
        public float GetYPos(int yIndex)
        {
            return yMin + (yIndex + 0.5f) * boxH;
        }

        public Grid(Rect rect, Vector2 boxSize)
        {
            centerX = rect.center.x;
            centerY = rect.center.y;
            gridW = rect.width;
            gridH = rect.height;

            boxW = boxSize.x;
            boxH = boxSize.y;

            horizontalNum = (int)(gridW / boxW / 2) * 2;
            verticalNum = (int)(gridH / boxH / 2) * 2;

            xMin = centerX - horizontalNum / 2 * boxW;
            xMax = centerX + horizontalNum / 2 * boxW;
            yMin = centerY - verticalNum / 2 * boxH;
            yMax = centerY + verticalNum / 2 * boxH;

            map = new(horizontalNum);
            for (int i = 0; i < horizontalNum; i++)
            {
                map.Add(new(verticalNum));
                for (int j = 0; j < verticalNum; j++)
                {
                    map[i].Add(weightFactor);
                }
            }
        }

        public bool AnyWeightIsExpr(Coord from, Coord to, Func<int, bool> expr)
        {
            int minX = Mathf.Min(from.x, to.x);
            int minY = Mathf.Min(from.y, to.y);
            int maxX = Mathf.Max(from.x, to.x);
            int maxY = Mathf.Max(from.y, to.y);
            int dx = Mathf.Abs(from.x - to.x);
            int dy = Mathf.Abs(from.y - to.y);
            for (int i = 0; i < dx / 2 + 1; i++)
            {
                for (int j = 0; j < dy / 2 + 1; j++)
                {
                    if (expr?.Invoke(map[minX + i][minY + j]) == true)
                    {
                        return true;
                    }
                    if (expr?.Invoke(map[maxX - i][maxY - j]) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void UpdateMap(Rect rect, float weight)
        {
            float xMin = rect.xMin;
            float xMax = rect.xMax;
            float yMin = rect.yMin;
            float yMax = rect.yMax;

            int i1 = GetXIndex(xMin);
            int i2 = GetXIndex(xMax);
            int j1 = GetYIndex(yMin);
            int j2 = GetYIndex(yMax);
            for (int i = i1; i <= i2; i++)
            {
                for (int j = j1; j <= j2; j++)
                {
                    map[i][j] = (int)(weight * weightFactor);
                }
            }
        }
    }
}
