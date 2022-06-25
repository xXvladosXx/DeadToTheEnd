namespace GameCore.SceneSystem
{
    public sealed class SceneManagerStart : SceneManagerBase
    {
        public override void InitScenesConfig()
        {
            SceneConfigs[SceneStartConfig.SCENE_NAME] = new SceneStartConfig();
        }
    }
}