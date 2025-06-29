using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class EnemiesLifebarManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _lifeBarPrefab;

    [SerializeField]
    private float _lifebarTime;

    private Dictionary<Entity, EnemyLifeBar> _enemyLifeBars = new();

    private void OnEnable()
    {
        UiActions.CreateLifeBar += CreateLifeBar;
        UiActions.RemoveLifeBar += DestroyBar;
    }

    private void OnDisable()
    {
        UiActions.CreateLifeBar -= CreateLifeBar;
        UiActions.RemoveLifeBar -= DestroyBar;
    }

    private void CreateLifeBar(Entity entity)
    {
        if (_enemyLifeBars.ContainsKey(entity))
        {
            _enemyLifeBars[entity].UpdateLifeBar();
        }
        else
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(entity.transform.position);
            GameObject currentBar = Instantiate(_lifeBarPrefab, transform);
            currentBar.transform.position = screenPos;
            EnemyLifeBar enemyLifeBar = currentBar.GetComponent<EnemyLifeBar>();
            enemyLifeBar.SetUp(entity, 2.0f);
            _enemyLifeBars.Add(entity, enemyLifeBar);
        }
    }

    public void DestroyBar(Entity entity)
    {
        if (_enemyLifeBars.ContainsKey(entity))
        {
            _enemyLifeBars.Remove(entity);
        }
    }
}
