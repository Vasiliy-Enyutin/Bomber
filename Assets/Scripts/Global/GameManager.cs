using System;
using System.Collections;
using EnemyLogic;
using UnityEngine;

namespace Global
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float stopGameTimer;
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private GameObject _winPanel;
        
        private void OnEnable()
        {
            GlobalEventStorage.OnPlayerDestroy += ShowLosePanel;
            GlobalEventStorage.OnEnemyDestroy += CheckForWin;
        }
        

        private void OnDisable()
        {
            GlobalEventStorage.OnPlayerDestroy -= ShowLosePanel;
            GlobalEventStorage.OnEnemyDestroy -= CheckForWin;
        }

        private void ShowLosePanel()
        {
            StartCoroutine(StopGameRoutine(_losePanel));
        }
        
        private void CheckForWin()
        {
            Debug.Log(FindObjectsOfType<Enemy>().Length);
            if (FindObjectsOfType<Enemy>().Length <= 0)
                ShowWinPanel();
        }

        private void ShowWinPanel()
        {
            StartCoroutine(StopGameRoutine(_winPanel));
        }

        private IEnumerator StopGameRoutine(GameObject gameEndPanel)
        {
            yield return new WaitForSeconds(stopGameTimer);
            gameEndPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
