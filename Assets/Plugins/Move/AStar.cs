using GameBase.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Move
{
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static int ManhattanDistance(Coord c1, Coord c2)
        {
            var dx = c1.x - c2.x;
            var dy = c1.y - c2.y;
            if (dx < 0)
            {
                dx = -dx;
            }
            if (dy < 0)
            {
                dy = -dy;
            }
            return dx + dy;
        }
        public static float DiagonalDistance(Coord c1, Coord c2)
        {
            int manD = ManhattanDistance(c1, c2);
            if (manD == 2)
            {
                return 1.414f;
            }
            return manD;
        }
        public static bool ValueEqual(Coord c1,  Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public override string ToString()
        {
            return $"x:{x}, y:{y}";
        }
    }
    public static class CoordUtil
    {
        public static int GetMapValue(this List<List<int>> map, Coord coord, int defaultValue)
        {
            if (map.Count == 0 || map[0].Count == 0)
            {
                return defaultValue;
            }
            if (coord.x >= map.Count || coord.y >= map[0].Count || coord.x < 0 || coord.y < 0)
            {
                return defaultValue;
            }
            return map[coord.x][coord.y];
        }
    }
    public class AStarNode : IComparable<AStarNode>
    {
        public Coord coord;
        public int gCost;
        public int hCost;
        public AStarNode last;

        public AStarNode(List<List<int>> map, int defaultWeight, AStarNode last, Coord curr, Coord begin, Coord end)
        {
            coord = curr;
            if (last == null)
            {
                gCost = 0;
            }
            else
            {
                gCost = last.gCost + (int)(Coord.DiagonalDistance(curr, last.coord) * map.GetMapValue(coord, defaultWeight));
            }
            hCost = Coord.ManhattanDistance(curr, end) * defaultWeight;
            this.last = last;
        }

        public int CompareTo(AStarNode other)
        {
            return other.gCost + other.hCost - gCost - hCost;
        }
    }
    public class AStar
    {
        private List<List<int>> _map;
        private PriorityQueue<AStarNode> _todo = new();
        private HashSet<Coord> _visited = new();
        private List<Coord> _way = new();
        private Coord _b;
        private Coord _e;
        private int _cost;

        public int maxCost = 1000;
        public int defaultMapWeight = 100;

        public int Cost => _cost;

        private void Enqueue(AStarNode last, Coord coord)
        {
            if (_visited.Contains(coord))
            {
                return;
            }
            _visited.Add(coord);
            _todo.Enqueue(new AStarNode(_map, defaultMapWeight, last, coord, _b, _e));
        }
        public List<Coord> GetWay(List<List<int>> map, Coord begin, Coord end)
        {
            _map = map;
            _todo.Clear();
            _visited.Clear();
            _b = begin;
            _e = end;
            _cost = 0;

            Enqueue(null, begin);

            var currNode = _todo.Dequeue();
            Coord curr = currNode.coord;
            _cost = 1;
            while (!Coord.ValueEqual(curr, end))
            {
                _cost++;
                if (_cost >  maxCost)
                {
                    break;
                }
                int x = curr.x;
                int y = curr.y;
                Enqueue(currNode, new Coord(x + 1, y + 1));
                Enqueue(currNode, new Coord(x + 1, y - 1));
                Enqueue(currNode, new Coord(x + 1, y    ));
                Enqueue(currNode, new Coord(x - 1, y + 1));
                Enqueue(currNode, new Coord(x - 1, y - 1));
                Enqueue(currNode, new Coord(x - 1, y    ));
                Enqueue(currNode, new Coord(x    , y + 1));
                Enqueue(currNode, new Coord(x    , y - 1));
                currNode = _todo.Dequeue();
                curr = currNode.coord;
                //Enqueue(currNode, new Coord(x    , y    ));
            }

            _way.Clear();
            for (var node = currNode; node != null; node = node.last)
            {
                _way.Add(node.coord);
            }

            return _way;
        }
    }
}
