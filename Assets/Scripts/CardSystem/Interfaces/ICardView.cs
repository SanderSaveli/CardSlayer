using UnityEngine;

namespace CardSystem
{
    public interface ICardView
    {
        public RectTransform rectTransform { get; }

        public IPlayingCard card { get; set; }
    }
}
