using BattleSystem;
using Services.StorageService;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public EntityDataSO enemy;
    public void ToMenu()
    {
        SceneLoader.instance.LoadScene(SceneNames.MainMenu);
    }

    public void Fight()
    {
        BattleSceneData battleData = new(enemy, new PlayerInfo());
        SceneLoader.instance.LoadScene(SceneNames.Battle, battleData);
    }
}
