using System;
using DestroyableObjects;
using UnityEngine;

namespace PlayerLogic.Bomb
{
    public class Explosion : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out IDestroyable idestroyable))
            {
                idestroyable.Destroy();
            }
        }

        private void OnAnimationEnded()     // called from Animator
        {
            Destroy(gameObject);
        }
    }
}
