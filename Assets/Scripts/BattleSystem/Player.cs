using BattleSystem.Field;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    public class Player : IFieldEntity
    {
        public EntityDataSO data { get; private set; }

        public int currentHealth { get; private set; }

        public Vector2Int position { get; private set; }

        public List<ICell> ocupatedCells { get; private set; }

        public event IFieldEntity.statsChange OnTakeDamage;
        public event IFieldEntity.statsChange OnDie;
        public event IFieldEntity.statsChange OnMove;

        private Field.Field field;

        public Player(PlayerInfo info, Vector2Int position, Field.Field field)
        {
            data = info.playerData;
            currentHealth = info.currentHealth;
            this.position = position;
            this.field = field;
            ocupatedCells = new List<ICell>();
            SetOcupatedCells();
        }

        private void SetOcupatedCells()
        {
            ocupatedCells.Clear();
            ocupatedCells.Add(field[position]);
        }
    }
}

