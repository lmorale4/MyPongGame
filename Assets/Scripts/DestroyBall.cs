using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBall : MonoBehaviour
{

    public PlayerController player;
    public GameManager gm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.AddScore();
        gm.roundWinner = null;
        gm.roundWinner = player;
        Destroy(collision.gameObject);
    }
}
