using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class TestJSON : MonoBehaviour
{
    [Serializable]
    class dataLolos
    {
        public int idx;
        public string nim;
        public int score;
    }
    // Start is called before the first frame update
    void Start()
    {
        ////dataLolos[] lolos = new dataLolos[2] ;
        ////lolos[0] = new dataLolos("1", 10);
        ////lolos[1] = new dataLolos("2", 20);
        ////string json = JsonHelper.ToJson(lolos,true);
        ////Debug.Log(json);
        ////File.WriteAllText(Application.dataPath + "/saveFile.json", json);

        //string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
        //List<dataLolos> load = JsonHelper.FromJson<dataLolos>(json);
        //load.Add(new dataLolos("3", 100));
        //json = JsonHelper.ToJson(load, true);
        //Debug.Log(json);
        //File.WriteAllText(Application.dataPath + "/saveFile.json", json);
        //Debug.Log(load[0].score);
        StartCoroutine(GetText());

    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://rplgdc-dashboard.herokuapp.com/allScore");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            string json = www.downloadHandler.text;
            Debug.Log(json);
            List<dataLolos> tmp = JsonHelper.FromJson<dataLolos>(json);
            Debug.Log(tmp[0].nim);
        }
    }
}
