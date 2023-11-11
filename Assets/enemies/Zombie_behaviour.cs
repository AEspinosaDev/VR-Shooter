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

    }
    private void OnEnable()
    {
        _agent.isStopped = false;
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

    public void ReceiveDamage(int dmg)
    {
        _life -= dmg;
        StartCoroutine(ColorizeDamage());
        if (_life <= 0)
        {
            StartCoroutine(Die());
        }
    }
    public void SetLife(int l)
    {
        _life = l;
    }

    IEnumerator Die()
    {
        _agent.isStopped = true;
        int t = Random.Range(0, 2);
        _animator.SetFloat("Die_Type", t);
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(5.0f);

        _manager.Pool(gameObject);

    }
    IEnumerator ColorizeDamage()
    {
        var c = _meshRenderer.material;
        _meshRenderer.material = _dmgMaterial;
        yield return new WaitForSeconds(.1f);
        _meshRenderer.material = c;
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
}
