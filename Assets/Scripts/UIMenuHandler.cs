using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMenuHandler : MonoBehaviour
{
    public TMP_InputField nameInput;

    private void Start()
    {
        if (!string.IsNullOrEmpty(MainManager.Instance.playerName) && !MainManager.Instance.playerName.Equals("Anonymous"))
        {
            nameInput.text = MainManager.Instance.playerName;
        }
    }

    public void StartNew()
    {
        MainManager.Instance.playerName = !string.IsNullOrEmpty(nameInput.text) ? nameInput.text : "Anonymous";
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
