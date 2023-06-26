using CardSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Services.StorageService
{
    public class TableLoader : MonoBehaviour
    {
        public Croupier croupier;
        public BattleController battleController;
        private IStoregeService _storageService;
        private string _batttleDataKey = "BattleDta";

        private void Start()
        {
            _storageService =
               ServiceLockator.instance.GetService<IStoregeService>();
        }
        public void Save()
        {
            TableData data = croupier.GetCurrentTable();
            Queue<ICard> IN = new();
            List<ICard> OUT = new();
            foreach (ICard card in data.deck._cardsInDeck)
            {
                IN.Enqueue(new Card(card));
            }
            foreach (ICard card in data.deck._cardsOutDeck)
            {
                OUT.Add(new Card(card));
            }

            Deck<ICard> deck = new(IN, OUT);
            data.deck.SetData(deck);
            _storageService.Save(_batttleDataKey, data);
        }

        public void Load()
        {
            _storageService.Load<TableData>(_batttleDataKey, croupier.DealGivenCards);
        }
    }
}
