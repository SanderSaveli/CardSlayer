using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    public class Croupier : MonoBehaviour
    {
        [SerializeField] private int _cardSlotCount;
        [SerializeField] private int _cardsInEachSlot;
        [SerializeField] private Transform _slotsPlace;
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private GameObject viewPrefab;
        [SerializeField] private Canvas canvas;

        private Deck<ICard> _deck;
        private List<ICardPlaceholder> _startSlots = new();
        private List<ITableCard> _tableCards = new();

        private void Start()
        {
            GenerateDeck();
            SetSlots(_cardSlotCount);
            Deal—ards();
        }
        public void Deal—ards()
        {
            _deck.Shuffle();
            TakeAllCardsFromTable();
            List<ICardPlaceholder> topOfCardStacks = new();
            foreach (ICardPlaceholder slot in _startSlots)
            {
                topOfCardStacks.Add(slot);
            }
            int cardsCount = topOfCardStacks.Count * _cardsInEachSlot;
            if (_tableCards.Count < cardsCount)
            {
                CreateCardViews(cardsCount - _tableCards.Count);
            }
            for (int i = 0; i < _cardsInEachSlot; i++)
            {
                for (int j = 0; j < topOfCardStacks.Count; j++)
                {
                    cardsCount--;
                    _tableCards[cardsCount].SetNewCard(new PlayingCard(_deck.GetTopCard(), i== _cardsInEachSlot-1? false: true));
                    topOfCardStacks[j].TryPlaceCard(_tableCards[cardsCount]);
                    topOfCardStacks[j] = _tableCards[cardsCount].TopCardPlace;
                }
            }

        }
        private void GenerateDeck()
        {
            List<ICard> cardsInDeck = new();
            for (int i = 1; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cardsInDeck.Add(new Card((Suits)j, (CardValues)i));
                }
            }
            _deck = new Deck<ICard>(cardsInDeck);
        }

        private void SetSlots(int count) 
        { 
            if(count > _startSlots.Count) 
            {
                for(int i = _startSlots.Count; i < count; i++) 
                {
                    _startSlots.Add(CreateNewSlot());
                }
            }
            else 
            {
                for(int i = count; i > _startSlots.Count; i--) 
                {
                    ICardPlaceholder destroyed = _startSlots[i -1];
                    _startSlots.Remove(destroyed);
                    Destroy(destroyed.rectTransform);
                }
            }
        }

        private ICardPlaceholder CreateNewSlot() 
        {
            GameObject newSlot = Instantiate(_slotPrefab, _slotsPlace);
            return newSlot.GetComponent<ICardPlaceholder>();
        }
        private void CreateCardViews(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ITableCard newCard = Instantiate(viewPrefab, canvas.transform).GetComponent<ITableCard>();
                _tableCards.Add(newCard);
            }
        }

        private void TakeAllCardsFromTable() 
        {
            _deck.ReturnAllCardsToDeck();
            foreach(ITableCard tableCard in _tableCards) 
            {
                tableCard.rectTransform.SetParent(transform);
                tableCard.rectTransform.localPosition = Vector3.zero;
            }
        }
    }
}
