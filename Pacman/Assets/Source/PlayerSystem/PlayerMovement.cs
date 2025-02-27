using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Tilemap tilemap;
    public string wallTileName = "Wall";

    private Vector3Int _currentCell;
    private Vector2 _targetDirection;
    private bool _isMoving = false;
    private GameManager _gameManager;

    void Start()
    {
        _currentCell = tilemap.WorldToCell(transform.position);
        SnapToGrid(); // Важно: привязываем к сетке при старте
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GameManager not found!");
        }
    }

    void Update()
    {
        if (GameManager.IsGameOver) return;
        Vector2 inputDirection = PlayerInput.Instance.MoveDirection;

        if (!_isMoving && inputDirection != Vector2.zero)
        {
            TryMove(inputDirection);
        }
    }

    void FixedUpdate()
    {
        if (GameManager.IsGameOver) return;

        if (_isMoving)
        {
            MoveTowardsTarget();
            //SnapToGrid(); // Привязываем к сетке ПОСЛЕ MoveTowardsTarget
        }
    }

    void TryMove(Vector2 direction)
    {
        Vector3Int targetCell = _currentCell + Vector3Int.RoundToInt(new Vector3(direction.x, direction.y, 0));
        if (IsCellWalkable(targetCell))
        {
            _targetDirection = direction;
            _isMoving = true;
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 targetPosition = tilemap.GetCellCenterWorld(_currentCell + Vector3Int.RoundToInt(new Vector3(_targetDirection.x, _targetDirection.y, 0)));
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            _currentCell += Vector3Int.RoundToInt(new Vector3(_targetDirection.x, _targetDirection.y, 0));
            SnapToGrid(); // Важно! Привязываем к сетке, когда достигли цели

            _isMoving = false;
        }

    }

    void SnapToGrid()
    {
        transform.position = tilemap.GetCellCenterWorld(_currentCell);
    }


    bool IsCellWalkable(Vector3Int cell)
    {
        return tilemap.GetTile(cell) == null || tilemap.GetTile(cell).name != wallTileName;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Point"))
        {
            _gameManager.CollectPoint(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            _gameManager.PlayerDied();
        }
    }
}