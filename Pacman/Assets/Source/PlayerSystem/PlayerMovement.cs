using UnityEngine;

[RequireComponent(typeof(InputListener))] // Требует наличие компонента PacmanInput
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения
    private Vector3 targetPosition; // Целевая позиция
    private bool isMoving = false; // Двигается ли Pacman
    private InputListener pacmanInput; // Ссылка на компонент ввода

    void Start()
    {
        // Получаем компонент PacmanInput
        pacmanInput = GetComponent<InputListener>();

        // Начальная позиция Pacman (центр клетки)
        targetPosition = transform.position;
    }

    void Update()
    {
        // Если Pacman не движется, обрабатываем ввод
        if (!isMoving && pacmanInput.Direction != Vector2.zero)
        {
            Move(pacmanInput.Direction);
        }

        // Движение к целевой позиции
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Если Pacman достиг целевой позиции
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }

    void Move(Vector2 direction)
    {
        // Проверка, можно ли двигаться в выбранном направлении
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
        if (hit.collider == null || !hit.collider.CompareTag("Wall"))
        {
            // Устанавливаем новую целевую позицию
            targetPosition = transform.position + (Vector3)direction;
            isMoving = true;
        }
    }
}