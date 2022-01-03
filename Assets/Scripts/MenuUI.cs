using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour
{
    public void GoToMain()
    {
      SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
      #if UNITY_EDITOR
       EditorApplication.ExitPlaymode();
      #else
       Application.Quit();
      #endif
    }
}
