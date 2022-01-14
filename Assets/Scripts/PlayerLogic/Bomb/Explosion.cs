using System;
using UnityEngine;

namespace PlayerLogic.Bomb
{
    public class Explosion : MonoBehaviour
    {
        private BoxCollider2D _collider;
        
        
        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

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

        private void DisableCollider()     // called from Animator
        {
            _collider.enabled = false;
        }
    }
}
