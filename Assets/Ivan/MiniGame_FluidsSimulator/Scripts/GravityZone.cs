using System.Collections.Generic;
using Ivan.MiniGame_FluidsSimulator.Scripts.Spawn;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ivan.MiniGame_FluidsSimulator.Scripts
{
    public class GravityZone : MonoBehaviour
    {
        [SerializeField] private SpawnInMouse spawnInMouse;
        
        [HideInInspector]
        public List<Rigidbody2D> objectsGravity = new List<Rigidbody2D>();

        [SerializeField] private int maxParticle = 155;

        [HideInInspector]
        public int waterCount;
        [HideInInspector]
        public int lavaCount;

        public float count;
        
        [Header("Температура")]
        public float _temperature;
        [SerializeField]
        private float lavaTemperature = 1f;
        [SerializeField]
        private float waterTemperature = -0.7f;
        
        [Header("UI")]
        [SerializeField] private TMP_Text TextTemperature;

        private void Start()
        {
            Physics2D.gravity = Vector2.zero;
        }

        private void FixedUpdate()
        {
            count = objectsGravity.Count;

            if (count > maxParticle)
            {
                spawnInMouse.canSpawn = false;
            }
            else
            {
                spawnInMouse.canSpawn = true;
            }
        }

        public void UpdateTemperature()
        {
            _temperature = (int)((lavaCount * lavaTemperature + waterCount * waterTemperature) * 0.5f);
            TextTemperature.text = _temperature + "°C";
        }
    }
}
