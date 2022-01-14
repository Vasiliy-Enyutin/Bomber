using System;
using Global;
using UnityEngine;

namespace EnemyLogic
{
    public class Enemy : MonoBehaviour, IDestroyable
    {
        private void OnDestroy()
        {
            GlobalEventStorage.InvokeEnemyDestroy();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
