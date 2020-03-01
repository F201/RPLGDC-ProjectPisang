using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

public class UIController : MonoBehaviour
{
    [TextArea]
    public string textLolos, textGagal;

    #region GameObejct
    public Text scoreText, timeText;
    public Text nimText;
    public GameObject panelStart;
    public GameObject panelGameOver;
    public GameObject highScore;
    #endregion

    public Text scoreTextPesonal, personalText;
    public GameObject winImage, loseImage;


    public void nextScene(string name) => SceneManager.LoadScene(name);

    public void ScoreVisual(int score)
    {
        scoreText.text = score.ToString();
    }

    public void TimeVisual(float time)
    {
        int last = Mathf.RoundToInt(time);
        int first = last / 60;
        timeText.text = string.Format("{0:d2}:{1:d2}",first,last%60);
    }

    public void StartGame()
    {
        if(nimText.text != "" && nimText.text.Length == 10)
        {
            panelStart.active = false;
            GameController.instance.ResetGame(nimText.text);
            GameController.instance.Message(nimText.text);
        }
    }

    public void PanelGameOver(List<DataCenter.HighScore> highScores)
    {
        int i = 0;
        foreach (Transform child in highScore.transform)
        {
            if(child.name != "Button")
            {
                child.GetChild(0).gameObject.GetComponent<Text>().text = highScores[i].nim;
                child.GetChild(1).gameObject.GetComponent<Text>().text = highScores[i].score.ToString();
                i++;
            }
        }
        highScore.active = true;
    }

    public void WinLoseCondition(int scores, bool lolos)
    {
        panelGameOver.active = true;
        scoreTextPesonal.text = scores.ToString();
        
        if(scores < 20)
        {
            personalText.text = "Play Again \n Get Score More than 20";
            
            winImage.active = false;
            loseImage.active = true;
            
        }else if (lolos)
        {
            personalText.text = textLolos;
            winImage.active = true;
            loseImage.active = false;
            AudioManager.audioManager.PlayBGMwin();
        }
        else
        {
            personalText.text = textGagal;
            winImage.active = false;
            loseImage.active = true;
            AudioManager.audioManager.PlayBGMLose();
        }
    }

    public void GetHighScore()
    {
        GameController.instance.getHighScore();
    }
}
