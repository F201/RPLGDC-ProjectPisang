using UnityEngine;

public class PlayerInput : PlayerPhysics
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _jumpPower;
    public KeyCode jumpKey;

    private GameController _controller;
    

    private void Start()
    {
        _controller = GameController.instance;
    }


    void Update()
    {
        if (GameController.instance.isGameOver)
            return;
        if (Input.GetKeyDown(jumpKey))
        {
            Jump(_jumpPower);
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        Movement(horizontal * _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Banana"))
        {
            collision.gameObject.GetComponent<Pisang>().Checking();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Banana"))
        {
            collision.gameObject.GetComponent<Pisang>().Checking();
        }
    }
}
