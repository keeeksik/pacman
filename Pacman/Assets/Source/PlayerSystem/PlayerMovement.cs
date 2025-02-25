using UnityEngine;

[RequireComponent(typeof(InputListener))] // ������� ������� ���������� PacmanInput
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� ��������
    private Vector3 targetPosition; // ������� �������
    private bool isMoving = false; // ��������� �� Pacman
    private InputListener pacmanInput; // ������ �� ��������� �����

    void Start()
    {
        // �������� ��������� PacmanInput
        pacmanInput = GetComponent<InputListener>();

        // ��������� ������� Pacman (����� ������)
        targetPosition = transform.position;
    }

    void Update()
    {
        // ���� Pacman �� ��������, ������������ ����
        if (!isMoving && pacmanInput.Direction != Vector2.zero)
        {
            Move(pacmanInput.Direction);
        }

        // �������� � ������� �������
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // ���� Pacman ������ ������� �������
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }

    void Move(Vector2 direction)
    {
        // ��������, ����� �� ��������� � ��������� �����������
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
        if (hit.collider == null || !hit.collider.CompareTag("Wall"))
        {
            // ������������� ����� ������� �������
            targetPosition = transform.position + (Vector3)direction;
            isMoving = true;
        }
    }
}