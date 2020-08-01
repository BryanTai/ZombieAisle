using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public int TotalZombiesToSpawn = 20;
    //public int MaxZombiesOnScreen;
    public int YSpawn = -20;
    public int YEnd = 16;
    public int minXSpawn = -20;
    public int maxXSpawn = 20;

    public float spawnDelaySeconds = 4.0f;


    [SerializeField] private GameObject _zombieGruntPrefab;

    private bool _enabled = false;
    private float _spawnTimer = 0.0f;

    private SimplePathFinding2D _pathFinding2d;

    public void ToggleSpawner(bool enabled)
    {
        _enabled = enabled;
        _spawnTimer = 0.0f;
    }

    private void Start()
    {
        if(_zombieGruntPrefab == null)
        {
            Debug.LogError("MISSING ZOMBIE GRUNT PREFAB IN SPAWNER!");
        }

        _pathFinding2d = GameObject.Find("Grid").GetComponent<SimplePathFinding2D>();

        //TODO: Preload some zombies at the start!
    }

    private void Update()
    {
        if(_enabled)
        {
            _spawnTimer += Time.deltaTime;

            if(_spawnTimer >= spawnDelaySeconds)
            {
                int xSpawn = Random.Range(minXSpawn, maxXSpawn);
                Vector3 startPosition = new Vector3(xSpawn, YSpawn, 0);
                Vector3 endGoalPosition = new Vector3(xSpawn, YEnd, 0);

                GameObject zombieObject = Instantiate(_zombieGruntPrefab, startPosition, Quaternion.identity);
                GruntController controller = zombieObject.GetComponent<GruntController>();

                if(controller != null)
                {
                    controller.Initialize(_pathFinding2d, endGoalPosition);
                }

                _spawnTimer = 0;
            }
        }
    }
}
