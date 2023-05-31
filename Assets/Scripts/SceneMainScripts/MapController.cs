using BattleSystem;
using LoadService;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public EnemyData enemy;
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
