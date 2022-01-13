using DestroyableObjects;
using UnityEngine;

namespace PlayerLogic
{
    public class Player : MonoBehaviour, IDestroyable
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}