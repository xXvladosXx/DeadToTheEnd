using UnityEngine;

namespace DefaultNamespace
{
    public interface IRotatable
    {
        void Rotate(GameObject gameObject, Vector3 axis, float speed);
    }
}