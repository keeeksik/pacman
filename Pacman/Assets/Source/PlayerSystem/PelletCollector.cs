using UnityEngine;
using UnityEngine.Tilemaps;

public class PelletCollector : MonoBehaviour
{
    public Tilemap pelletTilemap; // Tilemap � �������
    public Tile emptyTile; // ���� ��� ������ (��������, ������ ����)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������, ��� ����������� � Tilemap
        if (collision.GetComponent<Tilemap>() == pelletTilemap)
        {
            // �������� ������� ����� � ����������� Tilemap
            Vector3Int cellPosition = pelletTilemap.WorldToCell(transform.position);

            // ���������, ���� �� ���� ����� � ���� �������
            if (pelletTilemap.GetTile(cellPosition) != null)
            {
                // ������� ����� (�������� �� ������ ����)
                pelletTilemap.SetTile(cellPosition, emptyTile);

                // ��������� ����
                GameManager.Instance.AddScore(10);

                // ������������� ���� (���� ���� AudioSource)
                if (TryGetComponent(out AudioSource audioSource))
                {
                    audioSource.Play();
                }

                Debug.Log("Point collected!");
            }
        }
    }
}