using System.Collections;
using UnityEngine;

public class DestroyAfterSpawn : MonoBehaviour
{
	[SerializeField] private float _secondsToLive;
	private void Start()
	{
		StartCoroutine(CountdownToDestroy());
	}

	private IEnumerator CountdownToDestroy()
	{
		yield return new WaitForSeconds(_secondsToLive);
		Destroy(this.gameObject);
	}
}
