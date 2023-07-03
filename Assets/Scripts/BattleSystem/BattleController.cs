using CardSystem;
using Services.StorageService;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private EnemyData _stubEnemy;
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private Croupier _croupier;
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
            _enemyView.enemy = enemy;
            playerInfo = sceneData.playerInfo;
            enemy.OnDie += EnemyDie;
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


