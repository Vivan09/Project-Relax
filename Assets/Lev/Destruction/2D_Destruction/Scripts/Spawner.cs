using System.Collections;
using UnityEngine;

namespace Destruction
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Vector2 spawnPosition;        
        
        public DestructibleObject ActivePiece { get; set; }

        public static Spawner Instance;
        private static bool _isRunning = false;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(Instance);
            
            //StartCoroutine(SpawnPiece());
        }
        
        public IEnumerator SpawnPiece()
        {
            if (_isRunning)
                yield break;

            _isRunning = true;
            yield return new WaitForSeconds(1f);
            Instantiate(ActivePiece, spawnPosition, Quaternion.identity);
            _isRunning = false;
        }
    }
}

