using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
	private const int TOTAL_AISLES = 3;

	[SerializeField] private GameObject _zombieGruntPrefab;

	private GameplayController _gameController;

	public int totalZombiesToSpawn = 2; //TODO: How to keep track of zombies killed? Gamecontroller?
	//public int MaxZombiesOnScreen;
	//OLD
	// private int _ySpawn = -20;
	// private int _yEnd = 16; //Barrier spawn
	// private int _minXSpawn = -15;
	// private int _maxXSpawn = 15;

	private float _xSpawn = 40.0f;
	private float[][] _ySpawnRanges = new float[][]
	{
		//new float[] {13.4f, 10.6f}, //TODO: Store these values per Aisle or something
		new float[] {7.4f, 4.6f},
		new float[] {1.4f, -1.4f},
		new float[] {-4.6f, -7.4f},
		//new float[] {-10.6f, -13.4f},
	} ;

	private float _spawnDelaySeconds = 5.0f; //4

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

		_gameController = GameplayController.instance; //TODO: Pass this in! -btai

		//TODO: Preload some zombies at the start!
	}

	private void Update()
	{
		if(_enabled && _totalSpawned < totalZombiesToSpawn)
		{
			_spawnTimer += Time.deltaTime;

			if(_spawnTimer >= _spawnDelaySeconds)
			{
				int aisleIndex = Random.Range(0, TOTAL_AISLES);
				float[] aisleYSpawnRange = _ySpawnRanges[aisleIndex];
				float ySpawn = Random.Range(aisleYSpawnRange[0], aisleYSpawnRange[1]);
				Vector3 startPosition = new Vector3(_xSpawn, ySpawn, 0);
				//Vector3 endGoalPosition = new Vector3(_xSpawn, _yEnd, 0);
				//Vector3 endGoalPosition = new Vector3(xSpawn, GameController.BARRIER_Y_POS, 0);

				GameObject zombieObject = Instantiate(_zombieGruntPrefab, startPosition, Quaternion.identity);
				zombieObject.name = "Grunt " + _totalSpawned.ToString();
				_totalSpawned++;

				GruntController grunt = zombieObject.GetComponent<GruntController>();
				if(grunt != null)
				{
					grunt.Initialize(_gameController);
				}

				_spawnTimer = 0;
			}
		}
	}
}
