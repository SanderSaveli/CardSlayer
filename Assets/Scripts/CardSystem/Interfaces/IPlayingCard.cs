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

        public delegate void PutTopCard(IPlayingCard card);
        public event PutTopCard OnTopCardPutted;

        public delegate void RemoveTopCard();
        public event RemoveTopCard OnTopCardRemoved;
        #endregion
        public IPlayingCard bottomCard { get; }
        public IPlayingCard topCard { get; }
        public bool isFaceDown { get; }
        public bool isUnlock { get; }

        public void TopCardRemoved();

        public bool TryPutCardOnTop(IPlayingCard card);

        public void TopCardPutted(IPlayingCard card);

        public void BottomCardChanged(IPlayingCard card);

        public void LockCard();

        public void UnlockCard();   
    }
}
