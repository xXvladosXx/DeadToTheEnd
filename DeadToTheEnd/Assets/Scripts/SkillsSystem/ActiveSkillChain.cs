using System.Text;
using UnityEngine;

namespace SkillsSystem
{
    [CreateAssetMenu (menuName = "SkillSystem/ActiveSkillChain")]
    public class ActiveSkillChain : ActiveSkill
    {
        [SerializeField] private ActiveSkill[] _activeSkill;
        [SerializeField] private float _skillChainCooldown;

        public override void ApplySkill(IUser user, int index = 0)
        {
            base.ApplySkill(user, index);
            
            _activeSkill[index].ApplySkill(user, index);
        }

        public override float GetTime()
        {
            return _skillChainCooldown;
        }

        public override string GetInfoDisplayText()
        {
            StringBuilder = new StringBuilder();
            StringBuilder.AppendLine();
            
            foreach (var activeSkill in _activeSkill)
            {
                StringBuilder.Append(activeSkill.GetInfoDisplayText()).AppendLine();
            }

            foreach (var requirement in RequirementsToUpgrade)
            {
                StringBuilder.Append(requirement.Data()).AppendLine();
            }
            
            StringBuilder.AppendLine();
            StringBuilder.Append("Skill cooldown: <color=#A792DD>").Append(_skillChainCooldown).Append("</color=>").AppendLine();
            
            return StringBuilder.ToString();
        }
    }
}