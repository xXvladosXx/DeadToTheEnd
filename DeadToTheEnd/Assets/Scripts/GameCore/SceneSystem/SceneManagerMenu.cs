namespace GameCore.SceneSystem
{
    public class SceneManagerMenu : SceneManagerBase
    {
        public override void InitScenesConfig()
        {
            SceneConfigs[SceneMainMenuConfig.SCENE_NAME] = new SceneMainMenuConfig();
        }
    }
}