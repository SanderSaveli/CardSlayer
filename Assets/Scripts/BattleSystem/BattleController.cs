using CardSystem;
using Services.StorageService;
using System.Collections.Generic;
using UnityEngine;
using BattleSystem.Field;
namespace BattleSystem
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private EntityData _stubEnemy;
        [SerializeField] private EntityView _enemyView;
        [SerializeField] private Croupier _croupier;
        [SerializeField] private FillableBoundedGrid fieldGrid;
        private Field.Field _field;
        public Enemy enemy { get; private set; }
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
            CreateViewToentity(sceneData.enemy);
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
            SceneLoader.instance.LoadScene(SceneNames.Map);
        }

        private void CreateViewToentity(EntityData entity) 
        {
            enemy = new Enemy(entity, new Vector2Int(3, 1), _field);
            EntityView newView = Instantiate(_enemyView.gameObject).GetComponent<EntityView>();
            newView.entity = enemy;
            enemy.OnDie += EnemyDie;
        }
        private BattleSceneData GenerateStubSceneData()
        {
            Debug.LogWarning("Loaded stub scene data!");
            return new BattleSceneData(_stubEnemy, new PlayerInfo());
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
}


