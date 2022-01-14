using System.Collections;
using UI;
using UnityEngine;

namespace PlayerLogic.Bomb
{
    [RequireComponent(typeof(PlayerStats))]
    public class BombSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _bombPrefab;
        private PlayerStats _playerStats;
        private bool _isOnCooldown = false;
        

        private void Awake()
        {
            _playerStats = GetComponent<PlayerStats>();
        }
        
        private void OnEnable()
        {
            ButtonController.OnAbilityButtonPressed += SpawnBomb;
        }
        
        private void OnDisable()
        {
            ButtonController.OnAbilityButtonPressed -= SpawnBomb;
        }

        private void SpawnBomb()
        {
            if (_isOnCooldown == true)
                return;
            
            Instantiate(_bombPrefab, transform.position, Quaternion.identity);
            StartCoroutine(CooldownRoutine());
        }

        private IEnumerator CooldownRoutine()
        {
            _isOnCooldown = true;
            yield return new WaitForSeconds(_playerStats.AbilityReloadTime);
            _isOnCooldown = false;
        }
    }
}
