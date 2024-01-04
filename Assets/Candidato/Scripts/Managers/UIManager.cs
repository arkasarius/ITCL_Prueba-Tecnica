using System.Collections;
using Candidato.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Candidato.Scripts.Managers
{
   public class UIManager : MonoBehaviour
   {
      [SerializeField] private TMP_Text pointsText;
      [SerializeField] private TMP_Text timeText;
      [SerializeField] private TMP_Text timeupPoints;
      [SerializeField] private int currentScore;
      [SerializeField] private float currentTime=120f;
      [SerializeField] private GameObject pauseMenu;
      [SerializeField] private GameObject qualityMenu;
      [SerializeField] private GameObject timeUpMenu;
      
      /// <summary>
      ///   <para>Sets Subscriber to event for score increase.</para>
      /// </summary>
      private void OnEnable()
      {
         Events.ScoreIncrease += EventsScoreIncrease;
      }
      
      /// <summary>
      ///   <para>Unsubscribes from Score Increase</para>
      /// </summary>
      private void OnDisable()
      {
         Events.ScoreIncrease -= EventsScoreIncrease;
      }

      /// <summary>
      ///   <para>Updates Current score and updates UI to the new accumulated value.</para>
      /// </summary>
      /// <param name="score">Value of the increment of score.</param>
      private void EventsScoreIncrease(int score)
      {
         currentScore += score;
         pointsText.text = currentScore.ToString();
      }
      
      /// <summary>
      ///   <para>Starts the coroutine for the Time of the game.</para>
      /// </summary>
      private void Awake()
      {
         StartCoroutine(TimeHandler());
      }
      
      /// <summary>
      ///   <para>Coroutine that handles the timer of the game. Updates Ui over time and calls TimesUp function when we reach the time limit for the match.</para>
      /// </summary>
      private IEnumerator TimeHandler()
      {
         while(true){
           if (currentTime<=0)
           {
              currentTime = 0;
              UpdateTimeUI();
              TimesUp();
              yield break;
           }
           currentTime-=Time.deltaTime;
           UpdateTimeUI();
           yield return new WaitForEndOfFrame();
         }
      }
      
      /// <summary>
      ///   <para>Updates the UI text that represents the time. When we reach less than 10 seconds we show 1 decimal for a increase in tension.</para>
      /// </summary>
      private void UpdateTimeUI()
      {
         timeText.text = currentTime.ToString(currentTime>10 ? "f0" : "f1");
      }
      
      /// <summary>
      ///   <para>FlipFlop for the TimeScale to start/stop animations and particles and any time related callback. Activates PauseMenu Canvas if timescale is set to 0</para>
      /// </summary>
      public void TogglePause()
      {
         Time.timeScale = Time.timeScale == 0 ? 1 : 0;
         pauseMenu.SetActive(Time.timeScale == 0);
      }
      
      /// <summary>
      ///   <para>Resets current level and sets timescale to 1.</para>
      /// </summary>
      public void ResetLevel()
      {
         Time.timeScale = 1;
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }

      /// <summary>
      ///   <para>Toggles the quality Settings Canvas</para>
      /// </summary>
      public void ToggleQualitySettings()
      {
         if (qualityMenu.activeInHierarchy)
         {
            qualityMenu.SetActive(false);
            pauseMenu.SetActive(true);
         }
         else
         {
            qualityMenu.SetActive(true);
            pauseMenu.SetActive(false);
         }
      }
      
      /// <summary>
      ///   <para>Sets the Quality setting for the game. Stores the value as a PlayerPrefs "QualityLevel"</para>
      /// </summary>
      /// <param name="qualityLevel">sets the value for the quality settings. [0]->low [1]->medium [2]-> ultra </param>
      public void SetQualityLevel(int qualityLevel)
      {
         QualitySettings.SetQualityLevel(qualityLevel);
         PlayerPrefs.SetInt("QualityLevel",qualityLevel);
         ToggleQualitySettings();
      }

      /// <summary>
      ///   <para>Loads Main menu scene</para>
      /// </summary>
      public void LoadMenu()
      {
         Time.timeScale = 1;
         SceneManager.LoadScene(0);
      }
      
      /// <summary>
      ///   <para>Handles end of match logic. Stores High Score if we have a greater score than the PlayerPrefs "score" value. Shows the end of the match UI.</para>
      /// </summary>
      private void TimesUp()
      {
         Time.timeScale = 0;
         var gameScore = PlayerPrefs.GetInt("score");
         if (currentScore >= gameScore)
         {
            PlayerPrefs.SetInt("score",currentScore);
         }

         timeupPoints.text = "Puntos: " + currentScore.ToString();
         timeUpMenu.SetActive(true);
      }
   }
}

