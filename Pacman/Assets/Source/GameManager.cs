using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ��� ����������� UI

public class GameManager : MonoBehaviour
{
    public int startingLives = 3;
    public int pointsToWin = 0; // ���������� ����� ��� ������ (����������� � Start)
    public Text scoreText;       // ������ �� Text ��� ����������� �����
    public Text livesText;       // ������ �� Text ��� ����������� ������
    public GameObject gameOverScreen; // ����� ���������
    public GameObject winScreen; // ����� ������

    public static bool IsGameOver = false; // ���� ��������� ����

    private int _currentScore = 0;
    private int _currentLives;
    private GameObject[] _points; // ������ ��� �������� ���� �����

    void Start()
    {
        IsGameOver = false;
        _currentLives = startingLives;
        UpdateLivesUI();
        _points = GameObject.FindGameObjectsWithTag("Point");
        pointsToWin = _points.Length;
        UpdateScoreUI();
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    public void CollectPoint(GameObject point)
    {
        _currentScore ++;
        UpdateScoreUI();
        //  ����� ����� �������� ���� ����� ����
        if (_currentScore >= pointsToWin)
        {
            WinGame();
        }
    }

    public void PlayerDied()
    {
        if (IsGameOver) return;
        _currentLives--;
        UpdateLivesUI();
        //  ����� ����� �������� ���� ������ �����
        if (_currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            // �������������� ������
            ResetPlayerPosition(); // ���������� ���� �����
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + _currentScore;
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + _currentLives;
        }
    }

    void ResetPlayerPosition()
    {
        //  ������ �������������� ������� ������ (��������, � ��������� �������)
        //  ������� ������ (������������, ��� �� ����� ��� "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            //  �������� ��������� ������� ������
            //  (���� ������ ������������, ��� � ��� ���� ��������� �������,
            //  �� �� ����� ����������� � �����)
            Vector3 startPosition = GameObject.FindGameObjectWithTag("PlayerStartPosition").transform.position;
            player.transform.position = startPosition; // ����� �������
        }
    }

    void GameOver()
    {
        IsGameOver = true;
        //Debug.Log("Game Over!");
        gameOverScreen.SetActive(true); // ���������� ����� ���������
    }

    void WinGame()
    {
        IsGameOver = true;
        //Debug.Log("You Win!");
        winScreen.SetActive(true); // ���������� ����� ������
    }
}