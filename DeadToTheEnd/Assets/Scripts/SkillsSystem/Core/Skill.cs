using System.Linq;
using System.Text;
using Entities.Core;
using InventorySystem;
using SkillsSystem.SkillPlayerState;
using StateMachine.Core;
using StateMachine.Player.States.Movement.Grounded.Combat;
using UnityEngine;

namespace SkillsSystem
{
    public abstract class Skill : UpgradableItem, ITimeable
    {
        [field: SerializeField] public SkillAnimation Anim { get; private set; } 
        public virtual void ApplySkill(IUser user, int index = 0){}
        public abstract float GetTime();
        
        protected StringBuilder StringBuilder = new StringBuilder();
    }
}