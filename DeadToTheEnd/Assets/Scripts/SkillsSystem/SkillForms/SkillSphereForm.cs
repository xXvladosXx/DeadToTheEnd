using UnityEngine;

namespace SkillsSystem.SkillForms
{
    [CreateAssetMenu (menuName = "SkillSystem/Form/Sphere")]
    public class SkillSphereForm : SkillForm
    {
        [SerializeField] private float _radius;
        [SerializeField] private Vector3 _center;
        [SerializeField] private Vector3 _offset;

        private const int MAX_COLLIDERS = 200;

        public override void ApplyForm(SkillData skillData)
        {
            Collider[] hitColliders = new Collider[MAX_COLLIDERS];
            var numColliders = Physics.OverlapSphereNonAlloc(skillData.SkillUser.transform.position + _offset,
                _radius, hitColliders);

            FindTargets(skillData, numColliders, hitColliders);
        }
    }
}