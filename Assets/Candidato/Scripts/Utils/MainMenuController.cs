using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Candidato.Scripts.Utils
{
    public class MainMenuController : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject levelsMenu;
        public GameObject levelBlockMenu;
        public GameObject graphicsMenu;
        public GameObject eraseDataMenu;
        public TMP_Text points;
        public TMP_Text quality;
        
        /// <summary>
        ///   <para>Activates levelMenu Canvas</para>
        /// </summary>
        public void PlayButton()
        {
            mainMenu.SetActive(false);
            levelsMenu.SetActive(true);
        }
        
        /// <summary>
        ///   <para>Activates graphicsMenu Canvas</para>
        /// </summary>
        public void GraphicsButton()
        {
            mainMenu.SetActive(false);
            graphicsMenu.SetActive(true);
        }
        
        /// <summary>
        ///   <para>Activates eraseDataMenu Canvas</para>
        /// </summary>
        public void EraseDataButton()
        {
            mainMenu.SetActive(false);
            eraseDataMenu.SetActive(true);
        }

        /// <summary>
        ///   <para>Exists application. Not supported on Editor.</para>
        /// </summary>
        public void ExitButton()
        {
            Application.Quit();
        }

        /// <summary>
        ///   <para>Deactivates all Canvas and Activates mainMenu Canvas.</para>
        /// </summary>
        public void ReturnMenuButton()
        {
            levelsMenu.SetActive(false);
            graphicsMenu.SetActive(false);
            eraseDataMenu.SetActive(false);
            levelBlockMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        
        /// <summary>
        ///   <para>Sets the game Quality level and stores the value on PlayerPrefs "QualityLevel"</para>
        /// </summary>
        /// <param name="qualityLevel">The value for the quality setting. For this Game we have [0]->low [1]->medium [2]->ultra </param>
        public void SetQualityLevel(int qualityLevel)
        {
            QualitySettings.SetQualityLevel(qualityLevel);
            PlayerPrefs.SetInt("QualityLevel",qualityLevel);
            switch (qualityLevel)
            {
                case 0:
                    quality.text = "Gráficos : Low";
                    break;
                case 1:
                    quality.text = "Gráficos : Medium";
                    break;
                case 2:
                    quality.text = "Gráficos : Ultra";
                    break;
            }
            
        }
        /// <summary>
        ///   <para>Updates the UI acording to the PlayerPrefs "score"</para>
        /// </summary>
        private void SetPoints()
        {
            points.text = "Puntuación Máxima: " + PlayerPrefs.GetInt("score").ToString();
        }

        /// <summary>
        ///   <para>Erases all data stored on PlayerPrefs "score"</para>
        /// </summary>
        public void ErasePoints()
        {
            PlayerPrefs.SetInt("score",0);
            SetPoints();
            ReturnMenuButton();
        }
        
        /// <summary>
        ///   <para>Updates points and Quality settings acording to PlayerPrefs</para>
        /// </summary>
        private void Awake()
        {
            SetPoints();
            SetQualityLevel(PlayerPrefs.GetInt("QualityLevel"));
        }

        /// <summary>
        ///   <para>Loads the corresponding level. Checks if we have the amount of points needed in order to enter Hard difficulty.</para>
        /// </summary>
        /// <param name="level">Value of index for the level on Scenes. For this Game we have [1]->Easy [2]->Hard</param>
        public void LoadLevel(int level)
        {
            if (level == 2)
            {
                if (CheckPoints())
                {
                    SceneManager.LoadScene(level);
                }
                else
                {
                    levelsMenu.SetActive(false);
                    levelBlockMenu.SetActive(true);
                }
            }
            else
            { 
                SceneManager.LoadScene(level);  
            }
                
                
                
        }
        
        /// <summary>
        ///   <para>Checks if we have at least 3000 points as a High score stored on PlayerPrefs "score". Returns null if we dont have at least 3000</para>
        /// </summary>
        private bool CheckPoints()
        {
            return PlayerPrefs.GetInt("score") >= 3000;
        }
    }
}
