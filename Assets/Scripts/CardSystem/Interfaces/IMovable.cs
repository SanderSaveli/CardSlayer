using UnityEngine.EventSystems;

public interface IMovable : IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool isMoving { get; set; }
}
