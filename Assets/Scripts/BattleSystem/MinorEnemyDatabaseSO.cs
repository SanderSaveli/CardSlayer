using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "MinorEnemyDatabase", menuName = "Battle/new MinorEnemyDatabase")]
    public class MinorEnemyDatabaseSO : ScriptableObject
    {
        public List<EntityDataSO> _minorEnemies;
    }
}


