using UnityEngine;

namespace CardSystem 
{
    public interface IPlayingCard : ICard
    {
        #region Events
        public delegate void CardTurned();
        public event CardTurned OnCardTurned;

        public delegate void CardLocked();
        public event CardLocked OnCardLock;

        public delegate void CardUnlocked();
        public event CardUnlocked OnCardUnlock;
        #endregion
        public IPlayingCard topCard { get; }
        public IPlayingCard bottomCard { get; }
        public bool isFaceDown { get; }
        public bool isUnlock { get; }

        public void TopCardRemoved();

        public bool TryPutCardOnTop(IPlayingCard card);

        public void TopCardPutted(IPlayingCard card);

        public void BottomCardChanged(IPlayingCard card);

        public void LockCard();
    }
}
