using UnityEngine;

namespace CardSystem
{
    public interface ICardPlaceholder
    {
        public RectTransform rectTransform { get; }
        public ITableCard tableCard { get; }
        public bool TryPlaceCard(ITableCard cardView);

        public bool TryReplaceCard(ITableCard cardView);

        public void UpdateCardPosition(ITableCard cardView);
    }
}
