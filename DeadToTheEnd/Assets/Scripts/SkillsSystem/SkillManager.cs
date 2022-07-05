using System;
using System.Collections.Generic;
using Entities.Core;
using InventorySystem;
using InventorySystem.Core;
using SaveSystem;
using StateMachine.Core;
using StateMachine.Player.States.Movement.Grounded.Combat;
using StateMachine.WarriorEnemy.Components;
using TimerSystem;
using UnityEngine;

namespace SkillsSystem
{
    [RequireComponent(typeof(ISkillUser))]
    public class SkillManager : MonoBehaviour, ISavable, ITimerController
    {
        [SerializeField] private Skill _skill;
        [field: SerializeField] public SkillsContainer SkillsContainer { get; private set; }
        [field: SerializeField] public SkillsContainer QuickBarSkillsContainer { get; private set; }
        public Dictionary<ITimeable, float> SkillsCooldown { get; private set; }
        public Dictionary<ITimeable, float> CurrentSkillsCooldown { get; private set; }

        private CooldownTimer _cooldownTimer;
        public Skill LastAppliedSkill { get; set; }

        public Skill Skill => _skill;
        private void Awake()
        {
            SkillsCooldown = new Dictionary<ITimeable, float>();
            
            _cooldownTimer = new CooldownTimer();
            
            _cooldownTimer.Init(SkillsCooldown);
            CurrentSkillsCooldown = new Dictionary<ITimeable, float>(SkillsCooldown);
        }

        private void Update()
        {
            _cooldownTimer.Update(Time.deltaTime, CurrentSkillsCooldown);
            SkillsCooldown = _cooldownTimer.Cooldowns;
        }

        public void StartCooldown(ITimeable skill, float time)
        {
            _cooldownTimer.StartCooldown(skill, time);
            CurrentSkillsCooldown = new Dictionary<ITimeable, float>(SkillsCooldown);
        }

        public void SpawnPrefab(int index)
        {
             //_lastAppliedSkill.SpawnPrefab();
             LastAppliedSkill.ApplySkill(GetComponent<ISkillUser>(), index);
        }
       
        void OnDrawGizmos()
        {
            // Draw a semitransparent blue cube at the transforms position
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector3(6, 4, 14));
        }

        public CooldownTimer CooldownTimer => _cooldownTimer;
        public object CaptureState()
        {
            var savedInventories = new SavableInventories
            {
                AllSkills = new List<SavableItemSlot>(),
                QuickBarSkills = new List<SavableItemSlot>(),
            };
            
            foreach (var itemSlot in SkillsContainer.ItemContainer.GetItemSlots)
            {
                var savableItemSlot = new SavableItemSlot(itemSlot.ID, itemSlot.Quantity, itemSlot.Index);
                savedInventories.AllSkills.Add(savableItemSlot);
            }
            
            foreach (var itemSlot in QuickBarSkillsContainer.ItemContainer.GetItemSlots)
            {
                var savableItemSlot = new SavableItemSlot(itemSlot.ID, itemSlot.Quantity, itemSlot.Index);
                savedInventories.QuickBarSkills.Add(savableItemSlot);
            }

            return savedInventories;
        }

        public void RestoreState(object state)
        {
            SkillsContainer.ItemContainer.Clear();
            QuickBarSkillsContainer.ItemContainer.Clear();
            
            var savedInventories = (SavableInventories) state;

            foreach (var itemSlot in savedInventories.AllSkills)
            {
                var slot = new ItemSlot(SkillsContainer.ItemContainer.GetDatabase.GetItemByID(itemSlot.ItemId),
                    itemSlot.Quantity, itemSlot.ItemId);
                SkillsContainer.ItemContainer.AddItem(slot, itemSlot.Index);
            }

            foreach (var itemSlot in savedInventories.QuickBarSkills)
            {
                var slot = new ItemSlot(QuickBarSkillsContainer.ItemContainer.GetDatabase.GetItemByID(itemSlot.ItemId),
                    itemSlot.Quantity, itemSlot.ItemId);
                QuickBarSkillsContainer.ItemContainer.AddItem(slot, itemSlot.Index);
            }
        }
        
        public bool TryToApplySkill(int index, ISkillUser skillUser)
        {
            if (QuickBarSkillsContainer.ItemContainer.GetItemSlots[index].Item is ITimeable timeable)
            {
                if (SkillsCooldown.ContainsKey(timeable)) return false;
                var skill = QuickBarSkillsContainer.ItemContainer.GetItemSlots[index].Item as ActiveSkill;
                if (!skill.CheckRequirementsToCast(skillUser)) return false;
                
                LastAppliedSkill = skill;
                StartCooldown(timeable, timeable.GetTime());

                return true;
            }

            return false;
        }
    }
    
    [Serializable]
    public class SavableInventories
    {
        public List<SavableItemSlot> AllSkills;
        public List<SavableItemSlot> QuickBarSkills;
    }
}