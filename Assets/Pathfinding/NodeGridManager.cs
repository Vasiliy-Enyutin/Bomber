using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Pathfinding
{
    public class NodeGridManager
    {
        private readonly LayerMask _unwalkableMask;
        private readonly Vector2 _gridWorldSize;
        private readonly float _nodeRadius;
        
        private Node[,] _grid;

        private readonly float _nodeDiameter;
        private readonly int _gridSizeX, _gridSizeY;
        
        public List<Node> Path; // FOR DEBUG

        public NodeGridManager(LayerMask unwalkableMask, Vector2 gridWorldSize, float nodeRadius)
        {
            _unwalkableMask = unwalkableMask;
            _gridWorldSize = gridWorldSize;
            _nodeRadius = nodeRadius;

            _nodeDiameter = nodeRadius * 2f;
            _gridSizeX = Mathf.RoundToInt(gridWorldSize.x / _nodeDiameter);
            _gridSizeY = Mathf.RoundToInt(gridWorldSize.y / _nodeDiameter);

            CreateGrid();
        }
        
        public void CreateGrid()
        {
            _grid = new Node[_gridSizeX, _gridSizeY];
            Vector3 worldBottomLeft = new Vector3(2.31399989f + _nodeRadius, 0.400000006f + _nodeRadius, -30.0528603f);

            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + y*0.1f) +
                                         Vector3.up * (y * _nodeDiameter);
                    bool walkable = !Physics2D.CircleCast(worldPoint, _nodeRadius, worldPoint, 
                        0f, _unwalkableMask);
                    
                    _grid[x, y] = new Node(walkable, worldPoint, x, y);
                }
            }
        }

        public Node GetNodeFromWorldPoint(Vector3 worldPosition)
        {
            float percentX = Mathf.Clamp01(worldPosition.x / _gridWorldSize.x);
            float percentY = Mathf.Clamp01(worldPosition.y / _gridWorldSize.y);

            int gridLastIndexX = _gridSizeX - 1;
            int gridLastIndexY = _gridSizeY - 1;
            int x = Mathf.FloorToInt(Mathf.Min( _gridSizeX * percentX, gridLastIndexX));
            int y = Mathf.FloorToInt(Mathf.Min( _gridSizeY * percentY, gridLastIndexY));

            return _grid[x, y];
        }

        public List<Node> GetNodeNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0 || Math.Abs(x) == Math.Abs(y)) continue;

                    int checkX = node.GridX + x;
                    int checkY = node.GridY + y;

                    if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)
                    {
                        neighbours.Add(_grid[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }
        
        public void OnDrawGizmos()
        {
            if (_grid != null)
            {
                foreach (Node node in _grid)
                {
                    Color colorWhite = Color.black;
                    Color colorRed = Color.red;
                    colorWhite.a = 0.3f;
                    colorRed.a = 0.3f;
                    Gizmos.color = node.Walkable ? colorWhite : colorRed;
                    if (Path != null && Path.Contains(node))
                    {
                        Gizmos.color = Color.black;
                    }
                    Gizmos.DrawCube(node.WorldPosition, Vector3.one * (_nodeDiameter - 0.01f));
                }
            }
        }
    }
}
