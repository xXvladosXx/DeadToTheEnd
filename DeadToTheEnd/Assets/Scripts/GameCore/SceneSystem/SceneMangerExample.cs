namespace GameCore.SceneSystem
{
    public sealed class SceneManagerExample : SceneManagerBase
    {
        public override void InitScenesConfig()
        {
            SceneConfigs[SceneExample.SCENE_NAME] = new SceneExample();
        }
    }
}