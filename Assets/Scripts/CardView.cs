using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardSystem
{
    public class CardView : MonoBehaviour, ICardView, ICardPlaceholder, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
    {
        public RectTransform rectTransform { get; private set; }
        public IPlayingCard card
        {

            get => _card;
            set
            {
                _card = value;
                NewCardSetted();
            }
        }
        public ICardPlaceholder placeholder { get; set; }

        private IPlayingCard _card;
        private Texture2D CardFace;
        private Texture2D CardBack;
        private Image image;
        private Canvas canvas;
        private float top—ardOffset = 15f;

        private SmoothMove smoothMove;
        private SmoothFoolowParent followParent;

        private Vector3 _positionBeforeDrag;

        private void Awake()
        {
            image = GetComponent<Image>();
            rectTransform = GetComponent<RectTransform>();
            canvas = GameObject.FindGameObjectWithTag("CardCanvas").GetComponent<Canvas>();
            smoothMove = GetComponent<SmoothMove>();
            followParent = GetComponent<SmoothFoolowParent>();
        }

        private void OnDisable()
        {
            if (card != null)
            {
                card.OnCardTurned -= TurnOnCard;
                card.OnCardUnlock -= UnlockCard;
                card.OnCardLock -= LockCard;
            }
        }

        public void MoveCard(Vector3 position)
        {
            smoothMove.MoveTo(position);
        }

        public bool TryReplaceCard(ICardView cardView)
        {
            if (card.TryPutCardOnTop(cardView.card))
            {
                UpdateCardPosition(cardView);
                return true;
            }
            return false;
        }


        public bool TryPlaceCard(ICardView cardView)
        {
            card.TopCardPutted(cardView.card);
            UpdateCardPosition(cardView);
            return true;
        }

        public void UpdateCardPosition(ICardView cardView)
        {
            CardFitter.FitCard(cardView, this, new Vector2(0, -top—ardOffset));
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (card.isUnlock)
            {
                transform.SetParent(canvas.transform);
                transform.SetAsLastSibling();
                image.raycastTarget = false;
                smoothMove.StopMove();
                followParent.StopFollow();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (card.isUnlock)
            {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (card.isUnlock)
            {
                image.raycastTarget = true;
                placeholder.UpdateCardPosition(this);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (card.isUnlock)
            {
                if (eventData.pointerDrag.gameObject.TryGetComponent(out ICardView cardView))
                {
                    TryReplaceCard(cardView);
                }
            }
        }

        private void NewCardSetted()
        {
            card.OnCardUnlock += UnlockCard;
            card.OnCardTurned += TurnOnCard;
            card.OnCardLock += LockCard;

            CardFace = (Texture2D)Resources.Load(GetCardFacePath(card));
            CardBack = (Texture2D)Resources.Load("CardBack");
            if (card.isFaceDown)
            {
                image.sprite = Sprite.Create(CardBack, new Rect(0, 0, CardBack.width, CardBack.height), Vector2.zero);
            }
            else
            {
                image.sprite = Sprite.Create(CardFace, new Rect(0, 0, CardFace.width, CardFace.height), Vector2.zero);
            }
            if (card.isUnlock)
            {
                UnlockCard();
            }
            else
            {
                LockCard();
            }
        }

        private void UnlockCard()
        {
            image.color = Color.white;
        }

        private void TurnOnCard()
        {
            image.sprite = Sprite.Create(CardFace, new Rect(0, 0, CardFace.width, CardFace.height), Vector2.zero); ;
        }

        private void LockCard()
        {
            image.color = Color.gray;
        }

        private string GetCardFacePath(ICard card)
        {
            return "Cards/" + ConvertValue(card.value) + "_of_" + card.suit.ToString();
        }

        private string ConvertValue(CardValues value)
        {
            switch (value)
            {
                case (CardValues.ace):
                    return CardValues.ace.ToString();
                case (CardValues.two):
                    return 2.ToString();
                case (CardValues.three):
                    return 3.ToString();
                case (CardValues.four):
                    return 4.ToString();
                case (CardValues.five):
                    return 5.ToString();
                case (CardValues.six):
                    return 6.ToString();
                case (CardValues.seven):
                    return 7.ToString();
                case (CardValues.eight):
                    return 8.ToString();
                case (CardValues.nine):
                    return 9.ToString();
                case (CardValues.ten):
                    return 10.ToString();
                case (CardValues.jack):
                    return CardValues.jack.ToString();
                case (CardValues.queen):
                    return CardValues.queen.ToString();
                case (CardValues.king):
                    return CardValues.king.ToString();
            }
            return "";
        }
    }
}

