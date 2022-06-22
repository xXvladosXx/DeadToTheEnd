using GameCore;

namespace UI
{
    public interface ICaptureEvents
    {
        void OnCreate(InteractorsBase interactorsBase);
        void OnInitialize();
        void OnStart();
    }
}