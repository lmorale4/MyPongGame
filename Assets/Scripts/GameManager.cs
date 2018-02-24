using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int numRounds = 3;
    public float startDelay = 2f;
    public float endDelay = 2f;
    public GameObject ballPrefab;
    public Text scoreText;
    public Text winText;
    public Text restartText;
    public Animator restartAnim;
    public PlayerController[] players;
    [HideInInspector] public PlayerController roundWinner;
    
    private GameObject ball;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private PlayerController gameWinner;
    private bool restart = false;
    private AudioSource coinClip;

    private void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        winText.text = "";
        restartText.text = "";

        coinClip = GetComponent<AudioSource>();
        
        StartCoroutine(GameLoop());
    }

    private void Update()
    {
        if (restart)
        {
            restartText.text = "Press 'Space' to restart";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                coinClip.Play();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void SpawnBall()
    {
        ball = Instantiate(ballPrefab);
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(StartRound());
        yield return StartCoroutine(PlayingRound());
        yield return StartCoroutine(EndRound());

        if (gameWinner != null)
        {
            RevealGameWinner();
            restart = true;
            restartAnim.SetTrigger("GameOver");
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator StartRound()
    {
        ResetPlayers();

        yield return startWait;
    }

    private IEnumerator PlayingRound()
    {
        SpawnBall();

        while (ball)
        {
            yield return null;
        }
    }

    private IEnumerator EndRound()
    {
        SetScoreText();
        gameWinner = GetGameWinner();
        yield return endWait;
    }

    private void ResetPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].ResetPosition();
        }
    }

    private PlayerController GetGameWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            //print(numRounds);
            if (players[i].score >= numRounds)
            {
                //print(players[i].score);
                return players[i];
            }
        }
        return null;
    }

    private void RevealGameWinner()
    {
        winText.text = gameWinner.name.ToUpper() + " WINS!";
    }

     private void SetScoreText()
    {
        scoreText.text = "Score\n" + players[0].score + " - " + players[1].score;
    }
}
