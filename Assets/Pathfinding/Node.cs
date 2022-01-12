using UnityEngine;

namespace Pathfinding
{
    public class Node
    {
        public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
        {
            Walkable = walkable;
            WorldPosition = worldPosition;
            GridX = gridX;
            GridY = gridY;
        }

        public bool Walkable { get; }
        public Vector3 WorldPosition { get; }
        
        public int CostG { get; set; }
        public int CostH { get; set; }
        public int CostF => CostG + CostH;
        
        public int GridX { get; }
        public int GridY { get; }
        public Node Parent { get; set; }
    }
}
