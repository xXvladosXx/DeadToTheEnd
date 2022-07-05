using Entities.Core;

namespace UI.Stats
{
    public class ManaBarUI : BarUI
    {
        public override void InitBarData(AliveEntity aliveEntity)
        {
            aliveEntity.Mana.OnManaPctChanged += ChangeMana;
            Bar.fillAmount = aliveEntity.Mana.ManaValue / aliveEntity.Mana.GetMaxMana;
            StartCoroutine(ChangeToPct(aliveEntity.Mana.ManaValue / aliveEntity.Mana.GetMaxMana));
        }
        
        private void ChangeMana(float obj)
        {
            StartCoroutine(ChangeToPct(obj));
        }
    }
}