using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private int TotalZombiesToSpawn = 20;
    //public int MaxZombiesOnScreen;
    private int YSpawn = -20;
    private int YEnd = 16; //Barrier spawn
    private int minXSpawn = -15;
    private int maxXSpawn = 15;

    public float spawnDelaySeconds = 2.0f; //4


    [SerializeField] private GameObject _zombieGruntPrefab;

    private bool _enabled = false;
    private float _spawnTimer = 0.0f;
    private int _totalSpawned = 0;

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
                //Vector3 endGoalPosition = new Vector3(xSpawn, GameController.BARRIER_Y_POS, 0);

                GameObject zombieObject = Instantiate(_zombieGruntPrefab, startPosition, Quaternion.identity);
                zombieObject.name = "Grunt " + _totalSpawned.ToString();
                _totalSpawned++;
                GruntController grunt = zombieObject.GetComponent<GruntController>();

                if(grunt != null)
                {
                    grunt.Initialize(_pathFinding2d, endGoalPosition);
                }

                _spawnTimer = 0;
            }
        }
    }
}
