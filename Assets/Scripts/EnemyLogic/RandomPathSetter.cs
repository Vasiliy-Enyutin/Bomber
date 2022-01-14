using System;
using System.Collections;
using Global;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyLogic
{
    [RequireComponent(typeof(AIPath))]
    public class RandomPathSetter : MonoBehaviour
    {
        [SerializeField] private LayerMask _unwalkableLayer;
        [SerializeField] private float _cooldown;
        private GameObject[] _walkabkeTiles;
        private AIPath _path;
        private bool _isOnCooldown = false;


        public bool ReachedDestinaion => _path.reachedDestination;
        
        private void Awake()
        {
            _path = GetComponent<AIPath>();
            _path.endReachedDistance = 0.2f;
        }

        private void OnEnable()
        {
            SoftBlock.OnSoftBlockDestroy += Scan;
        }

        private void OnDisable()
        {
            SoftBlock.OnSoftBlockDestroy -= Scan;
        }

        private void Start()
        {
            _walkabkeTiles = GameObject.FindGameObjectsWithTag("WalkableTile");
            _path.destination = GetRandomTileToMove();
        }

        private void Update()
        {
            if (_path.reachedDestination == true)
            {
                if (_isOnCooldown == false)
                {
                    StartCoroutine(SetPathDestination());
                }
            }
        }

        private IEnumerator SetPathDestination()
        {
            _isOnCooldown = true;
            yield return new WaitForSeconds(_cooldown);
            _isOnCooldown = false;
            _path.destination = GetRandomTileToMove();
        }
    

        private Vector2 GetRandomTileToMove()
        {
            RaycastHit2D hit;
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, _walkabkeTiles.Length);
                hit = Physics2D.CircleCast(_walkabkeTiles[randomIndex].transform.position,
                    TileDiameterDefiner.TileDiameter / 2, Vector2.zero, _unwalkableLayer);
            } while (hit.collider != null);
            
            return _walkabkeTiles[randomIndex].transform.position;
        }

        private void Scan()
        {
            if (AstarPath.active != null)
                AstarPath.active.Scan();
        }
    }
}
