using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConnectionScript : MonoBehaviour
{
    [SerializeField] private GameObject object1;
  [SerializeField]  private GameObject object2;
    private bool isConnectionChanged;
   // private GameObject connection;
   // private DragAndDrop dragAndDrop;
    private Vector3 connectionPos;
    public void Init(GameObject obj1, GameObject obj2/*, bool isObjHeld*/)
    {
        object1 = obj1;
        object2 = obj2;
        //isObjectHeld = isObjHeld;
    }
    void Start()
    {
        
    }

 
    void Update()
    {
       
        if (object1 != null && object2 != null && object2.transform.position.x - object1.transform.position.x != 0 && object2.transform.position.y - object1.transform.position.y != 0)
        {
            isConnectionChanged = object1.GetComponent<BlockManager>().isConnectionChanged;
            CalculateConnectionPositon();
        }
        
     

        
    }
  
    private void CalculateConnectionPositon()
    {
        connectionPos = new Vector3((object1.transform.position.x + object2.transform.position.x) / 2, (object1.transform.position.y + object2.transform.position.y) / 2, (object1.transform.position.z + object2.transform.position.z) / 2);
        gameObject.transform.position = connectionPos;

        float sumOfSquares = Mathf.Pow(object2.transform.position.y - object1.transform.position.y, 2) + Mathf.Pow(object2.transform.position.x - object1.transform.position.x, 2);
        float distanceBetweenObjects = Mathf.Sqrt(sumOfSquares);

        float angleCos = (object2.transform.position.y - object1.transform.position.y) / distanceBetweenObjects;
        float angleInRadians = Mathf.Acos(angleCos);
        float angleInDegrees = angleInRadians * (180 / Mathf.PI);
    
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, distanceBetweenObjects / 1.3f, gameObject.transform.localScale.z);
      
           


            if (object2.transform.position.x > object1.transform.position.x)
            {

                gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, -angleInDegrees);
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, angleInDegrees);
            }


    }
    public void BreakConnection()
    {
        Destroy(gameObject);
    }
    
}
