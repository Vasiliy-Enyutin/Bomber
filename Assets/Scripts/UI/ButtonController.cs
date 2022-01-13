using System;
using UnityEngine;

namespace UI
{
    public class ButtonController : MonoBehaviour
    {
        public static Action OnAbilityButtonPressed;
        
        
        public void OnAbilityButtonClicked()
        {
            OnAbilityButtonPressed?.Invoke();
        }
    }
}
