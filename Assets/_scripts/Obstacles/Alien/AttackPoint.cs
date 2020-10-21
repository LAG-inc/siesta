using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    private Animator _animator;
    private static readonly int AnimExplosion = Animator.StringToHash("Explosion");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        _animator.Rebind();
    }

    public void Explosion()
    {
        _animator.SetTrigger(AnimExplosion);
    }
}