using UnityEngine;

namespace CardSystem
{
    public static class CardFitter
    {
        public static void FitCard(ITableCard cardView, ICardPlaceholder placeholder, Vector2 offset)
        {
            cardView.rectTransform.SetParent(placeholder.rectTransform);
            cardView.MoveCard((Vector3) offset);
            cardView.rectTransform.sizeDelta = placeholder.rectTransform.sizeDelta;
            cardView.rectTransform.localScale = Vector3.one;
            cardView.placeholder = placeholder;
        }
    }
}

