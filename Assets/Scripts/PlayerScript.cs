using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public float maxSpeed;
    public int jumpHeight;
    public float xVel;
    public Text score;
    public Text win;
    // public Text lives;
    private int scoreValue = 0;
    // private int livesValue = 3;
    public LayerMask groundLayer;

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null) {
            return true;
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        // score.text = "Lives: " + livesValue.ToString();
        win.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        int yMovement = (int)Input.GetAxisRaw("Vertical");
        if (yMovement == 1) {
            Jump();
        }
    }
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        xVel = rd2d.velocity.x;
        if (xVel >= maxSpeed)
        {
            if (hozMovement <= 0)
            {
                rd2d.AddForce(new Vector2(hozMovement * speed, 0));
            }
        }
        else if (xVel <= -maxSpeed)
        {
            if (hozMovement >= 0)
            {
                rd2d.AddForce(new Vector2(hozMovement * speed, 0));
            }
        }
        else
        {
            rd2d.AddForce(new Vector2(hozMovement * speed, 0));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D collide = collision.GetComponent<Collider2D>();
        if (collide.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.gameObject);
            /*
            if (scoreValue == 4)
            {
                transform.position = new Vector3(100.0f, 0.0f, 0.0f);
                livesValue = 3;
                lives.text = "Lives: " + livesValue.ToString();
            }
            */
            if (scoreValue >= 30)
            {
                win.text = "You win! By: Erik Murrell";
            }
        }
        /*
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
            if (livesValue <= 0)
            {
                win.text = "You lose! By: Erik Murrell";
                Destroy(this);
            }
        }
        */
    }

    void Jump()
    {
        if (!IsGrounded())
        {
            return;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            }
        }
    }
}
