using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{
    public class CardPlaceholder : MonoBehaviour, ICardPlaceholder, IDropHandler
    {
        public delegate void CardPutted(ITableCard card);
        public event CardPutted OnCardPutted;
        public RectTransform rectTransform { get; private set; }
        private float topCardOffset = 15f;
        public IPlayingCard card;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        public bool TryReplaceCard(ITableCard card)
        {
            if (this.card.TryPutCardOnTop(card.card))
            {
                UpdateCardPosition(card);
                OnCardPutted?.Invoke(card);
                return true;
            }
            return false;
        }


        public bool TryPlaceCard(ITableCard card)
        {
            this.card.TopCardPutted(card.card);
            UpdateCardPosition(card);
            OnCardPutted?.Invoke(card);
            return true;
        }

        public void UpdateCardPosition(ITableCard card)
        {
            CardFitter.FitCard(card, this, new Vector2(0, -topCardOffset));
        }


        public void OnDrop(PointerEventData eventData)
        {
            if (card.isUnlock)
            {
                if (eventData.pointerDrag.gameObject.TryGetComponent(out ITableCard cardView))
                {
                    TryReplaceCard(cardView);
                }
            }
        }
    }
}

