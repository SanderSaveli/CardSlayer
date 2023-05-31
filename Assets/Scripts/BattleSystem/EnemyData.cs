using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Battle/new EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string enumyName;
        public Sprite portrait;

        public int maxHealth;
    }
}

