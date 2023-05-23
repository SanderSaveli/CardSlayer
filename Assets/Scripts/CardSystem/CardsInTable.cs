using CardSystem;
using System.Collections.Generic;

[System.Serializable]
public struct CardsInTable<T> where T : ICard
{
    public List<List<T>> stacks;
    public int cardCount { get {
            int count = 0;
            foreach(List<T> stack in stacks) 
            { 
                count += stack.Count;
            }
            return count;
        } }
    public int stackCount { get => stacks.Count; }
    public CardsInTable(List<List<T>> cardStacks)
    {
        stacks = cardStacks;
    }

    public List<T> this[int index]
    {
        get => stacks[index];
        set => stacks[index] = value;
    }
}
