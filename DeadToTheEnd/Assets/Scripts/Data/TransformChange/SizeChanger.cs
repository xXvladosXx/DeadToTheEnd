using Data.Combat;
using UnityEngine;

namespace Data.TransformChange
{
    [CreateAssetMenu (menuName = "TransformChange/SizeChanger")]
    public class SizeChanger : TransformChanger
    {

        public override void Change(GameObject gameObject, AttackData attackData)
        {
            gameObject.transform.localScale +=  Vector3.one * Time.deltaTime * Speed;
        }

        public override void SetData(AttackData attackData, GameObject gameObject)
        {
            
        }
    }
}