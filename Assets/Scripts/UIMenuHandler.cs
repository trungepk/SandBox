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
    public TMP_Text highestScoreTxt;

    private void Start()
    {
        if (!string.IsNullOrEmpty(MainManager.Instance.curPlayerName) && !MainManager.Instance.curPlayerName.Equals("Anonymous"))
        {
            nameInput.text = MainManager.Instance.curPlayerName;
        }

        if (MainManager.Instance.highestScore > 0)
        {
            highestScoreTxt.text = string.Format("{0} has the highest score of {1}", MainManager.Instance.topPlayerName, MainManager.Instance.highestScore);
        }
        else
        {
            highestScoreTxt.text = string.Empty;
        }
    }

    public void StartNew()
    {
        MainManager.Instance.curPlayerName = !string.IsNullOrEmpty(nameInput.text) ? nameInput.text : "Anonymous";
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
