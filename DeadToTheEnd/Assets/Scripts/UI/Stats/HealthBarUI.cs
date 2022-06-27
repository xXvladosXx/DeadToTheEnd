using Entities.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stats
{
    public class HealthBarUI : BarUI
    {
        public override void InitBarData(AliveEntity aliveEntity)
        {
            aliveEntity.Health.OnHealthPctChanged += ChangeHealth;
            Debug.Log(aliveEntity.gameObject);
            Bar.fillAmount = aliveEntity.Health.HealthValue / aliveEntity.Health.GetMaxHealth;
            StartCoroutine(ChangeToPct(aliveEntity.Health.HealthValue / aliveEntity.Health.GetMaxHealth));
        }
        
        private void ChangeHealth(float obj)
        {
            StartCoroutine(ChangeToPct(obj));
        }
    }
}