using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField] private float velocity;

    private Vector3 _target;
    private CircleCollider2D _collider;
    private Vector3 _initialPosition;
    private Animator _animator;
    private static readonly int AnimExplosion = Animator.StringToHash("Explosion");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _initialPosition = transform.position;
        _collider = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        transform.position = _initialPosition;

        _collider.enabled = true;

        _animator.Rebind();

        var vector = new Vector3(Mathf.Abs(_target.x - transform.position.x),
            Mathf.Abs(_target.y - transform.position.y));

        _target = GameObject.FindGameObjectWithTag("Player").transform.position;

        transform.rotation = Quaternion.Euler(0, 0, Util.GetAngleFromVector(vector));
    }

    private void FixedUpdate()
    {
        if (GameManager.SI.currentGameState == GameState.MainMenu) return;

        transform.position = Vector3.MoveTowards(transform.position, _target, velocity * Time.deltaTime);

        if (transform.position == _target) Boom();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _target = other.transform.position;
        Boom();
    }


    private void Boom()
    {
        SFXManager.SI.PlaySound(Sound.Meteorite);
        _collider.enabled = false;
        _animator.SetTrigger(AnimExplosion);
    }
}