using UnityEngine;

public class FillableBoundedGrid : BoundedGrid
{
    public GameObject filler;
    protected override void UpdateGrid()
    {
        base.UpdateGrid();
        if (_rectTransform.childCount != collums * rows)
        {
            SetCarrectChildCount();
        }
    }
    private void SetCarrectChildCount()
    {
        int requiredChildCount = collums * rows;
        int childCount = _rectTransform.childCount;
        if (_rectTransform.childCount < requiredChildCount)
        {
            while (childCount < requiredChildCount)
            {
                Instantiate(filler, _rectTransform);
                childCount++;
            }
        }
        else
        {
            while (childCount > requiredChildCount)
            {
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    DestroyImmediate(_rectTransform.GetChild(_rectTransform.childCount - 1).gameObject);
                };
                childCount--;
            }
        }
    }
}
