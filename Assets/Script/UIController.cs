using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Button startButton;
    public Button backButton;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("Start");
        backButton = root.Q<Button>("Back");


        if (startButton != null)
        {
            startButton.clicked += StartGame;
        }
        if (backButton != null)
        {
            backButton.clicked += BackToMenu;
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("Level1");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
        {
            SceneManager.SetActiveScene(scene);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
