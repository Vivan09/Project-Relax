using System.Collections.Generic;
using UnityEngine;


namespace SoftTetris
{
    public class Piece : MonoBehaviour
    {
        private Board Board { get; set; }

        private Rigidbody2D PieceRigidbody { get; set; }

        public Rigidbody2D[] CellsRigidbodies { get; private set; }

        public TetrominoData Data { get; set; }

        private Vector3Int Position { get; set; }
        public List<GameObject> Cells { get; private set; }

        public void Initialize(Board board, Vector3Int position, TetrominoData data)
        {
            Board = board;
            Position = position;
            Data = data;
            Cells = new List<GameObject>();
            CellsRigidbodies = new Rigidbody2D[20];
            PieceRigidbody = GetComponent<Rigidbody2D>();

            FixedJoint2D lastRootBone = null;

            List<DistanceJoint2D> distanceJoints = new List<DistanceJoint2D>();
            
            for (int i = 0; i < data.Cells.Length; i++)
            {
                CreateCell(new Vector3Int(data.Cells[i].x, data.Cells[i].y, 0) + Position, ref lastRootBone, i);
                distanceJoints.Add(gameObject.AddComponent<DistanceJoint2D>());
                distanceJoints[i].autoConfigureDistance = true;
                distanceJoints[i].connectedBody = lastRootBone.GetComponent<Rigidbody2D>();
            }

            lastRootBone.enabled = false;
            
            SetRigidbodiesVelocity(Board.velocity);
        }

        public void MovePiece(Vector2 direction)
        {
            foreach (var rigidbody in CellsRigidbodies) rigidbody.AddForce(direction * Board.moveForce);
            
            PieceRigidbody.AddForce(direction * Board.moveForce);
        }

        void CreateCell(Vector3 position, ref FixedJoint2D lastRootBone, int i)
        {
            Cells.Add(Instantiate(Data.tile, position, Quaternion.identity, transform));
            FixedJoint2D cellRootBone = Cells[i].GetComponentInChildren<FixedJoint2D>();

            if(i != 0) lastRootBone.connectedBody = cellRootBone.gameObject.GetComponentInChildren<Rigidbody2D>();

            lastRootBone = cellRootBone;
            
            for (int j = 0; j < 5; j++)
                CellsRigidbodies[j + i * 5] = Cells[i].transform.GetChild(j).GetComponent<Rigidbody2D>();
        }
        
        public void Rotate(int direction)
        {
            transform.Rotate(0, 0, Board.rotationAngle * direction);
            int n = Cells.Count;

            //FixedJoint2D lastRootBone = null;
            Vector3 cellPosition = Vector3.zero;

            for(int i = 0; i < n; i++)
            {
                if (!Board.IsValidPosition(Cells[i].transform.GetChild(4).transform.position))
                {
                    transform.Rotate(0, 0, -(Board.rotationAngle * direction));
                    break;
                }

                /*cellPosition = Cells[i].transform.position;
                Destroy(Cells[i]);
                CreateCell(cellPosition, ref lastRootBone, i);*/
            }

            //lastRootBone.enabled = false;
        }

        public void SetRigidbodiesVelocity(Vector2 velocity)
        {
            foreach (var rigidbody in CellsRigidbodies) rigidbody.velocity = velocity;

            PieceRigidbody.velocity = velocity;
        }
        
    }
} 
 
