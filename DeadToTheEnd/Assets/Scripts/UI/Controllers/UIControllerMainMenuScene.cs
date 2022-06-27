using System.Linq;
using GameCore;

namespace UI.Controllers
{
    public class UIControllerMainMenuScene : UIController
    {
        public override void SendMessageOnCreate(InteractorsBase interactorsBase)
        {
        }

        public override void SendMessageOnStart(InteractorsBase interactorsBase)
        {
        }

        protected override void Init(InteractorsBase interactorsBase)
        {
            gameObject.SetActive(true);
            
            UIElements = GetComponentsInChildren<UIElement>().ToList();
            foreach (var uiElement in UIElements)
            {
                uiElement.OnCreate(interactorsBase);
            }
        }
    }
}