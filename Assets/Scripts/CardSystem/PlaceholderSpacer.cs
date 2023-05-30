using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    public class PlaceholderSpacer : MonoBehaviour
    {
        [SerializeField] private RectTransform _cardArea;
        [SerializeField] private float _maxPlaceholderScale = 1.5f;
        [SerializeField] private float _minPlaceholderScale = 0.3f;
        [SerializeField] private float _placeholderOffset = 30f;
        [SerializeField] private GameObject _placeholderPrefab;

        private List<ICardPlaceholder> _existPlaceholders = new();
        private float _cardAreaLength;
        private float _placeholderNormalLength;
        private float _placeholderNormalHeight;

        private void Awake()
        {
            if (_placeholderPrefab == null &&
                _placeholderPrefab.GetComponent<ICardPlaceholder>() == null)
            {
                Debug.LogError("Placeholder prefab missing!");
            }
            _cardAreaLength = Math.Abs(_cardArea.rect.max.x - _cardArea.rect.min.x);
            RectTransform rt = _placeholderPrefab.GetComponent<RectTransform>();
            _placeholderNormalLength = Math.Abs(rt.rect.max.x - rt.rect.min.x);
            _placeholderNormalHeight = Math.Abs(rt.rect.max.y - rt.rect.min.y);
        }

        public List<ICardPlaceholder> CreatePlaceholders(int count)
        {
            float maxSegmentLength = _placeholderNormalLength * _maxPlaceholderScale;
            float minSegmentLength = _placeholderNormalLength * _minPlaceholderScale;

            float segmentLength = (_cardAreaLength - (count - 1) * _placeholderOffset) / count;
            segmentLength = Mathf.Clamp(segmentLength, minSegmentLength, maxSegmentLength);
            float actualScaleFacktor = segmentLength / _placeholderNormalLength;

            float actualCardAreaLenth = segmentLength * count + _placeholderOffset * (count - 1);
            float yPosition = _cardArea.rect.max.y - (_placeholderNormalHeight * actualScaleFacktor) / 2;

            SetPlaceholders(count);
            for (int i = 0; i < count; i++)
            {
                float xPosition = (-actualCardAreaLenth + segmentLength) / 2 + (i * (segmentLength + _placeholderOffset));

                Vector2 coordinate = new Vector2(xPosition, yPosition);
                _existPlaceholders[i].rectTransform.localScale = new Vector3(actualScaleFacktor, actualScaleFacktor, 1);
                _existPlaceholders[i].rectTransform.localPosition = coordinate;
            }
            return _existPlaceholders;
        }

        private void SetPlaceholders(int count)
        {
            if (_existPlaceholders.Count < count)
            {
                for (int i = 0; count > _existPlaceholders.Count; i++)
                {
                    _existPlaceholders.Add(Instantiate(_placeholderPrefab, _cardArea).GetComponent<ICardPlaceholder>());
                }
            }
            else if (_existPlaceholders.Count > count)
            {
                for (int i = _existPlaceholders.Count; i > count; i--)
                {
                    ICardPlaceholder removeable = _existPlaceholders[i];
                    _existPlaceholders.Remove(removeable);
                    Destroy(removeable.rectTransform);
                }
            }
        }
    }

}
