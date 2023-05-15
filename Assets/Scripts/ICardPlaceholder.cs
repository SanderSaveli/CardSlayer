namespace CardSystem
{
    public interface ICardPlaceholder
    {
        public bool TryPlaceCard(ICardView cardView);
        public bool TryReplaceCard(ICardView cardView);

        public void UpdateCardPosition(ICardView cardView);
    }
}
