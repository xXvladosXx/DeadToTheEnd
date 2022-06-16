using Data.Combat;
using Data.TransformChange;
using Entities.Core;
using UnityEngine;

namespace SkillsSystem.SkillPrefab
{
    [CreateAssetMenu (menuName = "TransformChange/MoveForward")]
    public class MoveForward : TransformChanger, IMovable
    {
        private Vector3 _direction;

        public override void Change(GameObject gameObject)
        {
            Move(gameObject, _direction, Speed);
        }

        public override void SetData(AttackData attackData)
        {
            _direction = attackData.User.transform.forward;
        }

        public void Move(GameObject gameObject, Vector3 direction, float speed)
        {
            gameObject.transform.position += direction * Time.deltaTime * speed;
        }
    }
}