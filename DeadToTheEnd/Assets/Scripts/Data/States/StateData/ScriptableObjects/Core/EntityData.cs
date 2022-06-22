using Data.Camera;
using UnityEngine;

namespace Data.ScriptableObjects
{
    public abstract class EntityData : ScriptableObject
    {
        [field: SerializeField] public ShakeCameraData ShakeCameraData { get; private set; }

    }
}