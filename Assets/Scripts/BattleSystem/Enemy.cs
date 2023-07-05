using BattleSystem.Field;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    public class Enemy : IFieldEntity
    {
        public EntityData data { get; set; }

        public int currentHealth { get; private set; }

        public Vector2Int position { get; private set; }

        public List<ICell> ocupatedCells { get; private set; }

        public event IFieldEntity.statsChange OnTakeDamage;
        public event IFieldEntity.statsChange OnDie;
        public event IFieldEntity.statsChange OnMove;

        private Field.Field field;

        public Enemy(EntityData data, Vector2Int position, Field.Field field)
        {
            this.data = data;
            currentHealth = data.maxHealth;
            this.position = position;
            this.field = field;
            ocupatedCells = new List<ICell>();
            SetOcupatedCells();
        }

        public void MakeDamage(int damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, data.maxHealth);
            OnTakeDamage?.Invoke(this);
            if (currentHealth <= 0)
            {
                OnDie?.Invoke(this);
            }
        }

        public bool TryMove(Vector2Int toPosition)
        {
            if (field[toPosition].TryPlaceOnCell(this))
            {
                position = toPosition;
                SetOcupatedCells();
                return true;
            }
            return false;
        }

        private void SetOcupatedCells()
        {
            ocupatedCells.Clear();
            ocupatedCells.Add(field[position]);
        }
    }
}

