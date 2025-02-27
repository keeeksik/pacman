using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    public Vector2 MoveDirection { get; private set; } = Vector2.zero;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем этот объект между сценами (опционально)
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        MoveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MoveDirection = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MoveDirection = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveDirection = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveDirection = Vector2.right;
        }

        if (MoveDirection.magnitude > 0)
        {
            MoveDirection = MoveDirection.normalized;
        }
    }
}