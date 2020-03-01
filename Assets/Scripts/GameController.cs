using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private DataCenter _dataCenter;
    private int _score = 0;
    private float _time = 0;
    private string _nim;
    public bool lolos = false;
    public bool isGameOver;
    public UIController uIController;

    [SerializeField]
    private AudioManager audioManager;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            ResetGame("0");
            isGameOver = true;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }

    void initialize(string nim)
    {
        _player.transform.position = (Vector3.back);
        _nim = nim;
        _score = 0;
        _time = 0;
        uIController.ScoreVisual(_score);
        uIController.TimeVisual(_time);
    }

    public void ResetGame(string nim)
    {
        initialize(nim);
        isGameOver = false;
        Time.timeScale = 1;
    }

    public void AddingScore(int score)
    {
        if (isGameOver)
            return;
        _score = _score + score;
        uIController.ScoreVisual(_score);
    }

    public void AddingTime(float time)
    {
        if (isGameOver)
            return;
        _time += time;
        uIController.TimeVisual(_time);
    }

    public void GameOver()
    {
        audioManager.SetActiveBGM(false);
        audioManager.PlayBGMScore();
        isGameOver = true;
        uIController.WinLoseCondition(_score, lolos);
        StartCoroutine(_dataCenter.PostHighScore(_nim,_score));
        Time.timeScale = 0;
    }

    public void Message(string cek)
    {
        _nim = cek;
        StartCoroutine(_dataCenter.GetLolos(_nim));
    }

    public void getHighScore()
    {
        StartCoroutine(_dataCenter.GetHighScore());
    }
}
