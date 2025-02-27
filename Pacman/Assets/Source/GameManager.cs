using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Для отображения UI

public class GameManager : MonoBehaviour
{
    public int startingLives = 3;
    public int pointsToWin = 0; // Количество очков для победы (заполняется в Start)
    public Text scoreText;       // Ссылка на Text для отображения очков
    public Text livesText;       // Ссылка на Text для отображения жизней
    public GameObject gameOverScreen; // Экран поражения
    public GameObject winScreen; // Экран победы

    public static bool IsGameOver = false; // Флаг окончания игры

    private int _currentScore = 0;
    private int _currentLives;
    private GameObject[] _points; // Массив для хранения всех точек

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
        //  Здесь можно добавить звук сбора очка
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
        //  Здесь можно добавить звук потери жизни
        if (_currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            // Восстановление игрока
            ResetPlayerPosition(); // Реализуйте этот метод
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
        //  Логика восстановления позиции игрока (например, к стартовой позиции)
        //  Найдите игрока (предполагаем, что он имеет тег "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            //  Получите стартовую позицию игрока
            //  (этот пример предполагает, что у вас есть стартовая позиция,
            //  но ее нужно реализовать в сцене)
            Vector3 startPosition = GameObject.FindGameObjectWithTag("PlayerStartPosition").transform.position;
            player.transform.position = startPosition; // Сброс позиции
        }
    }

    void GameOver()
    {
        IsGameOver = true;
        //Debug.Log("Game Over!");
        gameOverScreen.SetActive(true); // Отображаем экран поражения
    }

    void WinGame()
    {
        IsGameOver = true;
        //Debug.Log("You Win!");
        winScreen.SetActive(true); // Отображаем экран победы
    }
}