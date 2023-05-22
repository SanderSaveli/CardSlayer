using CardSystem;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SaveSystem
{
    public class SaveLoadService : MonoBehaviour
    {
        public Croupier croupier;
        private JSONToFileStorageService _storageService = new();
        private string _batttleDataKey = "BattleDta";

        public void Save()
        {
            BattleData data = croupier.GetCurrentTable();
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
            _storageService.Load<BattleData>(_batttleDataKey, croupier.DealGivenCards);
        }
    }
}
