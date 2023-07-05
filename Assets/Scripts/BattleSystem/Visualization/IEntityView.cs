using UnityEngine;

namespace BattleSystem.Field
{
    public interface IEntityView
    {
        public RectTransform view { get; set; }
        public IFieldEntity entity { get; set; }
    }
}

