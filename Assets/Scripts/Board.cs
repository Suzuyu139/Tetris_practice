using UnityEditor.TextCore.Text;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] Transform _emptySpriteTf = null;
    [SerializeField] int _height = 30, _width = 10, _header = 8;

    Transform[,] _grid = null;

    private void Awake()
    {
        _grid = new Transform[_width, _height];
    }

    private void Start()
    {
        CreateBoard();
    }

    void CreateBoard()
    {
        if (!_emptySpriteTf)
        {
            return;
        }

        for (int y = 0; y < _height - _header; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                Transform clone = Instantiate(_emptySpriteTf, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }

    public bool CheckPosition(Block block)
    {
        foreach (Transform item in block.transform)
        {
            Vector2 pos = Rounding.Round(item.position);

            if (!BoardOutCheck((int)pos.x, (int)pos.y))
            {
                return false;
            }

            if (BlockCheck((int)pos.x, (int)pos.y, block))
            {
                return false;
            }
        }
        return true;
    }

    bool BoardOutCheck(int x, int y)
    {
        return (x >= 0 && x < _width && y >= 0);
    }

    bool BlockCheck(int x, int y, Block block)
    {
        return (_grid[x, y] != null && _grid[x, y].parent != block.transform);
    }

    public void SaveBlockInGrid(Block block)
    {
        foreach (Transform item in block.transform)
        {
            Vector2 pos = Rounding.Round(item.position);
            _grid[(int)pos.x, (int)pos.y] = item;
        }
    }

    public void ClearAllRows()
    {
        for (int y = 0; y < _height; y++)
        {
            if (IsComplete(y))
            {
                ClearRow(y);
                ShiftRowsDown(y + 1);
                y--;
            }
        }
    }

    bool IsComplete(int y)
    {
        for (int x = 0; x < _width; x++)
        {
            if (_grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    void ClearRow(int y)
    {
        for (int x = 0; x < _width; x++)
        {
            if (_grid[x, y] != null)
            {
                Destroy(_grid[x, y].gameObject);
            }
            _grid[x, y] = null;
        }
    }

    void ShiftRowsDown(int startY)
    {
        for (int y = startY; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (_grid[x, y] != null)
                {
                    _grid[x, y - 1] = _grid[x, y];
                    _grid[x, y] = null;
                    _grid[x, y - 1].position += Vector3.down;
                }
            }
        }
    }

    public bool OverLimit(Block block)
    {
        foreach (Transform item in block.transform)
        {
            if (item.transform.position.y >= _height - _header)
            {
                return true;
            }
        }
        return false;
    }
}
