using CardSystem;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartStats
{
    public static BattleSystem.EntityDataSO playerData { get => GenerateStartPlayerData(); }
    public static readonly int currentHealth = playerData.maxHealth;
    public static readonly int startSlots = 7;
    public static readonly int cardsInEatchSlot = 4;

    public static readonly int suitNumber = 4;
    public static readonly int sameSuitCardNumber = 10;
    public static Deck<ICard> GenerateStartDeck() 
    {
        Queue<ICard> cardsInDeck = new();
        for (int i = 1; i < sameSuitCardNumber; i++)
        {
            for (int j = 0; j < suitNumber; j++)
            {
                cardsInDeck.Enqueue(new Card((Suits)j, (CardValues)i));
            }
        }
        return new Deck<ICard>(cardsInDeck);
    }
    public static BattleSystem.EntityDataSO GenerateStartPlayerData() 
    {
        return (BattleSystem.EntityDataSO)Resources.Load("Player");
    }
}
