using TMPro.Examples;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
[RequireComponent(typeof(RectTransform))]
public class BoundedGrid : MonoBehaviour
{
    public int collums = 1;
    public int rows = 1;

    public RectOffset paddings;
    public Vector2 spacing;

    protected RectTransform _rectTransform;
    private GridLayoutGroup _grid;
    private Vector2 startDeltaSize;

    private void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
        _grid = GetComponent<GridLayoutGroup>();
        startDeltaSize = _rectTransform.sizeDelta;
    }

    private void OnValidate()
    {
        math.clamp(collums, 0, 100);
        math.clamp(rows, 0, 100);
        UpdateGrid();
    }
    protected virtual void UpdateGrid()
    {
        if(_rectTransform == null) 
        {
            _rectTransform = GetComponent<RectTransform>();
            _grid = GetComponent<GridLayoutGroup>();
            startDeltaSize = _rectTransform.sizeDelta;
        }
        _rectTransform.sizeDelta = startDeltaSize;
        CalculateWidthAndHeight(out float areaWidth, out float areaHeight);
        float widthCoefficent = areaWidth / collums;
        float heightCoefficent = areaHeight / rows;

        float sqareSide = heightCoefficent > widthCoefficent ?
            CalculateSizebyWidth(areaWidth) :
            CalculateSizebyHeight(areaHeight);
        SetGridPreferences(new Vector2(sqareSide, sqareSide));
        CutFreeSpace(sqareSide);
    }

    private void CutFreeSpace(float sqareSide) 
    {
        Vector2 occupiedArea = new();
        occupiedArea.x = paddings.right + paddings.left + (spacing.x * (collums - 1)) + sqareSide * collums;
        occupiedArea.y = paddings.top + paddings.bottom + (spacing.y * (rows - 1)) + sqareSide * rows;
        CalculateWidthAndHeight(out float areaWidth, out float areaHeight);
        Vector2 emptySpace = new Vector2(areaWidth - occupiedArea.x, areaHeight - occupiedArea.y);
        _rectTransform.sizeDelta -= emptySpace;
    }

    private float CalculateSizebyHeight(float height)
    {
        float reductionFactor = paddings.top + paddings.bottom + (spacing.y* (rows -1));
        return (height - reductionFactor) / rows;
    }

    private float CalculateSizebyWidth(float width)
    {
        float reductionFactor = paddings.right + paddings.left + (spacing.x * (collums - 1));
        return (width - reductionFactor) / collums;
    }


    private void CalculateWidthAndHeight(out float width, out float height)
    {
        Vector3[] corners = new Vector3[4];
        _rectTransform.GetLocalCorners(corners);
        width = math.abs(corners[1].x - corners[2].x);
        height = math.abs(corners[0].y - corners[1].y);
    }

    private void SetGridPreferences(Vector2 cellSize)
    {
        _grid.cellSize = cellSize;
        _grid.padding = paddings;
        _grid.spacing = spacing;
    }
}
