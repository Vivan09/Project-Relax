using System.Collections;
using UnityEngine;

namespace Ivan.MiniGame_FluidsSimulator.Scripts.Spawn
{
    public class SpawnInMouse : MonoBehaviour
    {
        [SerializeField] private GameObject prefabFluid;
        [HideInInspector] public bool canSpawn = true;

        [SerializeField] private float delaySpawn = 0.2f;

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                if (prefabFluid != null && canSpawn)
                {
                    StartCoroutine("SpawnFluid");
                }
            }
        }

        private IEnumerator SpawnFluid()
        {
            yield return new WaitForSeconds(delaySpawn);
        
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Instantiate(prefabFluid, mousePosition, Quaternion.identity);
        }

        public void ChangeFluid(GameObject prefab)
        {
            prefabFluid = prefab;
        }
    }
}
