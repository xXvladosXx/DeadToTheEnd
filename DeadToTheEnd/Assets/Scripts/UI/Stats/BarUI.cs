using System.Collections;
using Entities.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stats
{
    public abstract class BarUI : MonoBehaviour
    {
        [SerializeField] protected Image Bar;
        [SerializeField] private float _updateSpeedSeconds;

        public abstract void InitBarData(AliveEntity aliveEntity);

        protected IEnumerator ChangeToPct(float pct)
        {
            float preChangePct = Bar.fillAmount;
            float elapsed = 0f;

            while (elapsed < _updateSpeedSeconds)
            {
                elapsed += Time.deltaTime;
                Bar.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / _updateSpeedSeconds);
                yield return null;
            }

            Bar.fillAmount = pct;
        }
    }
}