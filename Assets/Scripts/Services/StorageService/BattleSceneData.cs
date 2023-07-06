using BattleSystem;
using UnityEngine;
using System;

namespace Services 
{
    namespace StorageService
    {
        public class BattleSceneData : ISceneData
        {
            public Type dataType { get => typeof(BattleSceneData); }

            //public BattleSceneData()
            public EntityDataSO enemy;
            public PlayerInfo playerInfo;

            public BattleSceneData(EntityDataSO enemy, PlayerInfo playerInfo)
            {
                if(enemy == null) 
                {
                    Debug.LogWarning("Enemy data is null!");
                }
                this.enemy = enemy;
                this.playerInfo = playerInfo;
            }
        }
    }
}


