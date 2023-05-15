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

        private IPlayingCard _card;
        private Texture2D CardFace;
        private Texture2D CardBack;
        private Image image;
        private Canvas canvas;
        private float top—ardOffset = 5f;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            card = null;
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

        public void MoveCard(Vector2 position)
        {
            rectTransform.position = position;
        }

        public bool TryReplaceCard (ICardView cardView)
        {
            if (card.TryReplaceCard(cardView.card)) 
            {
                PlaceCard(cardView);
                return true;
            }
            return false;
        }


        public bool TryPlaceCard(ICardView cardView)
        {
            card.PlaceCard(cardView.card);
            return true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            rectTransform.localPosition = Vector3.zero;
        }

        private void PlaceCard(ICardView view) 
        {
            view.rectTransform.SetParent(rectTransform);
            view.rectTransform.localPosition = new Vector3(0, -top—ardOffset, 0);
        }   
        private void NewCardSetted()
        {
            card.OnCardUnlock += UnlockCard;
            card.OnCardTurned += TurnOnCard;
            card.OnCardLock += LockCard;

            Debug.Log(Resources.Load("Cards/ace_of_hearts"));
            CardFace = (Texture2D)Resources.Load(GetCardFacePath(card));
            CardBack = (Texture2D)Resources.Load("CardBack");
            if (card.isFaceDown)
            {
                image.sprite = Sprite.Create((CardBack), new Rect(0, 0, CardBack.width, CardBack.height), Vector2.zero); 
            }
            else
            {
                image.sprite = Sprite.Create((CardFace), new Rect(0, 0, CardFace.width, CardFace.height), Vector2.zero);
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
            image.sprite = Sprite.Create((CardFace), new Rect(0, 0, CardFace.width, CardFace.height), Vector2.zero); ;
        }

        private void LockCard()
        {
            image.color = Color.gray;
        }

        private string GetCardFacePath(ICard card) 
        {
            //Debug.Log("Cards/" + ConvertValue(card.value) + "_of_" + card.suit.ToString());
            return "Cards/ace_of_hearts";
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
                    return CardValues.ace.ToString();
                case (CardValues.queen):
                    return CardValues.ace.ToString();
                case (CardValues.king):
                    return CardValues.ace.ToString();
            }
            return "";
        }
        public void OnDrop(PointerEventData eventData)
        {
            if(eventData.pointerDrag.gameObject.TryGetComponent(out ICardView cardView)) 
            {
                TryReplaceCard(cardView);
            }
        }
    }
}

