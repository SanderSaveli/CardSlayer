using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{
    public class CardPlaceholder : MonoBehaviour, ICardPlaceholder, IDropHandler
    {
        public  RectTransform rectTransform { get; private set; }

        private void Awake()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }
        public bool TryPlaceCard(ICardView cardView)
        {
            UpdateCardPosition(cardView);
            return true;
        }

        public bool TryReplaceCard(ICardView cardView)
        {
            UpdateCardPosition(cardView);
            cardView.card.BottomCardChanged(null);
            return true;
        }

        public void UpdateCardPosition(ICardView cardView)
        {
            CardFitter.FitCard(cardView, this, Vector2.zero); 
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.gameObject.TryGetComponent(out ICardView cardView))
            {
                TryReplaceCard(cardView);
            }
        }
    }
}

