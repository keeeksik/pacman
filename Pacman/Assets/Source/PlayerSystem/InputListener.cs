using UnityEngine;

public class InputListener : MonoBehaviour
{
    public Vector2 Direction { get; private set; } // ����������� ��������

    void Update()
    {
        // ��������� �����
        if (Input.GetKey(KeyCode.W)) Direction = Vector2.up;
        else if (Input.GetKey(KeyCode.S)) Direction = Vector2.down;
        else if (Input.GetKey(KeyCode.A)) Direction = Vector2.left;
        else if (Input.GetKey(KeyCode.D)) Direction = Vector2.right;
        else Direction = Vector2.zero; // ���� ������� �� ������
    }
}