using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> trashPrefabs;
    public GameObject spawnPos;

    public float spawnRate = 2f;
    private float spawnDelay = 0;

    private void Update()
    {
        if (Time.time >= spawnDelay)
        {
            var index = Random.Range(0, trashPrefabs.Count);
            Instantiate(trashPrefabs[index], spawnPos.transform.position, Quaternion.identity);

            spawnDelay = Time.time + 20f / spawnRate;
        }
    }
}
