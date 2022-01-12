using System;
using UnityEngine;

namespace Pathfinding
{
    public class PathfindingAgent : MonoBehaviour
    {
        [SerializeField] private LayerMask _unwalkableMask;
        [SerializeField] private Vector2 _gridWorldSize;
        [SerializeField] private float _nodeRadius;

        private NodeGridManager _gridManager;
        private Pathfinding _pathfinding;

        public Vector3[] GetPath(Vector3 targetPosition)
        {
            _gridManager.CreateGrid();
            return _pathfinding.FindPath(transform.position, targetPosition);
        }

        private void Awake()
        {
            _gridManager = new NodeGridManager(_unwalkableMask, _gridWorldSize, _nodeRadius);
            _pathfinding = new Pathfinding(_gridManager);
        }

        private void OnDrawGizmos()
        {
            _gridManager?.OnDrawGizmos();
        }
    }
}
