using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{

   public static MainManager mainManager;

    //Gameplay variable
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    //Text variables
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI bestScoreText;
    public GameObject GameOverText;

    public int score;//The total number of points for a single session

    //Game Management variables
    private bool m_Started = false;
    private bool m_GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
      score = 0;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)//Number of points obtained per brick destroyed
    {
       score += point;
       ScoreText.text = "Score: " + score + " pts";

       //Save only highest score
       if (score > DataPersists.dataPersists.highScore)
       {

         DataPersists.dataPersists.bestScorePlayer = DataPersists.dataPersists.playerName;

         DataPersists.dataPersists.highScore += point;

         bestScoreText.text = "Best Score: " + DataPersists.dataPersists.playerName + " - " + DataPersists.dataPersists.highScore + " pts";
       }

       bestScoreText.text = "Best Score: " + DataPersists.dataPersists.bestScorePlayer + " - " + DataPersists.dataPersists.highScore + " pts";
    }

    public void GoToMenu()
    {
      SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
      m_GameOver = true;
      GameOverText.SetActive(true);

       DataPersists.dataPersists.SaveNameAndScore();
    }
}
