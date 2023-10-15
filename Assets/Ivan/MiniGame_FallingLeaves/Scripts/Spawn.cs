using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawn : MonoBehaviour
{
    public GameObject[] Spawners;
    public float deltaTime = 5f;

    public GameObject Apple;
    public GameObject Leaf;

    private IEnumerator Start()
    {
        while (true)
        {
            int randomObj = Random.Range(0, 2);
            int randomSpawn = Random.Range(0, Spawners.Length);
            Quaternion randomRotation = new Quaternion(Random.Range(0, 180), 0, 0, 0);

            if (randomObj == 0)
            {
                var obj = Instantiate(Apple, Spawners[randomSpawn].transform.position, randomRotation);
                obj.transform.SetParent(Spawners[randomSpawn].transform);
            }
            else
            {
                var obj = Instantiate(Leaf, Spawners[randomSpawn].transform.position, randomRotation);
                obj.transform.SetParent(Spawners[randomSpawn].transform);
            }

            yield return new WaitForSeconds(deltaTime);
        }
    }
}
