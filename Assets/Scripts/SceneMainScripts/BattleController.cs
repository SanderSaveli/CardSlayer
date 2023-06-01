using BattleSystem;
using CardSystem;
using EventBusSystem;
using LoadService;
using SaveSystem;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour, IPlayerDropCardHandler
{
    [SerializeField] private EnemyData stubEnemy;
    [SerializeField] private EnemyView enemyView;
    [SerializeField] private Croupier croupier;
    public Enemy enemy { get; private set; }
    public PlayerInfo playerInfo { get; private set; }


    private void Awake()
    {
        BattleSceneData sceneData = (BattleSceneData)SceneLoader.instance.currentSceneData;
        if (sceneData == null)
        {
            sceneData = GenerateStubSceneData();
        }
        enemy = new Enemy(sceneData.enemy);
        enemyView.enemy = enemy;
        playerInfo = sceneData.playerInfo;
        enemy.OnDie += EnemyDie;
        EventBus.Subscribe(this);
    }

    private void Start()
    {
        croupier.SetTableSettings(playerInfo.cardsInEatchSlot, playerInfo.startSlots, playerInfo.deck);
        croupier.DealRandom—ards();
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
        enemy.OnDie -= EnemyDie;
    }
    public void BackToMap()
    {
        SceneLoader.instance.LoadScene(SceneNames.Map);
    }

    public void DropCard(List<ICard> droppedCards)
    {
        enemy.MakeDamage(droppedCards.Count);
    }

    private BattleSceneData GenerateStubSceneData()
    {
        Debug.LogWarning("Loaded stub scene data!");
        return new BattleSceneData(stubEnemy, new PlayerInfo());
    }

    private void EnemyDie(Enemy enemy)
    {
        Debug.Log("You kill enemy!");
        BackToMap();
    }

    public void LoadData(TableData battleData) 
    {
    
    }
}
