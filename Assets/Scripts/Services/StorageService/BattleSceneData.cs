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
            public EntityData enemy;
            public PlayerInfo playerInfo;

            public BattleSceneData(EntityData enemy, PlayerInfo playerInfo)
            {
                this.enemy = enemy;
                this.playerInfo = playerInfo;
            }
        }
    }
}


