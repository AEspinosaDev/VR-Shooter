using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemieManager : MonoBehaviour
{
    const int MAX_ENEMIES = 200;
    const int MAX_LIFE = 100;
    const float DISTANCE = 30.0f;
    Queue<GameObject> _enemies = new Queue<GameObject>();
    [SerializeField] GameObject _enemiePrefab;
    [SerializeField] Transform _playerTransform;
    [SerializeField] float _spawnInterval = 4;


    // Start is called before the first frame update
    void Start()
    {
        //Create Pool
        for (int i = 0; i < MAX_ENEMIES; i++)
        {
            var ins = Instantiate(_enemiePrefab, new Vector3(0, _playerTransform.position.y, 20), Quaternion.identity);
            _enemies.Enqueue(ins);
            ins.GetComponent<Zombie_behaviour>().SetPlayerTransform(_playerTransform);
            ins.SetActive(false);
        }
        InvokeRepeating("SpawnEnemy", _spawnInterval, _spawnInterval);
    }




    void SpawnEnemy()
    {
        if (_enemies.Count == 0) return;
        var enemy = _enemies.Dequeue();

        int degree = Random.Range(0, 391);
        Vector3 spawn_point = Quaternion.AngleAxis(degree, Vector3.up) * new Vector3(0.0f, 1.0f, DISTANCE);
        enemy.SetActive(true);
        enemy.transform.position = _playerTransform.position + spawn_point;
        var behaviour = enemy.GetComponent<Zombie_behaviour>();
        behaviour.SetManager(this);
        behaviour.SetLife(MAX_LIFE);
        behaviour.SetConfig(Random.Range(0, 3));
        behaviour.SetPlayerTransform(_playerTransform);

    }
    public void Pool(GameObject g)
    {
        g.SetActive(false);
        _enemies.Enqueue(g);
    }
}
