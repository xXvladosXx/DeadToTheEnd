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
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private float _timeToSpawn;
        [SerializeField] private float _lifeTime;

        private bool _wasSpawned;
        public override void OnEnter(AnimatorMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        public override void OnUpdate(AnimatorMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if(_wasSpawned) return;

            if (stateInfo.normalizedTime > _timeToSpawn)
            {
                _wasSpawned = true;
                GameObject particle = Instantiate(_particle, animator.transform.forward + animator.transform.position + _offset, 
                    Quaternion.Euler(_rotation));
                
                Destroy(particle, _lifeTime);
            }
        }

        public override void OnExit(AnimatorMachine characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            _wasSpawned = false;
        }
    }
}