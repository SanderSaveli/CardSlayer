using CardSystem;

namespace BattleSystem
{
    public class PlayerInfo
    {
        public int maxHealh;
        public int currentHealth;
        public int startSlots;
        public int cardsInEatchSlot;
        public Deck<ICard> deck;

        public PlayerInfo(int maxHealh, int currentHealth, int startSlots, int cardsInEatchSlot, Deck<ICard> deck)
        {
            this.maxHealh = maxHealh;
            this.currentHealth = currentHealth;
            this.startSlots = startSlots;
            this.cardsInEatchSlot = cardsInEatchSlot;
            this.deck = deck;
        }

         public PlayerInfo() 
        {
            this.maxHealh = PlayerStartStats.maxHealh;
            this.currentHealth = PlayerStartStats.currentHealth;
            this.startSlots = PlayerStartStats.startSlots;
            this.cardsInEatchSlot = PlayerStartStats.cardsInEatchSlot;
            this.deck = PlayerStartStats.GenerateStartDeck();
        }
    }
}

