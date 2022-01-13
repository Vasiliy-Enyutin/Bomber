using System;
using System.Collections;
using DestroyableObjects;
using UnityEngine;

namespace PlayerLogic.Bomb
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private float _timer;


        private void Awake()
        {
            StartCoroutine(TimerRoutine());
        }

        private IEnumerator TimerRoutine()
        {
            yield return new WaitForSeconds(_timer);
            BlowUp();
        }

        private void BlowUp()
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
