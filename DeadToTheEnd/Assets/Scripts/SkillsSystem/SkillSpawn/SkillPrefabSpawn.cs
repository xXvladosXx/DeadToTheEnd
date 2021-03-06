using Combat.ColliderActivators;
using Entities.Core;
using SkillsSystem.Core;
using SkillsSystem.SkillPrefab;
using Unity.Mathematics;
using UnityEngine;

namespace SkillsSystem
{
    [CreateAssetMenu (menuName = "SkillSystem/Prefab/SpawnPrefab")]

    public class SkillPrefabSpawn : Core.SkillPrefab
    {
        [field: SerializeField] public AttackColliderActivator AttackColliderActivator { get; private set; }
        [field: SerializeField] public Vector3 Offset { get; private set; }
        [field: SerializeField] public Vector3 Rotation { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float TimeToDestroy { get; private set; }
        [field: SerializeField] public bool UseUserRotation { get; private set; }
        public override void SpawnPrefab(SkillData skillData)
        {
            skillData.AttackData.Speed = Speed;
            var quaternion = Quaternion.Euler(Rotation);
            
            if (UseUserRotation)
            {
                quaternion = skillData.SkillUser.transform.rotation;
            }
            
            var attackColliderActivator = Instantiate(AttackColliderActivator,
                skillData.AttackData.User.transform.position + Offset,quaternion);
            
            
            attackColliderActivator.ActivateCollider(10f, skillData.AttackData);
            
            Destroy(attackColliderActivator.gameObject, TimeToDestroy);
        }
    }
}