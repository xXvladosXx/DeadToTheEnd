using TMPro;
using UI.Stats;
using UnityEngine;

namespace UI.HUD
{
    public abstract class HUDUI : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI[] Levels;
        [SerializeField] protected TextMeshProUGUI Name;

        [SerializeField] protected HealthBarUI HealthBarUI;
    }
}