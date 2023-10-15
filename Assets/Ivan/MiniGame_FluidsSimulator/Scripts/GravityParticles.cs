using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ivan.MiniGame_FluidsSimulator.Scripts
{
    public class GravityParticles : MonoBehaviour
    {
        private Vector2 _centrPosition; // Позиція центру притягання

        private float _timerThreshold = 1.6f;
        private bool _isTimerRunning = false;
        private float _timer = 0f; // Час таймера

        private Rigidbody2D _rigidbody2D; // Rigidbody2D цієї частинки
        private Rigidbody2D _rigidbody2DGravity; // Rigidbody2D зони притягання
        private GravityZone _gravityZone; // Зона притягання
        private GravityParticles _gravityParticles; // Інша частинка в зоні притягання

        [SerializeField]
        public VariantsParticles variantsParticles; // Варіанти частинок (трава, лава, вода, земля)

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>(); // Отримати компонент Rigidbody2D при початку
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _gravityZone = other.GetComponent<GravityZone>(); // Отримати компонент GravityZone зони притягання
            _rigidbody2DGravity = other.GetComponent<Rigidbody2D>(); // Отримати компонент Rigidbody2D зони притягання
            _gravityZone.objectsGravity.Add(_rigidbody2D); // Додати Rigidbody2D цієї частинки до списку об'єктів притягання в зоні
            _centrPosition = other.transform.position; // Зберегти позицію центру притягання

            // Додати цю частинку до відповідного списку частинок у зоні притягання (лава або вода)
            if (variantsParticles == VariantsParticles.Lava)
                _gravityZone.lavaCount++;
            else if (variantsParticles == VariantsParticles.Water)
                _gravityZone.waterCount++;

            _gravityZone.UpdateTemperature(); // Оновити температуру в зоні притягання
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _gravityZone.objectsGravity.Remove(_rigidbody2D); // Видалити Rigidbody2D цієї частинки зі списку об'єктів притягання в зоні
            _gravityZone = null; // Знищити зв'язок із зоною притягання
        }

        private void FixedUpdate()
        {
            if (_gravityZone != null)
            {
                var position = _rigidbody2D.position; // позиция частички
            
                Vector2 directionToCentre = (_centrPosition - position).normalized;
                float distance = (_centrPosition - position).magnitude;
                float strength = 10 * _rigidbody2DGravity.mass * _rigidbody2D.mass / distance;

                _rigidbody2D.AddForce(directionToCentre * strength); // притягання

                if (distance < 0.2f) // обьект в центре
                {
                    _rigidbody2D.bodyType = RigidbodyType2D.Static;
                }

                if (_rigidbody2D.velocity.magnitude < 0.25f) // если обьект не движетья
                {
                    if (!_isTimerRunning)
                    {
                        StartTimer();
                    }
                    else
                    {
                        UpdateTimer();
                        if (_timer >= _timerThreshold)
                        {
                            _rigidbody2D.bodyType = RigidbodyType2D.Static;
                            ResetTimer();
                        }
                    }
                }
                else
                {
                    ResetTimer();
                }

            }
        }

        private void StartTimer()
        {
            _isTimerRunning = true;
            _timer = 0f;
        }
    
        private void UpdateTimer()
        {
            _timer += Time.fixedDeltaTime;
        }
    
        private void ResetTimer()
        {
            _isTimerRunning = false;
            _timer = 0f;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            _gravityParticles = other.gameObject.GetComponent<GravityParticles>();
            
            if (_gravityParticles != null)
            {
                if (variantsParticles == VariantsParticles.Water) // взаємодія води
                {
                    if (_gravityParticles.variantsParticles == VariantsParticles.Lava)
                    {
                        Destroy(gameObject);
                        Destroy(other.gameObject);
                    }
                }
                else if(variantsParticles == VariantsParticles.Grass) // взаємодія трави
                {
                    if (_gravityParticles.variantsParticles == VariantsParticles.Lava)
                    {
                        Destroy(gameObject);
                    }
                }
                else if (variantsParticles == VariantsParticles.Ground) // взаємодія землі
                {
                    if (_gravityParticles.variantsParticles == VariantsParticles.Grass)
                    {
                        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
                        
                        int layerMask = LayerMask.GetMask("Grass");
                    
                        RaycastHit2D hit = Physics2D.Raycast(position2D, _centrPosition - position2D, 0.2f , layerMask);
                        if (hit.collider != null)
                        {
                            Debug.DrawLine(transform.position, _centrPosition);

                            GameObject touchedObject = hit.collider.gameObject;

                            GravityParticles gravityParticles = touchedObject.GetComponent<GravityParticles>();
                            if (gravityParticles != null)
                            {
                                Destroy(touchedObject);
                                Debug.Log("ISSSSenfejfefejfefebf");
                            }
                        }
                    }
                }

                if (other.gameObject.GetComponent<GravityTrees>()) // взаємодія з деревом
                {
                    
                }
            }
            
            
        }
    }
    public enum VariantsParticles
    {
        Grass,
        Lava,
        Water,
        Ground,
    }
}
