using UnityEngine;

namespace BattleSystem.Field
{
    public class Cell : ICell
    {
        public bool isEmpty => cellEntity == null;
        public Vector2 position { get; set; }
        public IFieldEntity cellEntity { get; set; }

        public ICellView cellView { get; set; }

        public bool TryPlaceOnCell(IFieldEntity entity)
        {
            if (isEmpty)
            {
                cellEntity = entity;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LeftTheCell(IFieldEntity entity)
        {
            if (cellEntity == entity)
            {
                cellEntity = null;
            }
        }
    }

}

