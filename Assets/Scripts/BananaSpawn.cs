using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSpawn : MonoBehaviour
{
    public GameObject banana;
    public int size;
    public static Queue<GameObject> bananas;
    private static int last;

    public float interval;
    private float _interval;
    private Vector3 lastPosition;

    private void Awake()
    {
        last = -1;
        bananas = new Queue<GameObject>();
        lastPosition = Vector3.zero;
        for(int i = 0;  i < size; i++)
        {
            GameObject newBanana = Instantiate(banana);
            newBanana.transform.SetParent(this.transform);
            newBanana.name = string.Format("Banana {0}", i + 1);
            BackToPoll(newBanana);
        }
    }

    public static void BackToPoll(GameObject obj)
    {
        last++;
        obj.active = false;
        bananas.Enqueue(obj);
    }

    void SpawnBanana()
    {
        if (last < 0)
            return;
        GameObject newBanana = bananas.Dequeue();
        last--;
        Vector3 newPos = transform.position + Vector3.right * Random.Range(-8.5f, 8.5f);
        while(Mathf.Abs(Vector3.Distance(newPos,lastPosition)) <= 0.4f)
        {
            newPos = transform.position + Vector3.right * Random.Range(-8.5f, 8.5f);
        }
        newBanana.transform.position = newPos;
        lastPosition = newBanana.transform.position;
        newBanana.active = true;
        newBanana.gameObject.GetComponent<Pisang>().Respawn();
        interval = Mathf.Clamp(interval - 0.1f, 1f, 3f);
    }

    void Start()
    {
        _interval = interval;
        GameController.instance.AddingTime(0);
        SpawnBanana();
    }

    void Update()
    {
        if (GameController.instance.isGameOver)
            return;
        _interval -= Time.deltaTime;
        if(_interval <= 0)
        {
            _interval = interval;
            SpawnBanana();
            int ouliers = Random.Range(0, 100);
            if(ouliers >= 95)
                SpawnBanana();
        }
        GameController.instance.AddingTime(Time.deltaTime);
    }
}
