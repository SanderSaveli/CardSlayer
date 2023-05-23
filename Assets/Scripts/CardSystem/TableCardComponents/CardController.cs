using UnityEngine;

namespace CardSystem
{
    public class CardController : MonoBehaviour, ITableCard
    {
        public RectTransform rectTransform { get; private set; }

        public IPlayingCard card { get; private set; }

        public ITableCard nextCard { get; private set; }
        public ICardPlaceholder placeholder { get; set; }
        public ICardPlaceholder TopCardPlace { get => _cardPlaceholder; }

        private ICardView _cardView;

        private CardPlaceholder _cardPlaceholder;

        private MovableCard _move;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            _cardView = gameObject.AddComponent<CardView>();
            _move = gameObject.AddComponent<MovableCard>();
            _cardPlaceholder = gameObject.AddComponent<CardPlaceholder>();
        }
        private void OnEnable()
        {
            _move.OnCardStartMove += CardStartMove;
            _move.OnCardEndMove += CardEndMove;
            _cardPlaceholder.OnCardPutted += TopCardPutted;
        }
        private void OnDisable()
        {
            if (card != null)
            {
                card.OnCardTurned -= TurnOnCard;
                card.OnCardUnlock -= UnlockCard;
                card.OnCardLock -= LockCard;
                card.OnTopCardRemoved -= TopCardRemoved;
            }
            _move.OnCardStartMove -= CardStartMove;
            _move.OnCardEndMove -= CardEndMove;
            _cardPlaceholder.OnCardPutted -= TopCardPutted;
        }
        public void SetNewCard(IPlayingCard card)
        {
            this.card = card;
            _cardView.card = card;
            _cardPlaceholder.card = card;
            card.OnCardUnlock += UnlockCard;
            card.OnCardTurned += TurnOnCard;
            card.OnCardLock += LockCard;
            card.OnTopCardRemoved += TopCardRemoved;
            if (card.isUnlock) 
            {
                _move.isMoving = true;
            }
            _move.StopFollowParent();
        }

        public void MoveCard(Vector3 position)
        {
            _move.MoveTo(position);
        }

        public void BottomCardStartMove() 
        {
            _move.StartFollowParent();
            if(nextCard!= null) 
            { 
                nextCard.BottomCardStartMove();
            }
        }
        public void BottomCardStopMove() 
        {
            _move.StopFollowParent();
            if (nextCard != null)
            {
                nextCard.BottomCardStopMove();
            }
        }
        private void UnlockCard()
        {
            _move.isMoving = true;
        }

        private void TurnOnCard()
        {
            
        }

        private void LockCard()
        {
            _move.isMoving = false;
        }
        private void TopCardPutted(ITableCard card)
        {
            nextCard = card;
        }
        private void TopCardRemoved() 
        {
            nextCard = null;
        }

        private void CardStartMove() 
        {
            _move.StopFollowParent();
            if(nextCard != null)
            {
                nextCard.BottomCardStartMove();
            }
        }
        private void CardEndMove()
        {
            placeholder.UpdateCardPosition(this);
        }
    }
}
