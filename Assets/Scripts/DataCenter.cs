using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Collections;

public class DataCenter : MonoBehaviour
{
    //public static string location = Application.dataPath + "/saveFile.json";

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

    //public List<HighScore> LoadHighScore()
    //{
    //    if (!File.Exists(location))
    //    {
    //        List<HighScore> hg = new List<HighScore>();
    //        string jsonDummy = JsonHelper.ToJson(hg,true);
    //        File.WriteAllText(location, jsonDummy);
    //    }
    //    string json = File.ReadAllText(location);
    //    List<HighScore> scores = JsonHelper.FromJson<HighScore>(json);
    //    return scores;
    //}

    //public void SaveHighScore(List<HighScore> scores)
    //{
    //    string json = JsonHelper.ToJson<HighScore>(scores, true);
    //    File.WriteAllText(location, json);
    //}

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

        post = UnityWebRequest.Post(urlPostScore, formDate);
        yield return post.SendWebRequest();

        if (post.isNetworkError || post.isHttpError)
        {
            Debug.Log(post.error);
        }
        else
        {
            Debug.Log(post.downloadHandler.text);
        }
    }

    public IEnumerator GetHighScore()
    {
        UnityWebRequest www = UnityWebRequest.Get(urlGetScore);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            listScore = JsonHelper.FromJson<HighScore>(json);
        }
    }

    public IEnumerator GetLolos(string nim)
    {
        UnityWebRequest www = UnityWebRequest.Get(urlGetLolos+nim);
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
        }
    }
}
