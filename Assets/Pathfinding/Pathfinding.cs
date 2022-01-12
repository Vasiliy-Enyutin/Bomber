using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Pathfinding
    {
        private readonly NodeGridManager _nodeGrid;

        public Pathfinding(NodeGridManager nodeGrid)
        {
            _nodeGrid = nodeGrid;
        }
        
        public Vector3[] FindPath(Vector3 startPosition, Vector3 targetPosition)
        {
            Node startNode = _nodeGrid.GetNodeFromWorldPoint(startPosition);
            Node targetNode = _nodeGrid.GetNodeFromWorldPoint(targetPosition);

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].CostF < currentNode.CostF || openSet[i].CostF == currentNode.CostF && openSet[i].CostH < currentNode.CostH)
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                { 
                    return GetRetracedPath(startNode, targetNode);
                }

                foreach (Node neighbour in _nodeGrid.GetNodeNeighbours(currentNode))
                {
                    if (neighbour.Walkable == false || closedSet.Contains(neighbour)) continue;

                    int newMovementCostToNeighbour =
                        currentNode.CostG + GetDistanceBetweenNodes(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.CostG || openSet.Contains(neighbour) == false)
                    {
                        neighbour.CostG = newMovementCostToNeighbour;
                        neighbour.CostH = GetDistanceBetweenNodes(neighbour, targetNode);
                        neighbour.Parent = currentNode;

                        if (openSet.Contains(neighbour) == false)
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }

            return new[] {startPosition};
        }

        private int GetDistanceBetweenNodes(Node nodeA, Node nodeB)
        {
            int distanceX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
            int distanceY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

            if (distanceX > distanceY)
            {
                return 14 * distanceY + 10 * (distanceX - distanceY);
            }

            return 14 * distanceX + 10 * (distanceY - distanceX);
        }

        private Vector3[] GetRetracedPath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            List<Vector3> pathPositions = new List<Vector3>();
            Node currentNode = endNode;

            do
            {
                path.Add(currentNode);
                pathPositions.Add(currentNode.WorldPosition);
                
                if (currentNode.Parent != null)
                {
                    currentNode = currentNode.Parent;
                }
            } while (currentNode != startNode);
            
            path.Reverse();
            pathPositions.Reverse();
            _nodeGrid.Path = path;
            return pathPositions.ToArray();
        }
    }
}
