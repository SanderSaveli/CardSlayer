using System;

namespace CardSystem
{
    [System.Serializable]
    public class SavebleCard : ICard
    {
        public Suits suit { get; set; }
        public CardValues value { get; set; }
        public bool isFaceDown;
        public bool isUnlock;
        public Type type;

        public SavebleCard()
        { }
        public SavebleCard(ICard card)
        {
            suit = card.suit;
            value = card.value;
            type = card.GetType();
        }

        public SavebleCard(IPlayingCard card)
        {
            suit = card.suit;
            value = card.value;
            isFaceDown = card.isFaceDown;
            isUnlock = card.isUnlock;
            type = card.GetType();
        }

        public ICard Get()
        {
            ICard card = (ICard)Activator.CreateInstance(type);
            card.SetData(this);
            return card;
        }

        public void SetData(SavebleCard data)
        {
            {
                suit = data.suit;
                value = data.value;
                isFaceDown = data.isFaceDown;
                isUnlock = data.isFaceDown;
                type = data.GetType();
            }
        }
    }

}
