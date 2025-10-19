using GameBase.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Move
{
    public class GridAStar
    {
        private Grid _grid;
        private AStar _aStar;
        private List<Coord> _aStarWay;
        private List<Vector3> _way = new();
        public GridAStar(Grid grid, AStar aStar)
        {
            _grid = grid;
            _aStar = aStar;
        }

        private bool HasObstacle(int weight)
        {
            return weight > 100;
        }

        public List<Vector3> GetWay(Vector3 begin, Vector3 end)
        {
            Coord b = new Coord(_grid.GetXIndex(begin.x), _grid.GetYIndex(begin.z));
            Coord e = new Coord(_grid.GetXIndex(end.x), _grid.GetYIndex(end.z));
            _aStarWay = _aStar.GetWay(_grid.map, b, e);
            _way.Clear();

            Coord c = _aStarWay[^1];
            int lastWayPointIndex = _aStarWay.Count - 1;
            _way.Add(new Vector3(_grid.GetXPos(c.x), end.y, _grid.GetYPos(c.y)));
            for (int i = _aStarWay.Count - 2; i >= 0; i--)
            {
                c = _aStarWay[i];
                var cLast = _aStarWay[lastWayPointIndex];
                if (!_grid.AnyWeightIsExpr(cLast, c, HasObstacle))
                {
                    _way[^1] = new Vector3(_grid.GetXPos(c.x), end.y, _grid.GetYPos(c.y));
                }
                else
                {
                    _way.Add(new Vector3(_grid.GetXPos(c.x), end.y, _grid.GetYPos(c.y)));
                    lastWayPointIndex = i;
                }
            }
            return _way;
        }

        public void LogWay()
        {
            XLogger.Instance.Log($"find way len:{_way.Count}, cost:{_aStar.Cost}");
        }
    }
}
