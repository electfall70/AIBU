using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private Button exitButton;

    private bool paused;
    public Button resumeButton;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update

    void Start()
    {
        pauseMenuUI.SetActive(false);
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (paused == false && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        else if (paused == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Unpause();
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        paused = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Awake()
    {
        backToMenuButton.onClick.AddListener(BackToMenu);
        exitButton.onClick.AddListener(ExitGame);
        resumeButton.onClick.AddListener(Unpause);

    }


    void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    void ExitGame()
    {
        Application.Quit();
    }
}
