using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class ButtonController : MonoBehaviour
    {
        public static Action OnAbilityButtonPressed;
        
        
        public void OnAbilityButtonClicked()
        {
            OnAbilityButtonPressed?.Invoke();
        }

        public void OnExitButtonClicked()
        {
            Application.Quit();
        }

        public void OnReloadButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
    }
}
