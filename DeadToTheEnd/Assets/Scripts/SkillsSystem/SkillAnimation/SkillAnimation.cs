using StateMachine.Player.States.Movement.Grounded.Combat;
using UnityEngine;

namespace SkillsSystem.SkillPlayerState
{
    [CreateAssetMenu (menuName = "SkillSystem/Animation/Anim")]
    public class SkillAnimation : ScriptableObject
    {
        [field: SerializeField] public string AnimName { get; private set; }
    }
}