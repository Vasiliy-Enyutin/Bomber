using DestroyableObjects;
using UnityEngine;

namespace PlayerLogic.Bomb
{
    public class Explosion : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDestroyable idestroyable))
            {
                idestroyable.Destroy();
            }
        }
    }
}
