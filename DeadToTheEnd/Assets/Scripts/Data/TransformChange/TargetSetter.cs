using Data.Combat;
using UnityEngine;

namespace Data.TransformChange
{
    [CreateAssetMenu (menuName = "TransformChange/TargetSetter")]
    public class TargetSetter: TransformChanger
    {
        public override void Change(GameObject gameObject)
        {
        }

        public override void SetData(AttackData attackData, GameObject gameObject)
        {
            Debug.Log(attackData.User);
            Vector3 direction = attackData.User.Target.transform.position -
                                gameObject.transform.position;

            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = gameObject.transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            gameObject.transform.rotation = targetRotation;
        }
    }
}