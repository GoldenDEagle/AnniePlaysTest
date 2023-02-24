using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class GameObjectExtensions
    {
        // checks if layer matches GO layer
        public static bool IsInLayer(this GameObject go, LayerMask layer)
        {
            return layer == (layer | 1 << go.layer);
        }
    }
}