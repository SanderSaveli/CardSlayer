using EventBusSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{
    public class AttackSlot : MonoBehaviour, ICardPlaceholder, IDropHandler
    {
        [SerializeField] private Croupier croupier;
        public RectTransform rectTransform { get; private set; }

        public ITableCard tableCard { get; private set; }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.gameObject.TryGetComponent(out ITableCard cardView))
            {
                TryReplaceCard(cardView);
            }
        }

        public bool TryPlaceCard(ITableCard cardView)
        {
            return false;
        }

        public bool TryReplaceCard(ITableCard cardView)
        {
            if (cardView.card.isUnlock) 
            { 
                IPlayingCard curr = cardView.card;
                List<ICard> droppedCards = new();
                while(curr != null) 
                {
                    droppedCards.Add(curr);
                    curr = curr.topCard;
                }
                EventBus.RaiseEvent<IPlayerDropCardHandler>(it => it.DropCard(droppedCards));
                croupier.DealRandom—ards();
                return true;
            }
            return false;
        }

        public void UpdateCardPosition(ITableCard cardView)
        {
            CardFitter.FitCard(cardView, this, Vector2.zero);
        }
    }
}

