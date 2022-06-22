namespace GameCore
{
    public abstract class Repository
    {
        public abstract void Init();
        public abstract void OnStart();

        public abstract void OnCreate();
    }
}