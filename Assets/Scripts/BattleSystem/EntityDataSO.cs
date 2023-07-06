using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "EntityData", menuName = "Battle/new EntityData")]
    public class EntityDataSO : ScriptableObject
    {
        public string enumyName;
        public Sprite portrait;

        public int maxHealth;
    }
}

