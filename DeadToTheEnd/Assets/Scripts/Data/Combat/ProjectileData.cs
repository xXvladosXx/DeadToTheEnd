using System;
using UnityEngine;

namespace Data.Combat
{
    [Serializable]
    public class ProjectileData
    {
        [field: SerializeField] public GameObject Projectile { get; private set; }
        [field: SerializeField] public Transform Position { get; private set; }
        [field: SerializeField] public float TimeToDestroy { get; private set; }
    }
}