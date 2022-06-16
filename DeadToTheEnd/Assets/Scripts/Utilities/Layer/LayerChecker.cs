using UnityEngine;

namespace Utilities.Layer
{
    public class LayerChecker
    {
        public static bool IsInLayerMask(GameObject obj, LayerMask layerMask) => ((layerMask.value & (1 << obj.layer)) > 0);
    }
}