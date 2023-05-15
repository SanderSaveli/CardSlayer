namespace CardSystem 
{
    public class Card : ICard
    {
        public Card(Suits suit, CardValues value) 
        { 
            this.suit = suit;
            this.value = value;
        }
        public Suits suit { get; private set; }

        public CardValues value { get; private set; }
    }

}
