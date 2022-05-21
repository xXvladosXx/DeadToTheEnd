using System;
using Data.Stats;
using UnityEngine;

namespace Entities
{
    public abstract class AliveEntity : MonoBehaviour
    {
        public abstract Health Health { get; protected set; }
        protected virtual void Awake()
        {
        }
    }
}