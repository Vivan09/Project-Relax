using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAnimalController : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private GameObject leafPrefab;

    private const int xOffset = 40;
    private void Start()
    {
        StartCoroutine(SpawnBird());
        StartCoroutine(SpawnLeaf());
    }
    private IEnumerator SpawnBird()
    {
        while (true)
        {
         
            yield return new WaitForSeconds(Random.Range(4, 10));
            Vector3 spawnPos = new Vector3(player.transform.position.x + xOffset, Random.Range(1.7f, 15));
            GameObject thisBird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);
           
        }
       

    }
    private IEnumerator SpawnLeaf()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 6));
            Vector3 spawnPos = new Vector3(player.transform.position.x + xOffset, Random.Range(22, 28));
            GameObject thisLeaf = Instantiate(leafPrefab, spawnPos, Quaternion.identity);

        }


    }
}
