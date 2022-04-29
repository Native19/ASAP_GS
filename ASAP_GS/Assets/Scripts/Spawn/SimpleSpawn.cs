using UnityEngine;

public class SimpleSpawn : MonoBehaviour
{
    [SerializeField] private Transform _enemyPrefab;
    [SerializeField] private int _spawnRange = 1;
    
    public void SpawnEnemy ()
    {
        Vector3 position = new Vector3(transform.position.x + Random.Range(-_spawnRange, _spawnRange), transform.position.y + Random.Range(-_spawnRange, _spawnRange), transform.position.z);
        Instantiate(_enemyPrefab, position, Quaternion.identity);
    }
}
