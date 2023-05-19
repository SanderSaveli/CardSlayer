
using System.Numerics;

namespace CardSystem 
{ 
    public interface ICard
    {
        public Suits suit { get; }
        public CardValues value { get; }
    }
}
