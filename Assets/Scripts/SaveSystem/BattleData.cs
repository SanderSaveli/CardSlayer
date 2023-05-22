using CardSystem;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace SaveSystem
{
    [System.Serializable]
    public class BattleData
    {
        [Serialize] public List<List<SavebleCard>> CardsInTable = new();

        [Serialize] public SaveableDeck deck = new();
    }
}
