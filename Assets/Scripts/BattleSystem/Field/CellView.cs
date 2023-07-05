using UnityEngine;

namespace BattleSystem.Field 
{
    public class CellView : MonoBehaviour, ICellView
    {
        public RectTransform rectTransform { get; private set; }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }
}

