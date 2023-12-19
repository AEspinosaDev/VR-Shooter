using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie_behaviour : MonoBehaviour
{
    private Transform _playerTransform;
    private Animator _animator;
    [SerializeField] SkinnedMeshRenderer _meshRenderer;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Material _dmgMaterial;
    EnemieManager _manager;
    float _speed;
    int _life;
    [SerializeField] int _damage = 25;

    List<Material> _materials = new List<Material>();


    private void Awake()
    {

        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

        int t = Random.Range(0, 2);
        _animator.SetFloat("Walk_Type", t);
        SetConfig(1);
        foreach (var m in _meshRenderer.materials)
        {
            _materials.Add(m);
        }

    }
    private void OnEnable()
    {
        _agent.isStopped = false;
        GetComponent<Collider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent != null)
            _agent.SetDestination(_playerTransform.position);
    }
    public void SetPlayerTransform(Transform t)
    {
        _playerTransform = t;
    }
    public void SetManager(EnemieManager m)
    {
        _manager = m;
    }

    public bool ReceiveDamage(int dmg)
    {
        _life -= dmg;
        StartCoroutine(ColorizeDamage());
        if (_life <= 0)
        {
            StartCoroutine(Die());
            return true;
        }
        return false;
    }
    public void SetLife(int l)
    {
        _life = l;
    }

    IEnumerator Die()
    {
        _agent.isStopped = true;
        GetComponent<Collider>().enabled = false;
        int t = Random.Range(0, 2);
        _animator.SetFloat("Die_Type", t);
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(5.0f);

        _manager.Pool(gameObject);

    }
    IEnumerator ColorizeDamage()
    {

        for (int i = 0; i < _meshRenderer.materials.Length; i++)
        {
            _meshRenderer.materials[i] = _dmgMaterial;
        }
        yield return new WaitForSeconds(.1f);
        for (int i = 0; i < _meshRenderer.materials.Length; i++)
        {
            _meshRenderer.materials[i] = _materials[i];
        }
    }
    public void SetConfig(int conf)
    {
        switch (conf)
        {
            case 0:
                _animator.SetFloat("Speed", 1f);
                _agent.speed = 0.5f;
                break;
            case 1:
                _animator.SetFloat("Speed", 1.2f);
                _agent.speed = 0.7f;
                break;
            case 2:
                _animator.SetFloat("Speed", 1.4f);
                _agent.speed = 0.9f;
                break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Attack(collision.gameObject.GetComponent<Player>()));
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(Attack(collision.gameObject.GetComponent<Player>()));
        }
    }

    IEnumerator Attack(Player player)
    {
        while (true)
        {
            player.DecreaseLife(_damage);
            yield return new WaitForSeconds(2.0f);

        }

    }
}
