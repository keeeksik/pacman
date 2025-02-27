using UnityEngine;

public class Point : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // “очка собираетс€ игроком (уничтожаем ее)
            Destroy(gameObject);
        }
    }
}

