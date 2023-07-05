using UnityEngine;

namespace BattleSystem.Field 
{
    public interface ICell
    {
        public bool isEmpty { get; }
        public Vector2 position { get; }
        public IFieldEntity cellEntity { get; set; }
        public ICellView cellView { get; set; }

        public bool TryPlaceOnCell(IFieldEntity entity);
        public void LeftTheCell(IFieldEntity entity);
    }
}

