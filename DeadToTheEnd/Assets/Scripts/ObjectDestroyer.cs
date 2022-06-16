using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ObjectDestroyer
    {
        private Type _destroyOnCollideWith;
        
        public ObjectDestroyer(Type triggerToDestroyObject, GameObject gameObject)
        {
            _destroyOnCollideWith = triggerToDestroyObject;
        }

    }
}