using Isometric2DGame.Enemy;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _entity;

    [SerializeField]
    private float _time;

    [SerializeField]
    private Transform _pool;

    [SerializeField]
    private List<Transform> _spawnPoints = new();

    private void Start()
    {
        InvokeRepeating(nameof(Create), 0, _time);
    }

    private void Create()
    {
        GameObject currentEnemy = Instantiate(_entity, _pool);
        currentEnemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;
        currentEnemy.GetComponent<Enemy>().SetUp(_spawnPoints);
    }
}
