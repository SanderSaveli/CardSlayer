using CardSystem;
using System.Collections.Generic;

namespace EventBusSystem
{
    public interface IPlayerDropCardHandler : IGlobalSubscriber
    {
        public void DropCard(List<ICard> droppedCards);
    }
}


