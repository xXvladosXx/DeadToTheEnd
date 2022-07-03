using System.Linq;
using Entities.Core;
using SkillsSystem;
using UnityEngine;

namespace InventorySystem
{
    public abstract class UpgradableItem : Item  
    {
        [field: SerializeField] public Requirement[] RequirementsToUpgrade { get; private set; }

        public bool CheckAllRequirements(AliveEntity aliveEntity)
        {
            if (RequirementsToUpgrade.Any(
                    requirement => requirement.IsChecked(aliveEntity) == false))
                return false;

            return true;
        }
    }
}