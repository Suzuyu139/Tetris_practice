using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Spawner _spawner = null;
    Block _activeBlock = null;

    [SerializeField] float _dropInterval = 0.25f;
    float _nextDropTimer = 0.0f;

    Board _board = null;

    float _nextKeyDownTimer = 0.0f, _nextKeyLeftRightTimer = 0.0f, _nextKeyRotateTimer = 0.0f;
    [SerializeField] float _nextKeyDownInterval = 0.02f, _nextKetLeftRightInterval = 0.25f, _nextKeyRotateInterval = 0.25f;

    [SerializeField] GameObject _gameOverPanel = null;
    bool _gameOver = false;

    private void Start()
    {
        _spawner = GameObject.FindFirstObjectByType<Spawner>();
        _board = GameObject.FindFirstObjectByType<Board>();

        _spawner.transform.position = Rounding.Round(_spawner.transform.position);

        _nextKeyDownTimer = Time.time + _nextKeyDownInterval;
        _nextKeyLeftRightTimer = Time.time + _nextKetLeftRightInterval;
        _nextKeyRotateTimer = Time.time + _nextKeyRotateInterval;

        if (!_activeBlock)
        {
            _activeBlock = _spawner.SpawnBlock();
        }

        if (_gameOverPanel.activeInHierarchy)
        {
            _gameOverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (_gameOver)
        {
            return;
        }

        PlayerInput();
    }

    void PlayerInput()
    {
        if (Input.GetKey(KeyCode.D) && (Time.time > _nextKeyLeftRightTimer) || Input.GetKeyDown(KeyCode.D))
        {
            _activeBlock.MoveRight();
            _nextKeyLeftRightTimer = Time.time + _nextKetLeftRightInterval;
            if (!_board.CheckPosition(_activeBlock))
            {
                _activeBlock.MoveLeft();
            }
        }
        else if (Input.GetKey(KeyCode.A) && (Time.time > _nextKeyLeftRightTimer) || Input.GetKeyDown(KeyCode.A))
        {
            _activeBlock.MoveLeft();
            _nextKeyLeftRightTimer = Time.time + _nextKetLeftRightInterval;
            if (!_board.CheckPosition(_activeBlock))
            {
                _activeBlock.MoveRight();
            }
        }
        else if (Input.GetKey(KeyCode.E) && (Time.time > _nextKeyRotateTimer))
        {
            _activeBlock.RotateRight();
            _nextKeyRotateTimer = Time.time + _nextKeyRotateInterval;
            if (!_board.CheckPosition(_activeBlock))
            {
                _activeBlock.RotateLeft();
            }
        }
        else if (Input.GetKey(KeyCode.S) && (Time.time > _nextKeyDownTimer) || (Time.time > _nextDropTimer))
        {
            _activeBlock.MoveDown();
            _nextKeyDownTimer = Time.time + _nextKeyDownInterval;
            _nextDropTimer = Time.time + _dropInterval;
            if (!_board.CheckPosition(_activeBlock))
            {
                if (_board.OverLimit(_activeBlock))
                {
                    GameOver();
                }
                else
                {
                    BottomBoard();
                }
            }
        }
    }

    void BottomBoard()
    {
        _activeBlock.MoveUp();
        _board.SaveBlockInGrid(_activeBlock);
        _activeBlock = _spawner.SpawnBlock();
        _nextKeyDownTimer = Time.time;
        _nextKeyLeftRightTimer = Time.time;
        _nextKeyRotateTimer = Time.time;
        _board.ClearAllRows();
    }

    void GameOver()
    {
        _activeBlock.MoveUp();
        if (!_gameOverPanel.activeInHierarchy)
        {
            _gameOverPanel.SetActive(true);
        }

        _gameOver = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
