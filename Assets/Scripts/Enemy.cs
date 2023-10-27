using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour, IDieable
{
    [SerializeField] private Gun _gun;
    [SerializeField] private Animator _animator;

    private Health _health;
    private bool _findedPlayer = false;
    private Player _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _player))
        {
            _animator.SetBool("finded_player", true);
            _findedPlayer=true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _animator.SetBool("finded_player", false);
            _findedPlayer = false;
        }
    }

    private void Update()
    {
        if (_findedPlayer)
        {
            transform.LookAt(_player.transform.position);
            _gun.TryFire();
        }
    }

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.Died += Die;
    }

    public void Die() => Destroy(gameObject);
}
