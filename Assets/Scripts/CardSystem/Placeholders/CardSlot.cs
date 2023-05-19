using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{
    public class CardSlot : MonoBehaviour, ICardPlaceholder, IDropHandler
    {
        public  RectTransform rectTransform { get; private set; }

        private void Awake()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }
        public bool TryPlaceCard(ITableCard cardView)
        {
            UpdateCardPosition(cardView);
            return true;
        }

        public bool TryReplaceCard(ITableCard cardView)
        {
            UpdateCardPosition(cardView);
            cardView.card.BottomCardChanged(null);
            return true;
        }

        public void UpdateCardPosition(ITableCard cardView)
        {
            CardFitter.FitCard(cardView, this, Vector2.zero); 
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.gameObject.TryGetComponent(out ITableCard cardView))
            {
                TryReplaceCard(cardView);
            }
        }
    }
}

