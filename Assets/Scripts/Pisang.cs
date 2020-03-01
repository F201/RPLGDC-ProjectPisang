using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pisang : MonoBehaviour
{
    
    public float time;
    private float _time;
    private SpriteRenderer spriteRenderer;
    public Sprite deadBanana;
    public Sprite banana;
    private bool _isCanEat = true;
    public Collider2D collider2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Respawn();
    }

    public void Respawn()
    {
        _time = time;
        _isCanEat = true;
        spriteRenderer.sprite = banana;
        transform.rotation = Quaternion.identity;
        collider2D.isTrigger = true;
    }

    public void Checking()
    {
        if (_isCanEat)
        {
            GameController.instance.AddingScore(1);
        }
        else
        {
            gameObject.SetActive(false);
            GameController.instance.GameOver();
        }
        BananaSpawn.BackToPoll(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            _isCanEat = false;
            spriteRenderer.sprite = deadBanana;
            collider2D.isTrigger = false;
        }
        if (collision.gameObject.CompareTag("Banana"))
        {
            if(collision.gameObject.GetComponent<SpriteRenderer>().sprite == deadBanana)
            {
                _isCanEat = false;
                spriteRenderer.sprite = deadBanana;
                collider2D.isTrigger = false;
            }
        }
    }

    private void Update()
    {
        if(GameController.instance.isGameOver)
        {
            gameObject.SetActive(false);
            BananaSpawn.BackToPoll(gameObject);
        }
        if (!_isCanEat)
        {
            _time -= Time.deltaTime;
            if(_time <= 0)
            {
                BananaSpawn.BackToPoll(gameObject);
            }
        }
    }
}
