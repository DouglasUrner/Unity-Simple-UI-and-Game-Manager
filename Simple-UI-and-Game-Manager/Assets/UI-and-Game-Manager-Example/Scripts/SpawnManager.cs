using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *
 */

public class SpawnManager : MonoBehaviour
{
  public float startDelay = 2.0f;
  public float spawnRate = 2.0f;

  public GameObject[] objectPrefabs;

  [SerializeField]
  public Vector3 spawnRange;  // XXX - set/get.

  void Start()
  {
    spawnRange = new Vector3(
      transform.localScale.x/2,
      transform.localScale.y/2,
      transform.localScale.z/2
    );
    InvokeRepeating("SpawnRandomObject", startDelay, spawnRate);
  }

  void SpawnObject(int index)
  {
      
  }

  void SpawnRandomObject()
  {
    var prefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];
    var spawnPos = new Vector3(
      Random.Range(
        transform.position.x - spawnRange.x, transform.position.x + spawnRange.x
      ),
      Random.Range(
        transform.position.y - spawnRange.y, transform.position.y + spawnRange.y
      ),
      Random.Range(
        transform.position.z - spawnRange.z, transform.position.z + spawnRange.z
      )
    );
    Instantiate(prefab, spawnPos, prefab.transform.rotation);
  }
}
