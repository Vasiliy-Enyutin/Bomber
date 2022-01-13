using System.Collections;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(Seeker))]
    public class RandomPathSetter : MonoBehaviour
    {
        [SerializeField] private LayerMask _unwalkableLayer;
        [SerializeField] private float _cooldown;
        private GameObject[] _walkabkeTiles;
        private AIPath _path;
        private bool isOnCooldown;


        private void Awake()
        {
            _path = GetComponent<AIPath>();
            _path.endReachedDistance = 0.2f;
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
                if (isOnCooldown == false)
                {
                    StartCoroutine(SetPathDestination());
                }
            }
        }

        private IEnumerator SetPathDestination()
        {
            isOnCooldown = true;
            yield return new WaitForSeconds(_cooldown);
            isOnCooldown = false;
            _path.destination = GetRandomTileToMove();
        }
    

        private Vector2 GetRandomTileToMove()
        {
            RaycastHit2D hit;
            do
            {
                int randomIndex = Random.Range(0, _walkabkeTiles.Length);
                hit = Physics2D.CircleCast(_walkabkeTiles[randomIndex].transform.position,
                    TileDiameterDefiner.TileDiameter / 2, Vector2.zero, _unwalkableLayer);

                if (hit.collider == null)
                {
                    return _walkabkeTiles[randomIndex].transform.position;
                }
            } while (hit.collider != null);

            Debug.Log("No tile to move");
            return Vector2.zero;
        }
    }
}
