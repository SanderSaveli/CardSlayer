using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(GridLayoutGroup))]
[RequireComponent(typeof(RectTransform))]
public class BoundedGrid : MonoBehaviour
{
    [SerializeField] private int _collums = 1;
    [SerializeField] private int _rows = 1;

    public RectOffset paddings;
    public Vector2 spacing;

    private RectTransform _rectTransform;
    private GridLayoutGroup _grid;

    private void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
        _grid = GetComponent<GridLayoutGroup>();
    }

    private void OnValidate()
    {
        UpdateGrid();
    }
    private void UpdateGrid()
    {
        CalculateWidthAndHeight(out float areaWidth, out float areaHeight);
        float widthCoefficent = areaWidth / _collums;
        float heightCoefficent = areaHeight / _rows;

        float sqareSide = heightCoefficent > widthCoefficent ?
            CalculateSizebyWidth(areaWidth) :
            CalculateSizebyHeight(areaHeight);
        SetGridPreferences(new Vector2(sqareSide, sqareSide));
    }

    private float CalculateSizebyHeight(float height)
    {
        float reductionFactor = paddings.top + paddings.bottom + (spacing.y* (_rows -1));
        return (height - reductionFactor) / _rows;
    }

    private float CalculateSizebyWidth(float width)
    {
        float reductionFactor = paddings.right + paddings.left + (spacing.x * (_collums - 1));
        return (width - reductionFactor) / _collums;
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
