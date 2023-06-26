using System.Collections.Generic;
using UnityEngine;
using Services.StorageService;

namespace CardSystem
{
    public class Croupier : MonoBehaviour
    {
        private int _cardSlotCount;
        private int _cardsInEachSlot;
        [SerializeField] private GameObject _viewPrefab;
        [SerializeField] private Transform _deckPlace;
        [SerializeField] private PlaceholderSpacer _deckSpacer;

        private Deck<ICard> _deck;
        private List<ICardPlaceholder> _startSlots = new();
        private List<ITableCard> _tableCards = new();

        public void SetTableSettings(int cardInEachSlot, int cardSlotCount, Deck<ICard> deck)
        {
            _cardsInEachSlot = cardInEachSlot;
            _cardSlotCount = cardSlotCount;
            _deck = deck;
        }

        public void DealGivenCards(TableData data)
        {
            SetTableSettings(data.cardsInEachSlot, data.cardSlotCount, data.deck.GetData());
            DealCards(data.ConvertAndGetCards<IPlayingCard>());
        }
        public void DealRandom—ards()
        {
            _deck.Shuffle();
            CardsInTable<IPlayingCard> cardStacks = new();
            cardStacks.stacks = new();
            for (int i = 0; i < _cardSlotCount; i++)
            {
                cardStacks.stacks.Add(new());
                for (int j = 0; j < _cardsInEachSlot; j++)
                {
                    cardStacks[i].Add(new PlayingCard(_deck.GetTopCard(), j == _cardsInEachSlot - 1 ? false : true));
                }
            }
            DealCards(cardStacks);
        }

        public TableData GetCurrentTable() 
        {
            TableData currentTable = new();
            currentTable.cardSlotCount = _cardSlotCount;
            currentTable.cardsInEachSlot = _cardsInEachSlot;
            currentTable.cardsInTable.stacks = new();
            int stackNumber = 0;
            foreach (ICardPlaceholder cardPlaceholder in _startSlots) 
            { 
                ITableCard currentCard = cardPlaceholder.tableCard;
                currentTable.cardsInTable.stacks.Add(new());
                while(currentCard != null) 
                {
                    currentTable.cardsInTable[stackNumber].Add(
                        new SavebleCard(currentCard.card));
                    currentCard = currentCard.nextCard;
                }
                stackNumber++;
            }
            currentTable.deck.SetData(_deck);
            return currentTable;
        }

        private void DealCards(CardsInTable<IPlayingCard> cardStacks)
        {
            TakeAllCardsFromTable();
            _startSlots = _deckSpacer.CreatePlaceholders(cardStacks.stackCount);
            CreateCardViews(cardStacks.cardCount - _tableCards.Count);

            int cardCount = cardStacks.cardCount;   
            for(int i = 0; i < cardStacks.stackCount; i++) 
            {
                ICardPlaceholder topOfStack = _startSlots[i];
                foreach(IPlayingCard card in cardStacks[i]) 
                {
                    cardCount--;
                    _tableCards[cardCount].SetNewCard(card);
                    topOfStack.TryPlaceCard(_tableCards[cardCount]);
                    topOfStack = _tableCards[cardCount].TopCardPlace;
                }
            }
        }

        private void CreateCardViews(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ITableCard newCard = Instantiate(_viewPrefab, _deckPlace.transform).GetComponent<ITableCard>();
                _tableCards.Add(newCard);
            }
        }

        private void TakeAllCardsFromTable()
        {
            _deck.ReturnAllCardsToDeck();
            foreach (ITableCard tableCard in _tableCards)
            {
                tableCard.rectTransform.SetParent(_deckPlace);
                tableCard.rectTransform.localPosition = Vector3.zero;
            }
        }
    }
}
