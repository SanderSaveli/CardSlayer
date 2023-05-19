using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardSystem
{
    public class MovableCard : MonoBehaviour, IMovable
    {
        #region Events
        public delegate void CardStartMove();
        public event CardStartMove OnCardStartMove;

        public delegate void CardEndMove();
        public event CardEndMove OnCardEndMove;
        #endregion

        private RectTransform _rectTransform;
        private Canvas _canvas;
        private Image _image;

        private SmoothMove smoothMove;
        private SmoothFoolowParent followParent;
        public bool isMoving { get; set; }

        private void Awake()
        {
            _canvas = GameObject.FindGameObjectWithTag("CardCanvas").GetComponent<Canvas>();
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
            smoothMove = gameObject.AddComponent<SmoothMove>();
            followParent = gameObject.AddComponent<SmoothFoolowParent>();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (isMoving)
            {
                smoothMove.StopMove();
                OnCardStartMove?.Invoke();
                transform.SetParent(_canvas.transform);
                transform.SetAsLastSibling();
                _image.raycastTarget = false;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isMoving)
            {
                _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isMoving)
            {
                _image.raycastTarget = true;
                OnCardEndMove?.Invoke();
            }
        }

        public void MoveTo(Vector3 position) 
        {
            smoothMove.MoveTo(position);
        }

        public void StartFollowParent() 
        {
            followParent.StartFollow();
        }

        public void StopFollowParent() 
        { 
            followParent.StopFollow();
        }
    }
}
