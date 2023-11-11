using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_behaviour : MonoBehaviour
{
    [SerializeField] int _maxLife = 100;
    [SerializeField] Transform _playerTransform;
    private Animator _animator;
    [SerializeField] SkinnedMeshRenderer _meshRenderer;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Material _dmgMaterial;
    float _speed;
    int _life;



    // Start is called before the first frame update
    void Start()
    {
        _life = _maxLife;
        _animator = GetComponent<Animator>();
        int t = Random.Range(0, 2);
        Debug.Log(t);
        _animator.SetFloat("Walk_Type", t);
    }

    // Update is called once per frame
    void Update()
    {

        _agent.SetDestination(_playerTransform.position);
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

    IEnumerator Die()
    {
        _agent.isStopped = true;
        int t = Random.Range(0, 2);
        _animator.SetFloat("Die_Type", t);
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);

    }
    IEnumerator ColorizeDamage()
    {
        var c = _meshRenderer.material;
        _meshRenderer.material= _dmgMaterial;
        yield return new WaitForSeconds(.1f);
        _meshRenderer.material = c;
    }
}
