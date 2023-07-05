using UnityEngine;

namespace BattleSystem.Field
{
    public class Field
    {
        private FillableBoundedGrid grid;
        public Field(FillableBoundedGrid grid) 
        {
            this.grid = grid;
            IniField();
        }
        public int fieldLenth { get; private set; }
        private ICell[,] _cells;

        public ICell this[int x, int y]
        {
            get { return _cells[x, y]; }
        }
        public ICell this[Vector2Int vector]
        {
            get { return _cells[vector.x, vector.y]; }
        }

        private void IniField() 
        {
            if (CheckForException()) 
            {
                fieldLenth = grid.collums;
                _cells = new ICell[fieldLenth, 2];
                for (int j = 0; j < 2; j++)
                {
                    for (int i = 0; i < fieldLenth; i++)
                    {
                        Cell newCell = new Cell();
                        newCell.cellView = grid.transform.GetChild(i + fieldLenth * j).GetComponent<ICellView>();
                        _cells[i,j] = newCell;
                    }
                }
            }
        }

        private bool CheckForException() 
        {
            if (grid.rows != 2)
            {
                Debug.LogError("Wrong grid settings, it has " + grid.rows + " rows. Must have 2 rows.");
                return false;
            }
            if(!grid.transform.GetChild(0).TryGetComponent(out ICellView cellView)) 
            {
                Debug.LogError("Grid's childrens does not have ICell interface");
                return false;
            }
            return true;
        }
    }
}

