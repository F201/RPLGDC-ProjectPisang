using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Collections;

public class DataCenter : MonoBehaviour
{

    private string urlPublish = "https://rplgdc-dashboard-api.herokuapp.com";
    private string urlDevelopment = "https://rplgdc-dashboard.herokuapp.com";

    public string urlGetScore;
    public string urlPostScore;
    public string urlGetLolos;

    private List<HighScore> listScore = new List<HighScore>();

    [Serializable]
    public class HighScore{
        public int idx;
        public string nim;
        public int score;
        public HighScore(string nim, int score)
        {
            this.nim = nim;
            this.score = score;
        }
    }

    [Serializable]
    public class Lolos
    {
        public string data, msg;
    }

    private static int Comperator(HighScore x, HighScore y)
    {
        return y.score - x.score;
    }

    public IEnumerator PostHighScore(string nim, int score)
    {
        listScore.Add(new HighScore(nim, score));
        listScore.Sort(Comperator);
        GameController.instance.uIController.PanelGameOver(listScore);

        UnityWebRequest post;
        WWWForm formDate = new WWWForm();
        formDate.AddField("nim", nim);
        formDate.AddField("score", score.ToString());

        post = UnityWebRequest.Post(urlPublish+urlPostScore, formDate);
        yield return post.SendWebRequest();

        //Debug.Log(post.downloadHandler.text);
    }

    public IEnumerator GetHighScore()
    {
        UnityWebRequest www = UnityWebRequest.Get(urlPublish+urlGetScore);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            //Debug.Log(json);
            listScore = JsonHelper.FromJson<HighScore>(json);
        }
    }

    public IEnumerator GetLolos(string nim)
    {
        UnityWebRequest www = UnityWebRequest.Get(urlPublish+urlGetLolos+nim);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            Lolos lolos = JsonUtility.FromJson<Lolos>(json); //1301174145
            if (lolos.data == "pass")
            {
                GameController.instance.lolos = true;
            }
            else
            {
                GameController.instance.lolos = false;
            }
            //Debug.Log(json);
        }
    }
}
