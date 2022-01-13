using UnityEngine;

namespace DestroyableObjects
{
    public class Wall : MonoBehaviour, IDestroyable
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
