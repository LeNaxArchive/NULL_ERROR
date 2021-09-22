using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement; //1 adding header for scene  
using UnityEngine.UI; // header for ui : score/ text /title

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject enemy;
    public GameObject cube;
    public Transform SpawnPointAstroid;
    //public Transform SpawnPointRCube;
    public float maxSpawnPointY;

    public GameObject MenuPanel;
    public GameObject Player;

    int score = 0;
    int highScore = 0;

    public Text ScoreText;
    public Text highScoreText;
    public Text NextText;
    public Text ErrorText;

    public Text GameOverText;
    public Text InstructionsText;
    public Text TittleText;

    public AudioSource MenuMusic;
    public AudioSource GameMusic;
    public AudioSource OverMusic;

    bool gameStarted = false;
    bool gameOver = false;

    private float timer;
    private int error;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        MenuMusic.Play();

        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetInt("highScore");
            highScoreText.text = "High Score : " + highScore.ToString(); //Display value
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.R) && gameOver == true)
        {
            gameOver = false;
            gameStarted = false;
            Restart();

        }

        timer += Time.deltaTime;

        if (timer > 2f)
        {
            if (gameOver == false)
            {
                error += 1;
                
                //We only need to update the text if the score changed.
                ErrorText.text = "Corruption : " + error.ToString() + "%";

                //Reset the timer to 0.
                timer = 0;


                if (error == 100)
                {

                    GUIGameOver();
                    Destroy(Player);
                }
            }

        }


        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            GameMusic.Play();
            Destroy(MenuMusic);

            MenuPanel.gameObject.SetActive(false);
            ScoreText.gameObject.SetActive(true);
            ErrorText.gameObject.SetActive(true);
            Player.gameObject.SetActive(true);

            StartCoroutine("SpawnEnemies" , "SpawnCube");

            gameStarted = true;
            
            //ScoreUp = true;


        }

        if (Input.GetKeyDown(KeyCode.Q) && (gameOver == true || gameOver == false || gameStarted == true || gameStarted == false))
        {
            Application.Quit();
        }

    }

   
    

        IEnumerator SpawnEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.8f);
                Spawn();

            }
        }
    
        IEnumerator SpawnCube()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.8f);
                SpawnrCube();

            }
        }

        public void Spawn()
        {
            float randomSpawnY = Random.Range(1, maxSpawnPointY);
            Vector3 enemySpawnPos = SpawnPointAstroid.position;
            enemySpawnPos.y = randomSpawnY;

            Instantiate(enemy, enemySpawnPos, Quaternion.identity);
        }
    
        public void SpawnrCube()
        {
            float randomSpawnCubeY = Random.Range(1, maxSpawnPointY);
            Vector3 cubeSpawnPos = SpawnPointAstroid.position;
            cubeSpawnPos.y = randomSpawnCubeY;

            Instantiate(cube, cubeSpawnPos, Quaternion.identity);
        }//end of Spawn()

        public void GUIGameOver()
        {
            GameOverText.gameObject.SetActive(true);
            NextText.gameObject.SetActive(true);
            gameOver = true;
            OverMusic.Play();
        Destroy(GameMusic);


    }

        public void Restart()
        {
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("highScore", highScore);
            }

            SceneManager.LoadScene(0);


        }

        public void ScoreUp() //to increase score
        {
        if (gameOver == false)
        {
            score++;
            ScoreText.text = "Score : " + score.ToString();
            
        }
        }

    public void Repair() //to Decrease errror
    {
        if (error > 0 && gameOver == false)
        {
            error -= 10;
            ErrorText.text = error.ToString();
            //ErrorText.text = "" + Mathf.Round(error);


            if (error < 0)
            {
                error =  0;
                ErrorText.text = "Corruption : " + error.ToString() + "%";
                ErrorText.text = "Corruption : " + Mathf.Round(error) + "%";
            }
        }
    }

    
}
