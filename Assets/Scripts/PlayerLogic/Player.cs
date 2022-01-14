using System;
using UnityEngine;

namespace PlayerLogic
{
    public class Player : MonoBehaviour, IDestroyable
    {
        private void OnDestroy()
        {
            GlobalEventStorage.InvokePlayerDestroy();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
