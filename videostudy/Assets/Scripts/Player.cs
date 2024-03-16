
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    public float speed;
    public float jampHeight;
    public Transform grondCheck;
    bool isGrounded;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        CheckGround();
        if(Input.GetAxis("Horizontal") == 0 && isGrounded)
        {
            anim.SetInteger("State", 1);
        }
        else
        {
            Flip();
            if (isGrounded)
            {
                anim.SetInteger("State", 2);
            }
        }
    }

    private void LateUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.AddForce(transform.up * jampHeight, ForceMode2D.Impulse);
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(grondCheck.position, 0.2f);
        isGrounded = colliders.Length > 1;
        if(!isGrounded)
        {
            anim.SetInteger("State", 3);
        }
    }
}
