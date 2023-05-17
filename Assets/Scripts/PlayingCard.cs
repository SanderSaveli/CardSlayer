using CardSystem;

public class PlayingCard : IPlayingCard
{
    public event IPlayingCard.CardTurned OnCardTurned;
    public event IPlayingCard.CardUnlocked OnCardUnlock;
    public event IPlayingCard.CardLocked OnCardLock;

    public PlayingCard(ICard card, bool isFaceDown)
    {
        this.suit = card.suit;
        this.value = card.value;
        this.isFaceDown = isFaceDown;
        isUnlock = !isFaceDown;
    }
    public Suits suit { get; private set; }

    public CardValues value { get; private set; }

    public IPlayingCard topCard { get; private set; }

    public IPlayingCard bottomCard { get; private set; }

    public bool isFaceDown { get; private set; }

    public bool isUnlock
    {
        get { return _isUnlock; }
        private set
        {
            if (value != _isUnlock)
            {
                _isUnlock = value;
                if (_isUnlock)
                {
                    OnCardUnlock?.Invoke();
                }
                else
                {
                    OnCardLock?.Invoke();
                }
            }
        }
    }
    private bool _isUnlock;

    public void TopCardRemoved()
    {
        topCard = null;
        if (!isUnlock)
        {
            UnlockCard();
        }

        if (isFaceDown)
        {
            isFaceDown = false;
            OnCardTurned?.Invoke();
        }
    }
    public void BottomCardChanged(IPlayingCard card)
    {
        if (bottomCard != null)
        {
            bottomCard.TopCardRemoved();
        }
        bottomCard = card;
    }
    public bool TryPutCardOnTop(IPlayingCard card)
    {
        if (card.isUnlock)
        {
            if (card.value == value - 1)
            {
                TopCardPutted(card);
                if (card.suit != suit)
                {
                    LockCard();
                }
                return true;
            }
        }
        return false;
    }

    public void TopCardPutted(IPlayingCard card)
    {
        card.BottomCardChanged(this);
        topCard = card;
        if (card.value != value - 1 || card.suit != suit)
        {
            LockCard();
        }
    }

    public void LockCard()
    {
        isUnlock = false;
        if (bottomCard != null && bottomCard.isUnlock)
        {
            bottomCard.LockCard();
        }
    }

    public void UnlockCard()
    {
        isUnlock = true;
        if (bottomCard != null && bottomCard.suit == suit && bottomCard.value - 1 == value && bottomCard.isUnlock)
        {
            bottomCard.UnlockCard();
        }
    }
}
