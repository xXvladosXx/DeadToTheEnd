namespace GameCore.SceneSystem
{
    public class SceneManagerFight: SceneManagerBase
    {
        public override void InitScenesConfig()
        {
            SceneConfigs[SceneFightConfig.SCENE_NAME] = new SceneFightConfig();
        }
    }
}