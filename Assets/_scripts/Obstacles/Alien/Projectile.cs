using System.Collections;
using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private ParticleSystem destroyParticle;

    private SpriteRenderer _sprite;
    private Vector3 _target;
    private AlienBehavior _alienBehavior;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();
        destroyParticle = destroyParticle == null ? GetComponent<ParticleSystem>() : destroyParticle;
        _alienBehavior = _alienBehavior == null
            ? transform.parent.parent.GetComponentInChildren<AlienBehavior>()
            : _alienBehavior;
    }

    private void OnEnable()
    {
        _sprite.enabled = true;
        _collider.enabled = true;
        transform.position = _alienBehavior.gameObject.transform.position;
        _target = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, velocity * Time.deltaTime);
        if (transform.position == _target) StartCoroutine(CBoom());
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _target = other.transform.position;
        Debug.Log("You've Die");
        StartCoroutine(CBoom());
        //CodeGameOver
    }


    private IEnumerator CBoom()
    {
        //Desactivar recursos visuales
        _collider.enabled = false;
        _sprite.enabled = false;
        destroyParticle.Play();

        yield return new WaitForSeconds(1.5f);
        //Retornar a la lista de proyectiles disponibles
        _alienBehavior.ProjectileDestroy(gameObject);
        //Regresar el proyectile a la cola
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }
}