using System;
using UnityEngine;

namespace DestroyableObjects
{
    public class Wall : MonoBehaviour, IDestroyable
    {
        public static event Action OnWallDestroy;

        private void OnDestroy()
        {
            OnWallDestroy?.Invoke();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
