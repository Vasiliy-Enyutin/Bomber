using DestroyableObjects;
using UnityEngine;

namespace EnemyLogic
{
    public class Enemy : MonoBehaviour, IDestroyable
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
