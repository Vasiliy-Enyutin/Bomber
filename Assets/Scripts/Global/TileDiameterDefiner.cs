using UnityEngine;

namespace Global
{
    public class TileDiameterDefiner : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _tileSpriteRenderer;
    
    
        public static float TileDiameter { get; private set; }

    
        private void Awake()
        {
            TileDiameter = _tileSpriteRenderer.sprite.bounds.size.x;
        }
    }
}
