using System;
using Data.Combat;
using Data.ScriptableObjects;
using Data.Stats;

namespace CameraManage
{
    public class CameraShaker
    {

        public void ShakeCameraOnAttackHit(AttackData attackData)
        {
            if(attackData.ShakeCameraData == null) return;
            
            switch (attackData.AttackType)
            {
                case AttackType.Knock:
                    CinemachineCameraSwitcher.Instance.ShakeCamera(attackData.ShakeCameraData.KnockAttackHitIntensity,
                        attackData.ShakeCameraData.KnockAttackHitTime);
                    break;
                case AttackType.Medium:
                    CinemachineCameraSwitcher.Instance.ShakeCamera(attackData.ShakeCameraData.MediumAttackHitIntensity,
                        attackData.ShakeCameraData.MediumAttackHitTime);
                    break;
                case AttackType.Easy:
                    CinemachineCameraSwitcher.Instance.ShakeCamera(attackData.ShakeCameraData.EasyAttackHitIntensity,
                        attackData.ShakeCameraData.EasyAttackHitTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }
}