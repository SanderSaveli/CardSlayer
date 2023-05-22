using System.Collections.Generic;
using UnityEngine;
using SaveSystem;

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
            DealRandom—ards();
        }

        public void SetTableSettings(int cardInEachSlot, int cardSlotCount)
        {
            _cardsInEachSlot = cardInEachSlot;
            _cardSlotCount = cardSlotCount;
            SetSlots(_cardSlotCount);
        }

        public void DealGivenCards(BattleData data)
        {
            _deck.ReturnAllCardsToDeck();
            _deck = data.deck.GetData();
            List<List<IPlayingCard>> CardsInTable = new();
            for (int i = 0; i < data.CardsInTable.Count; i++) 
            {
                CardsInTable.Add(new List<IPlayingCard>());
                for (int j = 0; j < data.CardsInTable[i].Count; j++) 
                {
                    CardsInTable[i].Add((IPlayingCard)data.CardsInTable[i][j].Get());
                }
            }
            DealCards(CardsInTable);
        }
        public void DealRandom—ards()
        {
            _deck.Shuffle();
            List<List<IPlayingCard>> cardStacks = new();
            for (int i = 0; i < _cardSlotCount; i++)
            {
                cardStacks.Add(new());
                for (int j = 0; j < _cardsInEachSlot; j++)
                {
                    cardStacks[i].Add( new PlayingCard(_deck.GetTopCard(), j == _cardsInEachSlot - 1 ? false : true));
                }
            }
            DealCards(cardStacks);
        }

        public BattleData GetCurrentTable() 
        {
            BattleData currentTable = new();
            currentTable.CardsInTable = new();
            List<List<IPlayingCard>> cardStacks = new();
            int stackNumber = 0;
            int cardNumber;
            foreach (ICardPlaceholder cardPlaceholder in _startSlots) 
            { 
                ITableCard currentCard = cardPlaceholder.tableCard;
                cardNumber = 0;
                cardStacks.Add(new());
                while(currentCard != null) 
                {
                    cardStacks[stackNumber].Add(currentCard.card);
                    currentCard = currentCard.nextCard;
                    cardNumber++;
                }
                stackNumber++;
            }

            for (int i = 0; i < cardStacks.Count; i++)
            {
                currentTable.CardsInTable.Add(new List<SavebleCard>());
                for (int j = 0; j < cardStacks[i].Count; j++)
                {
                    currentTable.CardsInTable[i].Add(new SavebleCard(cardStacks[i][j]));
                }
            }
            currentTable.deck.SetData(_deck);
            return currentTable;
        }

        private void DealCards(List<List<IPlayingCard>> cardStacks)
        {
            TakeAllCardsFromTable();
            SetSlots(cardStacks.Count);
            int stackNumber = 0;
            int cardCount = 0;
            foreach (List<IPlayingCard> stack in cardStacks)
            {
                cardCount += stack.Count;
            }

            if (_tableCards.Count < cardCount)
            {
                CreateCardViews(cardCount - _tableCards.Count);
            }
            foreach (List<IPlayingCard> stack in cardStacks)
            {
                ICardPlaceholder topOfStack = _startSlots[stackNumber];
                foreach (IPlayingCard card in stack)
                {
                    cardCount--;
                    _tableCards[cardCount].SetNewCard(card);
                    topOfStack.TryPlaceCard(_tableCards[cardCount]);
                    topOfStack = _tableCards[cardCount].TopCardPlace;
                }
                stackNumber++;
            }
        }
        private void GenerateDeck()
        {
            Queue<ICard> cardsInDeck = new();
            for (int i = 1; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cardsInDeck.Enqueue(new Card((Suits)j, (CardValues)i));
                }
            }
            _deck = new Deck<ICard>(cardsInDeck);
        }

        private void SetSlots(int count)
        {
            if (count > _startSlots.Count)
            {
                for (int i = _startSlots.Count; i < count; i++)
                {
                    _startSlots.Add(CreateNewSlot());
                }
            }
            else
            {
                for (int i = count; i > _startSlots.Count; i--)
                {
                    ICardPlaceholder destroyed = _startSlots[i - 1];
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
            foreach (ITableCard tableCard in _tableCards)
            {
                tableCard.rectTransform.SetParent(transform);
                tableCard.rectTransform.localPosition = Vector3.zero;
            }
        }
    }
}
