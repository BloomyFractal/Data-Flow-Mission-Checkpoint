using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

//Data persistence script
public class DataPersists : MonoBehaviour
{

  public static DataPersists dataPersists;
  public TMP_InputField inputField;

  public string playerName;
  public string bestScorePlayer;
  public TextMeshProUGUI bestScoreText;
  public int highScore;

  //Singleton
  private void Awake()
  {

   if (dataPersists != null)
    {
      Destroy(gameObject);
      return;
    }

    dataPersists = this;
    DontDestroyOnLoad(gameObject);

     LoadNameAndScore();
     bestScoreText.text = "Best Score: " + bestScorePlayer + " - " + highScore + " pts";
  }

  public void SetPlayerName()
  {
    playerName = inputField.text;
    Debug.Log("playerName is " + playerName + ".");
    SceneManager.LoadScene(1);
  }

  public void Exit()
  {

    #if UNITY_EDITOR
     EditorApplication.ExitPlaymode();
    #else
     Application.Quit();
    #endif
  }

  [System.Serializable]
  class SaveData
  {
    public string bestScorePlayer;
    public int highScore;
  }

  public void SaveNameAndScore()
  {
    SaveData data = new SaveData();
    data.bestScorePlayer = bestScorePlayer;
    data.highScore = highScore;

    string json = JsonUtility.ToJson(data);

    File.WriteAllText(Application.persistentDataPath + "/savefile.json",json);
  }

  public void LoadNameAndScore()
  {
    string path = Application.persistentDataPath + "/savefile.json";
    if(File.Exists(path))
    {
      string json = File.ReadAllText(path);
      SaveData data = JsonUtility.FromJson<SaveData>(json);

      bestScorePlayer = data.bestScorePlayer;
      highScore = data.highScore;
    }
  }
}
