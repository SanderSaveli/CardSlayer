using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "EntityData", menuName = "Battle/new EntityData")]
    public class EntityData : ScriptableObject
    {
        public string enumyName;
        public Sprite portrait;

        public int maxHealth;
    }
}

