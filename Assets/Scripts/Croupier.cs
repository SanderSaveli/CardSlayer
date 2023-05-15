using System.Collections.Generic;
using UnityEngine;
namespace CardSystem
{
    public class Croupier : MonoBehaviour
    {
        private Deck<ICard> deck;

        public GameObject viewPrefab;
        [SerializeField] public GameObject[] emptySlots;
        [SerializeField] private int _cardsInEachSet;

        private List<ICardView> _cardViews;

        private void Start()
        {
            GenerateDeck();
            _cardViews = new();
            Deal—ards();
        }

        public void Deal—ards()
        {
            deck.Shuffle();
            List<ICardPlaceholder> topOfCardStacks = new();
            foreach (GameObject slot in emptySlots)
            {
                topOfCardStacks.Add(slot.GetComponent<ICardPlaceholder>());
            }
            int cardsCount = topOfCardStacks.Count * _cardsInEachSet;
            if (_cardViews.Count < cardsCount)
            {
                CreateCardViews(cardsCount - _cardViews.Count);
            }

            for (int i = 0; i < _cardsInEachSet; i++)
            {
                foreach (ICardPlaceholder stack in topOfCardStacks)
                {
                    cardsCount--;
                    _cardViews[cardsCount].card = new PlayingCard(deck.GetTopCard(), false);
                    stack.TryPlaceCard(_cardViews[cardsCount]);
                }
            }

        }
        private void GenerateDeck()
        {
            List<ICard> cardsInDeck = new();
            for (int i = 1; i < 14; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cardsInDeck.Add(new Card((Suits)j, (CardValues)i));
                }
            }
            deck = new Deck<ICard>(cardsInDeck);
        }

        private void CreateCardViews(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ICardView newView = Instantiate(viewPrefab).GetComponent<ICardView>();
                _cardViews.Add(newView);
            }
        }
    }
}
