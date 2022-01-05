using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainScense : MonoBehaviour
{
   public GameObject input;
   public GameDataScript gameData;
   public GameObject text;
   public GameObject battonExit;
   public void Load()
   {
      SetName();
      SceneManager.LoadScene("MainScene");
   }

   public void SetName()
   {
      Debug.Log(input.GetComponent<Text>().text);
      gameData.NamePlayer = input.GetComponent<Text>().text;
   }

   private void Start()
   {
      if (gameData.isFinsh)
         battonExit.SetActive(true);
      var end = "Best \n";
      foreach (var i  in gameData.bestPlayer)
         end += i + '\n';
      text.GetComponent<Text>().text = end;
   }

   public void Exit()
   {
      Application.Quit();
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#endif
   }
}
