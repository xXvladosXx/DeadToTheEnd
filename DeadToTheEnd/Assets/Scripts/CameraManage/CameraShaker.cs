using System;
using Data.Combat;
using Data.ScriptableObjects;

namespace CameraManage
{
    public class CameraShaker
    {
        private PlayerData _playerData;

        public CameraShaker(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void ShakeCameraOnAttackHit(AttackData attackData)
        {
            CinemachineShake.Instance.ShakeCamera(_playerData.PlayerShakeCameraData.MediumAttackHitIntensity,
                _playerData.PlayerShakeCameraData.MediumAttackHitTime);
        }
        
        public void ShakeCameraOnDamageTaken(AttackData attackData)
        {
            switch (attackData.AttackType)
            {
                case AttackType.Knock:
                    CinemachineShake.Instance.ShakeCamera(_playerData.PlayerShakeCameraData.KnockDamageTakenIntensity,
                        _playerData.PlayerShakeCameraData.KnockDamageTakenTime);
                    break;
                case AttackType.Medium:
                    CinemachineShake.Instance.ShakeCamera(_playerData.PlayerShakeCameraData.DamageTakenIntensity,
                        _playerData.PlayerShakeCameraData.DamageTakenTime);
                    break;
                case AttackType.Easy:
                    CinemachineShake.Instance.ShakeCamera(_playerData.PlayerShakeCameraData.DamageTakenIntensity,
                        _playerData.PlayerShakeCameraData.DamageTakenTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
           
        }
    }
}