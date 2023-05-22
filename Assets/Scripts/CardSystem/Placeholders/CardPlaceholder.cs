using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{
    public class CardPlaceholder : MonoBehaviour, ICardPlaceholder, IDropHandler
    {
        public delegate void CardPutted(ITableCard card);
        public event CardPutted OnCardPutted;
        public RectTransform rectTransform { get; private set; }
        public ITableCard tableCard { get; private set; }
        private float topCardOffset = 15f;
        public IPlayingCard card;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        public bool TryReplaceCard(ITableCard tableCard)
        {
            if (card.TryPutCardOnTop(tableCard.card))
            {
                UpdateCardPosition(tableCard);
                OnCardPutted?.Invoke(tableCard);
                this.tableCard = tableCard;
                return true;
            }
            return false;
        }


        public bool TryPlaceCard(ITableCard tableCard)
        {
            card.TopCardPutted(tableCard.card);
            UpdateCardPosition(tableCard);
            OnCardPutted?.Invoke(tableCard);
            this.tableCard = tableCard;
            return true;
        }

        public void UpdateCardPosition(ITableCard tableCard)
        {
            CardFitter.FitCard(tableCard, this, new Vector2(0, -topCardOffset));
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

