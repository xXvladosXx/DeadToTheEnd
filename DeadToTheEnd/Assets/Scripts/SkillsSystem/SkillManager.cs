using System;
using System.Collections.Generic;
using InventorySystem;
using InventorySystem.Core;
using SaveSystem;
using StateMachine.Core;
using StateMachine.WarriorEnemy.Components;
using StatsSystem.Core;
using TimerSystem;
using UnityEngine;

namespace SkillsSystem
{
    [RequireComponent(typeof(IUser))]
    public class SkillManager : MonoBehaviour, ISavable, ITimerController, IPointsAssignable
    {
        [SerializeField] private Skill _skill;
        [SerializeField] private int _defaultPointsPerLevel = 2;
        [field: SerializeField] public SkillsContainer SkillsContainer { get; private set; }
        [field: SerializeField] public SkillsContainer QuickBarSkillsContainer { get; private set; }
        [field: SerializeField] public int UnassignedPoints { get; private set; } = 10;
       
        public Dictionary<ITimeable, float> SkillsCooldown { get; private set; }
        public Dictionary<ITimeable, float> CurrentSkillsCooldown { get; private set; }

        private CooldownTimer _cooldownTimer;
        public Skill LastAppliedSkill { get; set; }

        public event Action OnPointsChange;

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
            if (Input.GetKeyDown(KeyCode.A))
            {
                AddNewUnassignedPoints();
            }
            
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
             LastAppliedSkill.ApplySkill(GetComponent<IUser>(), index);
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

            savedInventories.Points = UnassignedPoints;
            
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
            
            UnassignedPoints = savedInventories.Points;
        }
        
        public bool TryToApplySkill(int index, IUser user)
        {
            if (QuickBarSkillsContainer.ItemContainer.GetItemSlots[index].Item is ITimeable timeable)
            {
                if (SkillsCooldown.ContainsKey(timeable)) return false;
                var skill = QuickBarSkillsContainer.ItemContainer.GetItemSlots[index].Item as ActiveSkill;
                if (!skill.CheckRequirementsToCast(user)) return false;
                
                LastAppliedSkill = skill;
                StartCooldown(timeable, timeable.GetTime());

                return true;
            }

            return false;
        }
        
        public void AddNewUnassignedPoints()
        {
            UnassignedPoints += _defaultPointsPerLevel;
            OnPointsChange?.Invoke();
        }

        public void RemovePoints(int value)
        {
            UnassignedPoints -= value;
            OnPointsChange?.Invoke();
        }

    }
    
    [Serializable]
    public class SavableInventories
    {
        public List<SavableItemSlot> AllSkills;
        public List<SavableItemSlot> QuickBarSkills;
        public int Points;
    }
}