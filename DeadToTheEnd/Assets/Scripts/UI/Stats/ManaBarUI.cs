using Entities.Core;

namespace UI.Stats
{
    public class ManaBarUI : BarUI
    {
        public override void InitBarData(AliveEntity aliveEntity)
        {
            aliveEntity.Health.OnHealthPctChanged += ChangeHealth;
            StartCoroutine(ChangeToPct(aliveEntity.Health.HealthValue / aliveEntity.Health.GetMaxHealth));
        }
        
        private void ChangeHealth(float obj)
        {
            StartCoroutine(ChangeToPct(obj));
        }
    }
}