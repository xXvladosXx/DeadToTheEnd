using System;
using AnimatorStateMachine.Core;
using SkillsSystem;
using UnityEngine;

namespace AnimatorStateMachine.States
{
    [CreateAssetMenu (menuName = "AnimatorState/SpawnEffectState")]
    public class SpawnEffectAnimatorState : BaseAnimatorState
    {
        [SerializeField] private GameObject _particle;
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private float _timeToSpawn;
        [SerializeField] private float _lifeTime;
        [SerializeField] private ActiveSkill _activeSkill;

        private bool _wasSpawned;
        private Skill _currentSkill;
        public override void OnEnter(AnimatorMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            _currentSkill = animator.GetComponent<SkillManager>().Skill;
        }

        public override void OnUpdate(AnimatorMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if(_wasSpawned) return;

            if (stateInfo.normalizedTime > _timeToSpawn)
            {
                _wasSpawned = true;
                GameObject particle = Instantiate(_particle, animator.transform);
                
                Destroy(particle, _lifeTime);
            }
        }

        public override void OnExit(AnimatorMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            _wasSpawned = false;
        }
    }
}