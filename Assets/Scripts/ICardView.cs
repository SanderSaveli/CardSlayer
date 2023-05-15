using UnityEngine;

namespace CardSystem 
{ 
    public interface ICardView
    {
        public RectTransform rectTransform { get; }
        public IPlayingCard card { get; set; }
        public void MoveCard(Vector2 position);
    }

}
