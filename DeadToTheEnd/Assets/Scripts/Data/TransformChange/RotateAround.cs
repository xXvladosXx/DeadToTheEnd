using Data.Combat;
using DefaultNamespace;
using UnityEngine;

namespace Data.TransformChange
{
    [CreateAssetMenu (menuName = "TransformChange/RotateAround")]
    public class RotateAround : TransformChanger, IRotatable
    {
        public void Rotate(GameObject gameObject, Vector3 axis, float speed)
        {
            gameObject.transform.RotateAround(gameObject.transform.position, gameObject.transform.up, Time.deltaTime * speed);
        }

        public override void Change(GameObject gameObject)
        {
            Rotate(gameObject, gameObject.transform.up, Speed);
        }

        public override void SetData(AttackData attackData, GameObject gameObject)
        {
            
        }
    }
}