using BattleSystem;
using System;

namespace Services 
{
    namespace StorageService
    {
        public class BattleSceneData : ISceneData
        {
            public Type dataType { get => typeof(BattleSceneData); }

            //public BattleSceneData()
            public EnemyData enemy;
            public PlayerInfo playerInfo;

            public BattleSceneData(EnemyData enemy, PlayerInfo playerInfo)
            {
                this.enemy = enemy;
                this.playerInfo = playerInfo;
            }
        }
    }
}


