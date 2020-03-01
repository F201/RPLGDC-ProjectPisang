using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    
    private Rigidbody2D _rigidbody2d;
    private Collision2D _ground;
    private SpriteRenderer _sprite;
    private Animator _animator;

    [SerializeField]
    private AudioManager _audioManager;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _ground = null;
    }

    public void Movement(float directional)
    {
        if (directional > 0.1)
            _sprite.flipX = false;
        else if (directional < -0.1)
            _sprite.flipX = true;
        if (directional != 0)
        {
            _animator.SetInteger("direction", 1);
        }
        else
        {
            _animator.SetInteger("direction", 0);
        }
        _rigidbody2d.velocity = new Vector2(1 * Time.deltaTime * directional,_rigidbody2d.velocity.y);
    }

    public void Jump(float trustPower)
    {
        if (_ground == null)
            return;
        _animator.SetTrigger("jump");
        _audioManager.JumpSound();
        _rigidbody2d.velocity = _rigidbody2d.velocity + (Vector2)(transform.up * trustPower);
        _ground = null;
    }
    

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _ground = collision;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _ground = null;
        }

    }
}
