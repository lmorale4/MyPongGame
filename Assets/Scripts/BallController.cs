using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb;
    private AudioSource hit;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0.0f);
        hit = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float ballY = BallHitFactor(transform.position, other.transform.position, other.collider.bounds.size.y);
            float ballX = -other.transform.position.x / Mathf.Abs(other.transform.position.x);
            Vector2 ballDirection = new Vector2(ballX, ballY).normalized;

            rb.velocity = ballDirection * speed;
            hit.Play();
        }
    }

    private float BallHitFactor(Vector2 ballPosition, Vector2 playerPosition, float playerHeight)
    {
        return (ballPosition.y - playerPosition.y) / playerHeight;
    }
}
