using BattleSystem.Field;
using CardSystem;
using Services.StorageService;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private MinorEnemyDatabaseSO _minorEnemies;
        [SerializeField] private EntityView _enemyView;
        [SerializeField] private Croupier _croupier;
        [SerializeField] private FillableBoundedGrid fieldGrid;
        private Field.Field _field;
        public Enemy enemy { get; private set; }
        public Player player { get; private set; }
        public PlayerInfo playerInfo { get; private set; }


        private void Awake()
        {
            BattleSceneData sceneData = (BattleSceneData)SceneLoader.instance.currentSceneData;
            _field = new Field.Field(fieldGrid);
            if (sceneData == null)
            {
                sceneData = GenerateStubSceneData();
            }
            playerInfo = sceneData.playerInfo;
            CreateViewToEntity(sceneData.enemy);
            CreateViewToPlayer();
        }

        private void Start()
        {
            _croupier.SetTableSettings(playerInfo.cardsInEatchSlot, playerInfo.startSlots, playerInfo.deck);
            _croupier.DealRandom—ards();
        }

        private void OnDisable()
        {
            enemy.OnDie -= EnemyDie;
        }
        public void AttackByCard(List<ICard> cards)
        {
            enemy.MakeDamage(cards.Count);
            _croupier.DealRandom—ards();
        }
        public void BackToMap()
        {
            SceneLoader.instance.LoadTransistor();
            SceneLoader.instance.LoadScene(SceneNames.Map);
        }

        private void CreateViewToEntity(EntityDataSO entity)
        {
            enemy = new Enemy(entity, new Vector2Int(3, 0), _field);
            EntityView newView = Instantiate(_enemyView.gameObject).GetComponent<EntityView>();
            newView.entity = enemy;
            enemy.OnDie += EnemyDie;
        }
        private void CreateViewToPlayer()
        {
            this.player = new Player(playerInfo, new Vector2Int(0, 0), _field);
            EntityView newView = Instantiate(_enemyView.gameObject).GetComponent<EntityView>();
            newView.entity = player;
        }
        private BattleSceneData GenerateStubSceneData()
        {
            Debug.LogWarning("Loaded stub scene data!");
            int index = Random.Range(0, _minorEnemies._minorEnemies.Count);
            return new BattleSceneData(_minorEnemies._minorEnemies[index], new PlayerInfo());
        }

        private void EnemyDie(Enemy enemy)
        {
            Debug.Log("You kill enemy!");
            BackToMap();
        }

        private void PlayerDie(Enemy enemy)
        {
            Debug.Log("You dead!");
            BackToMap();
        }

        public void LoadData(TableData battleData)
        {

        }
    }
}


