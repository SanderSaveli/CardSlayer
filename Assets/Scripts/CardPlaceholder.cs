using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{
    public class CardPlaceholder : MonoBehaviour, ICardPlaceholder, IDropHandler
    {
        private RectTransform _rectTransform;

        public CardPlaceholder(RectTransform rectTransform)
        {
            _rectTransform = rectTransform;
        }

        private void Awake()
        {
            _rectTransform = gameObject.GetComponent<RectTransform>();
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
            cardView.rectTransform.SetParent(_rectTransform);
            cardView.rectTransform.localPosition = Vector3.zero;
            cardView.placeholder = this;
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

