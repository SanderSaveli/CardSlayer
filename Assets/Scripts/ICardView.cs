using UnityEngine;
using System.Collections;

namespace CardSystem 
{ 
    public interface ICardView
    {
        public RectTransform rectTransform { get; }
        public IPlayingCard card { get; set; }

        public ICardPlaceholder placeholder { get; set; }
        public void MoveCard(Vector3 position);
    }
}
