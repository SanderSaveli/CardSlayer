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
                int count = 0;
                //while(curr != null) 
                //{
                    //count++;
                    //curr = curr.topCard;
                //}
                Debug.Log("You deal " + count + " damage!");
                croupier.DealRandom�ards();
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

