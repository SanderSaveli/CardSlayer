using CardSystem;
using UnityEngine;

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
    }
    public Suits suit { get; private set; }

    public CardValues value { get; private set; }

    public IPlayingCard topCard { get; private set; }

    public IPlayingCard bottomCard { get; private set; }

    public bool isFaceDown { get; private set; }

    public bool isUnlock { get { return isUnlock; } 
        private set { 
            if(value != isUnlock) 
            {
                isUnlock = value;
                if (isUnlock) 
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

    public void TopCardRemoved()
    {
        topCard = null;
        if (!isUnlock)
        {
            isUnlock = true;
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
            bottomCard = card;
        }
    }
    public bool TryReplaceCard(IPlayingCard card)
    {
        if (card.value - 1 == value)
        {
            PlaceCard(card);
            if (card.suit != suit)
            {
                isUnlock = false;
            }
            return true;
        }
        return false;
    }

    public void PlaceCard(IPlayingCard card)
    {
        card.BottomCardChanged(this);
        topCard = card;
    }
}
