using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
	[SerializeField] private GameObject _zombieGruntPrefab;

	private int _totalZombiesToSpawn = 20;
	//public int MaxZombiesOnScreen;
	private int _ySpawn = -20;
	private int _yEnd = 16; //Barrier spawn
	private int _minXSpawn = -15;
	private int _maxXSpawn = 15;

	private float _spawnDelaySeconds = 2.0f; //4

	private bool _enabled = false;
	private float _spawnTimer = 0.0f;
	private int _totalSpawned = 0;

	private SimplePathFinding2D _pathFinding2d; //TODO: Load this reference directly

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

			if(_spawnTimer >= _spawnDelaySeconds)
			{
				int xSpawn = Random.Range(_minXSpawn, _maxXSpawn);
				Vector3 startPosition = new Vector3(xSpawn, _ySpawn, 0);
				Vector3 endGoalPosition = new Vector3(xSpawn, _yEnd, 0);
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
