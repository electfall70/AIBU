using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button back;
    private void Awake()
    {
        back.onClick.AddListener(ChangeScene);

    }
    void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }
}
