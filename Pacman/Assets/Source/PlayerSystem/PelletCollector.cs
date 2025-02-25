using UnityEngine;
using UnityEngine.Tilemaps;

public class PelletCollector : MonoBehaviour
{
    public Tilemap pelletTilemap; // Tilemap с точками
    public Tile emptyTile; // Тайл для замены (например, пустой тайл)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что столкнулись с Tilemap
        if (collision.GetComponent<Tilemap>() == pelletTilemap)
        {
            // Получаем позицию тайла в координатах Tilemap
            Vector3Int cellPosition = pelletTilemap.WorldToCell(transform.position);

            // Проверяем, есть ли тайл точки в этой позиции
            if (pelletTilemap.GetTile(cellPosition) != null)
            {
                // Удаляем точку (заменяем на пустой тайл)
                pelletTilemap.SetTile(cellPosition, emptyTile);

                // Добавляем очки
                GameManager.Instance.AddScore(10);

                // Воспроизводим звук (если есть AudioSource)
                if (TryGetComponent(out AudioSource audioSource))
                {
                    audioSource.Play();
                }

                Debug.Log("Point collected!");
            }
        }
    }
}