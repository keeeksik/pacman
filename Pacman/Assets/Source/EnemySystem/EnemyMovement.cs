using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float chaseRadius = 5f;
    public Tilemap tilemap;
    public string wallTileName = "Wall";
    public float changeDirectionInterval = 2f;

    private Vector3Int _currentCell;
    private Vector2 _moveDirection;
    private GameObject _player;
    private float _timeSinceLastDirectionChange = 0f;
    private bool _isMoving = false;

    void Start()
    {
        _currentCell = tilemap.WorldToCell(transform.position);
        SnapToGrid(); // Привязываем к сетке при старте
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
        {
            Debug.LogError("Player not found!");
        }

        ChooseNewDirection();
    }

    void Update()
    {
        if (GameManager.IsGameOver) return;
        if (!_isMoving)
        {
            _timeSinceLastDirectionChange += Time.deltaTime;
            if (_timeSinceLastDirectionChange >= changeDirectionInterval)
            {
                ChooseNewDirection();
                _timeSinceLastDirectionChange = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        if (GameManager.IsGameOver) return;

        if (_isMoving)
        {
            MoveTowardsTarget();
        }

        if (_player != null && Vector2.Distance(transform.position, _player.transform.position) < chaseRadius)
        {
            ChasePlayer();
        }
    }

    void ChooseNewDirection()
    {
        Vector2[] possibleDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        System.Random rnd = new System.Random();
        for (int i = 0; i < possibleDirections.Length; i++)
        {
            int j = rnd.Next(possibleDirections.Length);
            Vector2 temp = possibleDirections[i];
            possibleDirections[i] = possibleDirections[j];
            possibleDirections[j] = temp;
        }

        foreach (Vector2 direction in possibleDirections)
        {
            Vector3Int targetCell = _currentCell + Vector3Int.RoundToInt(new Vector3(direction.x, direction.y, 0));
            if (IsCellWalkable(targetCell))
            {
                _moveDirection = direction;
                _isMoving = true;
                break;
            }
        }
    }

    void ChasePlayer()
    {
        if (_player == null) return;

        Vector3Int playerCell = tilemap.WorldToCell(_player.transform.position);
        Vector2 chaseDirection = Vector2.zero;

        if (playerCell.x > _currentCell.x && IsCellWalkable(_currentCell + Vector3Int.right))
        {
            chaseDirection = Vector2.right;
        }
        else if (playerCell.x < _currentCell.x && IsCellWalkable(_currentCell + Vector3Int.left))
        {
            chaseDirection = Vector2.left;
        }
        else if (playerCell.y > _currentCell.y && IsCellWalkable(_currentCell + Vector3Int.up))
        {
            chaseDirection = Vector2.up;
        }
        else if (playerCell.y < _currentCell.y && IsCellWalkable(_currentCell + Vector3Int.down))
        {
            chaseDirection = Vector2.down;
        }

        if (chaseDirection == Vector2.zero)
        {
            ChooseNewDirection();
            return;
        }

        _moveDirection = chaseDirection;
        _isMoving = true;
    }

    void MoveTowardsTarget()
    {
        Vector3 targetPosition = tilemap.GetCellCenterWorld(_currentCell + Vector3Int.RoundToInt(new Vector3(_moveDirection.x, _moveDirection.y, 0)));
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            _currentCell += Vector3Int.RoundToInt(new Vector3(_moveDirection.x, _moveDirection.y, 0));
            SnapToGrid();


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
}