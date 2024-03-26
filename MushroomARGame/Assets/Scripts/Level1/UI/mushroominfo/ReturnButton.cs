using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour
{
    private Button myButton; 

    void Awake()
    {
        if (myButton == null && !TryGetComponent(out myButton))
        {
            Debug.LogError("Button component is not assigned or found on the GameObject.");
        }
    }

    void Start()
    {
        if (myButton != null)
        {
            myButton.onClick.AddListener(ChangeScene);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
