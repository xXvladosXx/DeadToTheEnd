using System;
using System.Collections.Generic;
using Entities.Core;
using StateMachine.WarriorEnemy.Components;
using UnityEngine;

namespace SkillsSystem
{
    [RequireComponent(typeof(ISkillUser))]
    public class SkillManager : MonoBehaviour
    {
        [SerializeField] private Skill _skill;
        [field: SerializeField] public SkillsContainer SkillsContainer { get; private set; }
        public Dictionary<Type, float> SkillsCooldown { get; private set; }
        private Dictionary<Type, float> CurrentSkillsCooldown { get; set; }

        private CooldownTimer _cooldownTimer;
        private ActiveSkill _lastAppliedSkill;

        public Skill Skill => _skill;
        private void Awake()
        {
            SkillsCooldown = new Dictionary<Type, float>();
            
            _cooldownTimer = new CooldownTimer();
            _cooldownTimer.Init(SkillsCooldown);
        }

        private void Update()
        {
            _cooldownTimer.Update(Time.deltaTime, CurrentSkillsCooldown);

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _skill.ApplySkill(GetComponent<AliveEntity>());
                var activeSkill = _skill as ActiveSkill;
                _lastAppliedSkill = activeSkill;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _skill.ApplySkill(GetComponent<AliveEntity>());
                var activeSkill = _skill as ActiveSkill;
                _lastAppliedSkill = activeSkill;
            }
        }

        public void StartCooldown(Type skill, float time)
        {
            _cooldownTimer.StartCooldown(skill, time);
            CurrentSkillsCooldown = new Dictionary<Type, float>(SkillsCooldown);
        }

        public void SpawnPrefab()
        {
            _lastAppliedSkill.SpawnPrefab();
        }
       
        void OnDrawGizmos()
        {
            // Draw a semitransparent blue cube at the transforms position
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector3(6, 4, 14));
        }
    }
}