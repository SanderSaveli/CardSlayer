using UnityEngine;

namespace CardSystem
{
    public interface ITableCard
    {
        public RectTransform rectTransform { get; }
        public IPlayingCard card { get; }
        public ITableCard nextCard { get; }
        public ICardPlaceholder placeholder { get; set; }

        public ICardPlaceholder TopCardPlace { get; }

        public void SetNewCard(IPlayingCard card);

        public void MoveCard(Vector3 position);

        public void BottomCardStartMove();
        public void BottomCardStopMove();
    }
}

