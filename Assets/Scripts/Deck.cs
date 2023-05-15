using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    public class Deck<T> where T : ICard
    {
        private List<T> _cardsInDeck;
        private List<T> _cardsOutDeck;
        public Deck(List<T> cardsInDeck)
        {
            _cardsInDeck = new(cardsInDeck);
            _cardsOutDeck = new();
        }

        public void Shuffle()
        {
            System.Random rand = new System.Random();
            int n = _cardsInDeck.Count;
            while (n > 1)
            {
                int k = rand.Next(n);
                n--;
                T temp = _cardsInDeck[k];
                _cardsInDeck[k] = _cardsInDeck[n];
                _cardsInDeck[n] = temp;
            }
        }

        public T GetTopCard()
        {
            if (_cardsInDeck.Count > 0)
            {
                T temp = _cardsInDeck[0];
                _cardsInDeck.Remove(temp);
                _cardsOutDeck.Add(temp);
                return temp;
            }
            Debug.LogWarning("Deck is empty!");
            return default(T);
        }

        public void ReturnAllCardsToDeck()
        {
            foreach (T card in _cardsOutDeck)
            {
                _cardsInDeck.Add(card);
                _cardsOutDeck.Remove(card);
            }
        }
    }


}
