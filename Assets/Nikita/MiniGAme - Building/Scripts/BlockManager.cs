using UnityEngine;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private Text triggerText;
    [SerializeField] private GameObject gameObjectPreset;
    [SerializeField] private GameObject connectionPreset;
    [SerializeField] private GameObject spawnEffect;
    [SerializeField] private SceneMovementScr movingPart;
    [SerializeField] private SceneMovementScr miniMapMovingPart;
    [SerializeField] public Sprite stoneSprite;
    [SerializeField] private float movingSpeed = 3f;

    private Rigidbody2D grabbedObj;
    private GameObject newBlock;
    private GameObject connection;
    private GameObject connection2;

    private ConnectionScript connectionScript;
    private ConnectionScript connectionScript2;

    public bool isConnectionChanged;
    private bool isObjectHeld;

    private DistanceJoint2D distanceJoint;
    private DistanceJoint2D distanceJoint2;

    [SerializeField] private BlockListManager blockListManager;
    public GameObject nearestBlock;

    private const float TRIGGER_MIN_DISTANCE = 2.5f;

    public void Init(BlockListManager blockListManager, SceneMovementScr movingPart, SceneMovementScr miniMapMovingPart, Text triggerText)
    {
        this.blockListManager = blockListManager;
        this.movingPart = movingPart;
        this.miniMapMovingPart = miniMapMovingPart;
        this.triggerText = triggerText;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                gameObjectPreset = Resources.Load<GameObject>("Body");
                connection = null; connectionScript = null;
                isObjectHeld = true;

                newBlock = Instantiate(gameObjectPreset.gameObject, gameObject.transform.position, gameObject.transform.rotation);
                blockListManager.AddBlock(newBlock);
                newBlock.GetComponent<BlockManager>().Init(blockListManager, movingPart, miniMapMovingPart, triggerText);

                blockListManager.CheckNeedSpecialBlock();

                connection = Instantiate(connectionPreset, gameObject.transform.position, Quaternion.identity);

                connectionScript = connection.GetComponent<ConnectionScript>();
                connectionScript.Init(gameObject, newBlock);

                if (blockListManager.GetBlocksCount() >= 2 && blockListManager.FindNearestBlock(gameObject, newBlock, nearestBlock) != null)
                {
                    connection2 = Instantiate(connectionPreset, gameObject.transform.position, Quaternion.identity);

                    connectionScript2 = connection2.GetComponent<ConnectionScript>();
                    connectionScript2.Init(newBlock, blockListManager.FindNearestBlock(gameObject, newBlock, nearestBlock));
                }

                newBlock.layer = 6;

                foreach (Transform child in newBlock.transform)
                {
                    child.gameObject.layer = 6;
                }

                grabbedObj = newBlock.GetComponent<Rigidbody2D>();
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0) && isObjectHeld)
        {
            ConnectJoints();
            isObjectHeld = false;
            Instantiate(spawnEffect, newBlock.transform.position, newBlock.transform.rotation);

            if (newBlock != null)
            {
                newBlock.layer = 10;

                foreach (Transform child in newBlock.transform)
                {
                    child.gameObject.layer = 10;
                }
            }

            if (blockListManager.lastBlock != null)
            {
                blockListManager.lastBlock.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                blockListManager.lastBlock.GetComponent<SpriteRenderer>().sprite = stoneSprite;
                blockListManager.lastBlock.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            }

            grabbedObj = null;

            MoveFieldIfNeeds();
        }
    }

    private void MoveFieldIfNeeds()
    {
        Debug.Log(newBlock.transform.position);
        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)));

        float topYPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float blockYPos = newBlock.transform.position.y;
        float distanceToTopBorder = Mathf.Abs(topYPos - blockYPos);

        if (distanceToTopBorder < TRIGGER_MIN_DISTANCE)
        {
            movingPart.StartMove("MY");
        }
    }

    private void OnJointBreak2D(Joint2D brokenJoint)
    {
        if (connection != null)
        {
            if (connection.TryGetComponent(out ConnectionScript component))
            {
                component.BreakConnection();
            }
            else
            {
                Debug.LogError("ConnectionScript is not available");
            }
        }
        else
        {
            Debug.LogError("Connection is not available");
        }
    }

    private void ConnectJoints()
    {
        distanceJoint = gameObject.AddComponent<DistanceJoint2D>();
        distanceJoint.connectedBody = newBlock.GetComponent<Rigidbody2D>();
        distanceJoint.breakForce = 170;

        if (blockListManager.GetBlocksCount() >= 2 && blockListManager.FindNearestBlock(gameObject, newBlock, nearestBlock) != null)
        {
            distanceJoint2 = newBlock.AddComponent<DistanceJoint2D>();
            distanceJoint2.connectedBody = blockListManager.FindNearestBlock(gameObject, newBlock, nearestBlock).GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    void FixedUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (grabbedObj != null)
        {
            Vector3 objPos = grabbedObj.position;
            Vector3 vel = mousePos - objPos;
            grabbedObj.velocity = vel * movingSpeed;
        }
    }
}