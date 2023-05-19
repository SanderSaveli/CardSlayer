using UnityEngine;
using UnityEngine.UI;

namespace CardSystem
{
    public class CardView : MonoBehaviour, ICardView
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
        private Texture2D _cardFace;
        private Texture2D _cardBack;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            rectTransform = GetComponent<RectTransform>();
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

        private void NewCardSetted()
        {
            card.OnCardUnlock += UnlockCard;
            card.OnCardTurned += TurnOnCard;
            card.OnCardLock += LockCard;

            _cardFace = (Texture2D)Resources.Load(GetCardFacePath(card));
            _cardBack = (Texture2D)Resources.Load("CardBack");
            if (card.isFaceDown)
            {
                _image.sprite = Sprite.Create(_cardBack, new Rect(0, 0, _cardBack.width, _cardBack.height), Vector2.zero);
            }
            else
            {
                _image.sprite = Sprite.Create(_cardFace, new Rect(0, 0, _cardFace.width, _cardFace.height), Vector2.zero);
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
            _image.color = Color.white;
        }

        private void TurnOnCard()
        {
            _image.sprite = Sprite.Create(_cardFace, new Rect(0, 0, _cardFace.width, _cardFace.height), Vector2.zero); ;
        }

        private void LockCard()
        {
            _image.color = Color.gray;
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

