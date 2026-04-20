using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] private float surviveDuration = 60f;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject winPopup;
    [SerializeField] private GameObject losePopup;

    private float remainingTime;
    private bool gameOver;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        remainingTime = surviveDuration;
        winPopup.SetActive(false);
        losePopup.SetActive(false);
    }

    void Update()
    {
        if (gameOver) return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            Win();
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText == null) return;
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    public void Lose()
    {
        if (gameOver) return;
        gameOver = true;
        Time.timeScale = 0f;
        losePopup.SetActive(true);
    }

    private void Win()
    {
        if (gameOver) return;
        gameOver = true;
        Time.timeScale = 0f;
        winPopup.SetActive(true);
    }
}
