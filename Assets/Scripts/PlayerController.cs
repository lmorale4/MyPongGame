using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public int playerNumber;
	public float speed;
	public float yMin;
    public float yMax;
	[HideInInspector] public int score = 0;

	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate()
	{
		float move = Input.GetAxis ("Vertical" + playerNumber);

		rb.velocity = new Vector2(0f, move * speed);
	}
		
	public void AddScore()
	{
		score++;
	}

	public void ResetScore()
	{
		score = 0;
	}

	public void ResetPosition()
	{
		rb.position = new Vector2 (rb.transform.position.x, 0f);
	}
}
