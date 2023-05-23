using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    [System.Serializable]
    public class Deck<T> where T : ICard
    {
        [SerializeField]
        public Queue<T> _cardsInDeck { get; set; }
        [SerializeField]
        public List<T> _cardsOutDeck { get; set; }
        public Deck() 
        { 
        
        }
        public Deck(Queue<T> cardsInDeck)
        {
            _cardsInDeck = new(cardsInDeck);
            _cardsOutDeck = new();
        }
        public Deck(Queue<T> cardsInDeck, List<T> cardsOutDeck)
        {
            _cardsInDeck = new(cardsInDeck);
            _cardsOutDeck = new(cardsOutDeck);
        }

        public void Shuffle()
        {
            System.Random random = new System.Random();
            List<T> values = new List<T>(_cardsInDeck);

            int n = values.Count;
            Debug.Log(n);
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = values[k];
                values[k] = values[n];
                values[n] = value;
            }

            _cardsInDeck.Clear();
            foreach (T value in values)
            {
                _cardsInDeck.Enqueue(value);
            }
        }

        public T GetTopCard()
        {
            if (_cardsInDeck.Count > 0)
            {
                T temp = _cardsInDeck.Dequeue();
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
                _cardsInDeck.Enqueue(card);
            }
            _cardsOutDeck.Clear();
        }
    }
}
