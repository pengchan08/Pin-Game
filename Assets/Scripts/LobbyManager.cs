using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance;
    
    public GameObject htpPanel;
    public Canvas lobbyCanvas;

    public Button startButton;
    public Button htpButton;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            lobbyCanvas.gameObject.SetActive(true);
            
            startButton = GameObject.Find("StartButton").GetComponent<Button>();
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(StartGame);

            htpButton = GameObject.Find("HtpButton").GetComponent<Button>();
            startButton.onClick.RemoveAllListeners();
            htpButton.onClick.AddListener(OpenHtpPanel);
        }
        else
        {
            lobbyCanvas.gameObject.SetActive(false);
        }
    }

    public void OpenHtpPanel()
    {
        htpPanel.SetActive(true);
    }

    public void CloseHtpPanel()
    {
        htpPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
