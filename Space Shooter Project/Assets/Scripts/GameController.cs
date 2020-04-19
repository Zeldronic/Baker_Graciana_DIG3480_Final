using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text GameOverText;
    public Text RestartText;
    private int score;

    public AudioSource musicSource;
    public AudioClip mainAudio;
    public AudioClip loseAudio;
    public AudioClip winAudio; 

    private bool gameOver;
    private bool restart;

    void Start()
    {
        musicSource.clip= mainAudio;
        musicSource.Play(); 

        gameOver = false;
        restart = false;
        RestartText.text = "";
        GameOverText.text = "";

        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                // Application.LoadLevel(Application.loadedLevel);
                SceneManager.LoadScene("Tutorial3_SpaceShooter");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;

                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                RestartText.text = "Press 'X' for Restart.";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            musicSource.Stop();

            musicSource.clip = winAudio;
            musicSource.Play();

            GameOverText.text = "You win! GAME CREATED BY Graciana Baker";
            gameOver = true;
            restart = true;
        }
    }

    public void GameOver()
    {
        musicSource.Stop();

        musicSource.clip = loseAudio;
        musicSource.Play();

        GameOverText.text = "Game Over. GAME CREATED BY Graciana Baker";
        gameOver = true;

    }
}

