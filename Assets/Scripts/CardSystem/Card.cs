namespace CardSystem 
{
    public class Card : ICard
    {
        public Card() 
        { 
        
        }
        public Card(ICard card) 
        { 
            suit = card.suit;
            value = card.value;
        }
        public Card(Suits suit, CardValues value) 
        { 
            this.suit = suit;
            this.value = value;
        }
        public Suits suit { get; private set; }

        public CardValues value { get; private set; }

        public void SetData(SavebleCard data)
        {
            suit = data.suit;
            value = data.value;
        }
    }

}
