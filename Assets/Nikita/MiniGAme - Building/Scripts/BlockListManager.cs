using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockListManager : MonoBehaviour
{
   [SerializeField] private List<GameObject> blockList;
    public GameObject lastBlock { get; private set; }
    


    


    public void CheckNeedSpecialBlock()
    {
        if (blockList.Count > 0)
        {
            if (blockList.Count % 8 == 0)
            {
                lastBlock = blockList[blockList.Count - 1];
            }
            else
            {
                lastBlock = null;
            }
        }
      
       

    }
    internal void AddBlock(GameObject newBlock)
    {
        blockList.Add(newBlock);
    }
    public int GetBlocksCount() => blockList.Count;

    public GameObject FindNearestBlock(GameObject thisBlock, GameObject newBlock, GameObject nearestBlock)
    {


        float dist = Mathf.Infinity;
        Vector3 position = newBlock.transform.position;

        foreach (GameObject block in blockList)
        {



            if (block != null && block != thisBlock && block != newBlock)
            {
                Vector3 difference = block.transform.position - position;
                //Vector3 newDirection = Vector3.RotateTowards(transform.forward, difference, speed * Time.deltaTime, 0.0f);
                //transform.rotation = Quaternion.LookRotation(newDirection);

                float currDist = difference.sqrMagnitude;



                if (currDist < dist)
                {
                    nearestBlock = block;
                    dist = currDist;

                }
            }


        }


        return nearestBlock;


    }
}

