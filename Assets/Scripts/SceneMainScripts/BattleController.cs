using BattleSystem;
using LoadService;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField]private EnemyView enemyView;
    private Enemy enemy;

    private void Awake()
    {
        BattleSceneData sceneData = (BattleSceneData)SceneLoader.instance.currentSceneData;
        enemy = new Enemy(sceneData.enemy);
        enemyView.enemy = enemy;
    }
    public void BackToMap()
    {
        SceneLoader.instance.LoadScene(SceneNames.Map);
    }
}
