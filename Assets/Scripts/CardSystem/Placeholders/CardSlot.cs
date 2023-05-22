using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{
    public class CardSlot : MonoBehaviour, ICardPlaceholder, IDropHandler
    {
        public  RectTransform rectTransform { get; private set; }

        public ITableCard tableCard { get; private set; }

        private void Awake()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }
        public bool TryPlaceCard(ITableCard tableCard)
        {
            UpdateCardPosition(tableCard);
            this.tableCard = tableCard;
            return true;
        }

        public bool TryReplaceCard(ITableCard tableCard)
        {
            UpdateCardPosition(tableCard);
            tableCard.card.BottomCardChanged(null);
            this.tableCard = tableCard;
            return true;
        }

        public void UpdateCardPosition(ITableCard tableCard)
        {
            CardFitter.FitCard(tableCard, this, Vector2.zero); 
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

