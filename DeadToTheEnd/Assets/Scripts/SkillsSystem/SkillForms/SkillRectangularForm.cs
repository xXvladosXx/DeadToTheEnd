using UnityEngine;

namespace SkillsSystem.SkillForms
{
    [CreateAssetMenu (menuName = "SkillSystem/Form/Rectangular")]
    public class SkillRectangularForm : SkillForm
    {
        [SerializeField] private Vector3 _halfExtents;
        [SerializeField] private Vector3 _offset;
        
        private const int MAX_COLLIDERS = 200;
        
        public override void ApplyForm(SkillData skillData)
        {
            Collider[] hitColliders = new Collider[MAX_COLLIDERS];
            Quaternion rotation = skillData.SkillUser.transform.rotation;
            var numColliders = Physics.OverlapBoxNonAlloc(skillData.SkillUser.transform.position + _offset,
                _halfExtents, hitColliders, rotation);

            FindTargets(skillData, numColliders, hitColliders);
        }
    }
}