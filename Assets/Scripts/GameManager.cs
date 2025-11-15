using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("UI")]
    public GameObject pauseMenu;
    public Button resumeButton;
    public Button quitButton;
    
    private bool isPaused = true;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    
    void Start()
    {
        PauseGame();
        if (resumeButton != null)
            resumeButton.onClick.AddListener(PauseGame);
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
            
        if (pauseMenu != null)
            pauseMenu.SetActive(true);
    }
    
    
    
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        if (pauseMenu != null)
            pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}