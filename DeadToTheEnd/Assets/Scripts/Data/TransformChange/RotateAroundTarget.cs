using Data.Combat;
using DefaultNamespace;
using UnityEngine;

namespace Data.TransformChange
{
    [CreateAssetMenu (menuName = "TransformChange/RotateAroundTarget")]
    public class RotateAroundTarget : TransformChanger, IRotatable
    {
        private AttackData _attackData;
        public override void Change(GameObject gameObject, AttackData attackData)
        {
            Rotate(gameObject, gameObject.transform.up, Speed);
        }

        public override void SetData(AttackData attackData, GameObject gameObject)
        {
            _attackData = attackData;
        }

        public void Rotate(GameObject gameObject, Vector3 axis, float speed)
        {
            gameObject.transform.RotateAround(_attackData.User.transform.position, axis, Time.deltaTime * speed);
        }
    }
}