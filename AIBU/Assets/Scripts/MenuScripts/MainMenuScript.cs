using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Button newGame;
    [SerializeField] private Button exit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake()
    {
        newGame.onClick.AddListener(StartGame);
        exit.onClick.AddListener(ExitGame);
    }

    void ExitGame()
    {
        Application.Quit();
    }
    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

}
