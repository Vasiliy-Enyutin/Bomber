using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SoftBlock : MonoBehaviour, IDestroyable
{
    private Animator _animator;
        
    public static event Action OnSoftBlockDestroy;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        OnSoftBlockDestroy?.Invoke();
    }

    public void Destroy()
    {
        PlayDieAnimation();
    }

    private void PlayDieAnimation()
    {
        _animator.Play("Die");
    }

    private void OnAnimationEnded()     // called from animator
    {
        Destroy(gameObject);
    }
}
