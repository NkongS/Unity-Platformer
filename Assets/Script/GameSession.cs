using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    [SerializeField] TextMeshProUGUI textLives;
    [SerializeField] TextMeshProUGUI textScore;

    void Start()
    {
        textLives.text = playerLives.ToString();
        textScore.text = score.ToString();
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
        textScore.text = score.ToString();
    }

    void Awake()
    {
        //--------------------
        // Singleton Pattern
        //--------------------
        // Count how many GameSession object at this point
        int numGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;

        // If there is a GameSession object already
        if(numGameSessions >= 2) // ..together with this object, there will be 2 objects
        {
            Destroy(gameObject); // Kill this one... So, the first one is the only one left.
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Make this object survive when we load a new scene
        }
    }


    public void ProcessPlayerDeath() 
    {
        if(playerLives > 1)
        {
            playerLives--;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            textLives.text = playerLives.ToString();
        }
        else
        {
            SceneManager.LoadScene(5); // assume that scene 0 is the first one..or the menu
            Destroy(gameObject); // Destroy teh game session
        }
    }
}
