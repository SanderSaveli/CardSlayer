using CardSystem;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Services.StorageService
{
    [System.Serializable]
    public class SaveableDeck
    {
        [Serialize] public Queue<SavebleCard> _cardsInDeck = new();
        [Serialize] public List<SavebleCard> _cardsOutDeck = new();

        public void SetData(Deck<ICard> deck) 
        {
            _cardsInDeck = new Queue<SavebleCard>();
            foreach (ICard card in deck._cardsInDeck)
            {
                _cardsInDeck.Enqueue(new SavebleCard(card));
            }
            foreach (ICard card in deck._cardsOutDeck)
            {
                _cardsOutDeck.Add(new SavebleCard(card));
            }
        }

        public Deck<ICard> GetData() 
        {
            Queue<ICard> cardsInDeck = new ();
            List<ICard> cardsOutDeck = new ();
            foreach (SavebleCard card in _cardsInDeck) 
            {
                cardsInDeck.Enqueue(card.Get());
            }
            foreach (SavebleCard card in _cardsOutDeck) 
            {
                cardsOutDeck.Add(card.Get());
            }
            return new Deck<ICard>(cardsInDeck, cardsOutDeck);
        }
    }
}

