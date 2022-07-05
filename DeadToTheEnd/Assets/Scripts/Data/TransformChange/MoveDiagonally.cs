using Data.Combat;
using Data.TransformChange;
using UnityEngine;

namespace SkillsSystem.SkillPrefab
{
    [CreateAssetMenu (menuName = "TransformChange/MoveDiagonally")]
    public class MoveDiagonally : TransformChanger, IMovable
    {
        private Transform _user;
        public override void Change(GameObject gameObject, AttackData attackData)
        {
            var rotation = new Vector3(gameObject.transform.eulerAngles.x, _user.transform.eulerAngles.y,
                _user.transform.eulerAngles.z);
            
            gameObject.transform.eulerAngles = rotation;      
            Move(gameObject, Speed);
        }

        public override void SetData(AttackData attackData, GameObject gameObject)
        {
            _user = attackData.User.transform;
        }

        public void Move(GameObject gameObject, float speed)
        {
            gameObject.transform.position += gameObject.transform.forward * Time.deltaTime * speed;
        }
    }
}