using CardSystem;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace SaveSystem
{
    [System.Serializable]
    public class BattleData
    {
        [Serialize] public CardsInTable<SavebleCard> cardsInTable = new();

        [Serialize] public SaveableDeck deck = new();

        public void ConvertAndSetCards(CardsInTable<ICard> table) 
        {
            cardsInTable.stacks.Clear();
            for(int i =0; i < table.stackCount; i++) 
            {
                cardsInTable.stacks.Add(new List<SavebleCard>());
                for(int j =0; j < table[i].Count; j++) 
                {
                    cardsInTable[i].Add(new SavebleCard(table[i][j]));
                }
            }
        }

        public CardsInTable<T> ConvertAndGetCards<T>() where T : ICard
        {
            CardsInTable<T> table = new();
            table.stacks = new();
            for (int i = 0; i < cardsInTable.stackCount; i++)
            {
                table.stacks.Add(new List<T>());
                for (int j = 0; j < cardsInTable[i].Count; j++)
                {
                    table[i].Add((T)cardsInTable[i][j].Get());
                }
            }
            return table;
        }
    }
}
