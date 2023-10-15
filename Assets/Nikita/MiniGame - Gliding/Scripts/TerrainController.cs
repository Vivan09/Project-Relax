using UnityEngine;
using UnityEngine.U2D;

public class TerrainController : MonoBehaviour
{
    public SpriteShapeController shape { get; private set; }
    [SerializeField] private int scale;

    [SerializeField] private int numOfPoints = 50;
    [SerializeField] private GameObject triggerToGenerate;
    [SerializeField] private GameObject triggerToDestroy;
    private int numOfSpawningPoints;
    private int numOfGenerations = 0;
    private int currentScale;

    private EdgeCollider2D edgeCollider;


    private void Start()
    {
       edgeCollider = gameObject.GetComponent<EdgeCollider2D>();


        shape = GetComponent<SpriteShapeController>();


        shape.spline.SetPosition(2, shape.spline.GetPosition(2) + Vector3.right * scale);
        shape.spline.SetPosition(3, shape.spline.GetPosition(3) + Vector3.right * scale);


        GenerateTerraine(0);

    }


    public void GenerateTerraine(int index)
    {
        currentScale += scale;
        numOfSpawningPoints += numOfPoints ;

        numOfGenerations++;
        if (numOfGenerations >= 2)
        {
            MoveLastTwoPoints();
           
        }

        float distanceBetweenPoints = (float)scale / (float)numOfPoints;
        for (int i = index; i < numOfSpawningPoints; i++)
        {
            float xPos = shape.spline.GetPosition(i + 1).x + distanceBetweenPoints;
            float yPos = Mathf.PerlinNoise(i * Random.Range(5.0f, 15.0f), 0) * 30;
            //float yPos = Random.Range(0.0f, 1.0f) * 15;
            shape.spline.InsertPointAt(i + 2, new Vector3(xPos, yPos));
        }
        for (int i = index; i < numOfSpawningPoints; i++)
        {
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            shape.spline.SetLeftTangent(i, new Vector3(-2.0f, 0, 0));
            shape.spline.SetRightTangent(i, new Vector3(2.0f, 0, 0));
        }
        GameObject thisTrigger = Instantiate(triggerToGenerate, transform.TransformPoint(shape.spline.GetPosition(shape.spline.GetPointCount() - 10)), Quaternion.identity);
        thisTrigger.GetComponent<TriggerScript>().FindTarget();
        if (numOfGenerations >= 2)
        {
            GameObject destroyTrigger = Instantiate(triggerToDestroy, transform.TransformPoint(shape.spline.GetPosition(shape.spline.GetPointCount() - 46)), Quaternion.identity);
            destroyTrigger.GetComponent<TriggerScript>().FindTarget();

        }
       
        Vector2[] shapePoints = GetSpriteShapePoints();


        edgeCollider.points = shapePoints;

    }

    public void ClearPointsBetweenFirstAndLast()
    {
     
       
        for (int i = numOfSpawningPoints - numOfPoints + 1 ; i > 1; i--)
        {

            shape.spline.RemovePointAt(i);

        }
        MoveFirstTwoPoints();
        currentScale -= scale;
        numOfSpawningPoints -= numOfPoints;


    }
    private void MoveLastTwoPoints()
    {
        shape.spline.SetPosition(shape.spline.GetPointCount() - 2, shape.spline.GetPosition(shape.spline.GetPointCount() - 2) + Vector3.right * scale);
        shape.spline.SetPosition(shape.spline.GetPointCount() - 1, shape.spline.GetPosition(shape.spline.GetPointCount() - 1) + Vector3.right * scale);
    }
    private void MoveFirstTwoPoints()
    {

        shape.spline.SetPosition(0, shape.spline.GetPosition(0) + Vector3.right * scale );
        shape.spline.SetPosition(1, shape.spline.GetPosition(1) + Vector3.right * scale);

    }

    private Vector2[] GetSpriteShapePoints()
    {

        Vector2[] points = new Vector2[numOfSpawningPoints];
        for (int i = 0; i < numOfSpawningPoints; i++)
        {
            points[i] = shape.spline.GetPosition(i);
        }
        return points;
    }
}
