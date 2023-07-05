using BattleSystem;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem.Field 
{
    public interface IFieldEntity
    {
        #region Events
        public delegate void statsChange(Enemy enemy);
        public event statsChange OnTakeDamage;
        public event statsChange OnDie;
        public event statsChange OnMove;
        #endregion

        public EntityData data { get; set; }
        public int currentHealth { get;}
        public Vector2Int position { get; }
        public List<ICell> ocupatedCells { get; }
    }
}

