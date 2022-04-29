using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsController : MonoBehaviour
{
    [SerializeField] private List<SimpleSpawn> _spawners = new List<SimpleSpawn>();
    [SerializeField] private int _maxEnemy = 10;
    [SerializeField] private int _spawnRate = 3;
    [SerializeField] private int _enemyAtOnce = 2;
    private float _spawnTimer = 0;
    [SerializeField] private int _enemyAlive = 0;
    [SerializeField] private int _spaawnDilay = 5;
    [SerializeField] private string _nextScene;


    void Start()
    {
        StartCoroutine(LvlIsOver());
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer < 0 && _enemyAlive < _maxEnemy)
        {
            _spawnTimer = _spawnRate;
            for (int i = 0; i < Mathf.Min(_enemyAtOnce, _maxEnemy - _enemyAlive); i++)
            {
                int numberOfSpawn = Random.Range(0, _spawners.Count - 1);
                _spawners[numberOfSpawn].SpawnEnemy();
                _enemyAlive++;
                SimpleEnemy.OnDie += EnemyDie;
            }

        }
    }

    private void EnemyDie()
    {
        _enemyAlive--;
    }

    IEnumerator LvlIsOver()
    {
        yield return new WaitForSeconds(300f);
        SceneChanger.ChangeScene(_nextScene);
    }
}
