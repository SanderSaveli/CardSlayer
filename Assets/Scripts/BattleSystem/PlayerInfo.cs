using CardSystem;

namespace BattleSystem
{
    public class PlayerInfo
    {
        public EntityDataSO playerData;
        public int currentHealth;
        public int startSlots;
        public int cardsInEatchSlot;
        public Deck<ICard> deck;

        public PlayerInfo(EntityDataSO playerData, int currentHealth, int startSlots, int cardsInEatchSlot, Deck<ICard> deck)
        {
            this.playerData = playerData;
            this.currentHealth = currentHealth;
            this.startSlots = startSlots;
            this.cardsInEatchSlot = cardsInEatchSlot;
            this.deck = deck;
        }

         public PlayerInfo() 
        {
            this.playerData = PlayerStartStats.playerData;
            this.currentHealth = PlayerStartStats.currentHealth;
            this.startSlots = PlayerStartStats.startSlots;
            this.cardsInEatchSlot = PlayerStartStats.cardsInEatchSlot;
            this.deck = PlayerStartStats.GenerateStartDeck();
        }
    }
}

