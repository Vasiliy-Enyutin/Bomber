using PlayerLogic;
using UnityEngine;

namespace EnemyLogic
{
    public class EnemyAttacker : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player player))
            {
                player.Destroy();
            }
        }
    }
}
