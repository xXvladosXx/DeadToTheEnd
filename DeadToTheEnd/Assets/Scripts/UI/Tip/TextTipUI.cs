using GameCore;
using TMPro;
using UnityEngine;

namespace UI.Tip
{
    public class TextTipUI : UIElement
    {
        [SerializeField] private TextMeshProUGUI _text;
        public override void OnCreate(InteractorsBase interactorsBase)
        {
            
        }


        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}