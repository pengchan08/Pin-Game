using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public bool isGameOver = false;
    public int goal = 1;

    public int score = 0;
    public int basicScore = 1;
    public TextMeshProUGUI scoreText;

    public int comBo = 0;
    public float comBoTime = 1;
    public GameObject comBoText;

    public int level = 1;
    public TextMeshProUGUI levelText;

    public GameObject parentObject;

    [SerializeField] private GameObject btnRetry;
    [SerializeField] private GameObject btnLobby;
    [SerializeField] private TextMeshProUGUI textGoal;
    [SerializeField] private Color green;
    [SerializeField] private Color red;

    public AudioClip audioClip;
    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        textGoal.SetText(goal.ToString());
        levelText.SetText("Level " + level);
    }
    
    public void DecreaseGoal()
    {
        goal -= 1;
        textGoal.SetText(goal.ToString());
        
        if (goal <= 0 && level != 25 && !isGameOver)
        {
            level += 1;
            levelText.SetText("Level " + level);
            goal = level;
            textGoal.SetText(goal.ToString());
            Transform[] childList = parentObject.GetComponentsInChildren<Transform>();
            if (childList != null)
            {
                for (int i = 1; i < childList.Length; i++)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }
        else if (goal <= 0 && level == 25)
        {
            SetGameOver(true);
        }
    }

    private void AddScore()
    {
        score = score + basicScore + comBo;
        scoreText.SetText("SCORE: " + score);
    }

    public void AddComBo()
    {
        StopAllCoroutines();
        StartCoroutine(StartComBo());
    }

    IEnumerator StartComBo()
    {
        if (comBo < 5) comBo++;
        comBoText.SetActive(true);
        comBoText.GetComponentInChildren<TextMeshProUGUI>().text = "COMBO " + comBo;
        AddScore();
        yield return new WaitForSeconds(comBoTime);
        comBo = 0;
        comBoText.SetActive(false);
        comBoText.GetComponentInChildren<TextMeshProUGUI>().text = "COMBO " + comBo;
    }

    public void SetGameOver(bool success)
    {
        if (isGameOver == false && level < 25)
        {
            isGameOver = true;
            Camera.main.backgroundColor = success ? green : red;
            Invoke(nameof(ShowRetryButton), 1f);
            Invoke(nameof(ShowLobbyButton), 1f);
        }
        else if (isGameOver == false && level == 25)
        {
            isGameOver = true;
            Camera.main.backgroundColor = Color.blue;
            Invoke(nameof(ShowRetryButton), 1f);
            Invoke(nameof(ShowLobbyButton), 1f);
        }
    }

    private void ShowRetryButton()
    {
        btnRetry.SetActive(true);
    }

    private void ShowLobbyButton()
    {
        btnLobby.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Lobby()
    {
        SceneManager.LoadScene(0);
    }
}
