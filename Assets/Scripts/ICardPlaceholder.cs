using UnityEngine;

namespace CardSystem
{
    public interface ICardPlaceholder
    {
        public RectTransform rectTransform { get; }
        public bool TryPlaceCard(ICardView cardView);

        public bool TryReplaceCard(ICardView cardView);

        public void UpdateCardPosition(ICardView cardView);
    }
}
