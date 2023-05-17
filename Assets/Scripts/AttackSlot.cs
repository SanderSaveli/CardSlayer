using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{
    public class AttackSlot : MonoBehaviour, ICardPlaceholder, IDropHandler
    {
        [SerializeField] private Croupier croupier;
        public RectTransform rectTransform { get; private set; }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.gameObject.TryGetComponent(out ICardView cardView))
            {
                TryReplaceCard(cardView);
            }
        }

        public bool TryPlaceCard(ICardView cardView)
        {
            return false;
        }

        public bool TryReplaceCard(ICardView cardView)
        {
            IPlayingCard curr = cardView.card;
            int count = 0;
            while(curr != null) 
            {
                count++;
                curr = curr.topCard;
            }
            Debug.Log("You deal " + count + " damage!");
            croupier.Deal—ards();
            return true;
        }

        public void UpdateCardPosition(ICardView cardView)
        {
            CardFitter.FitCard(cardView, this, Vector2.zero);
        }
    }
}

