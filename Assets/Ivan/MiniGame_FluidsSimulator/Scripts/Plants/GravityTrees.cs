using System;
using UnityEngine;
using DG.Tweening;

namespace Ivan.MiniGame_FluidsSimulator.Scripts
{
    public class GravityTrees : MonoBehaviour
    {
        private Vector2 _centrPosition;
        private Quaternion rotation;

        private Rigidbody2D _rigidbody2D;
        private Rigidbody2D _rigidbody2DGravity;
        private GravityZone _gravityZone;

        private VariantsParticles _variantsParticles;

        [Header("Спрайти")] 
        [SerializeField] private GameObject SeedSrite;
        [SerializeField] private GameObject TreeSprite;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _gravityZone = other.GetComponent<GravityZone>();
            _rigidbody2DGravity = other.GetComponent<Rigidbody2D>();
            _gravityZone.objectsGravity.Add(_rigidbody2D);
            _centrPosition = other.transform.position;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _gravityZone.objectsGravity.Remove(_rigidbody2D);
            _gravityZone = null;
        }

        private void FixedUpdate()
        {
            if (_gravityZone != null)
            {
                var position = _rigidbody2D.position; // позиция частички

                Vector2 directionToCentre = (_centrPosition - position).normalized;
                float distance = (_centrPosition - position).magnitude;
                float strength = 10 * _rigidbody2DGravity.mass * _rigidbody2D.mass / distance;

                Vector2 postion2D = new Vector2(transform.position.x, transform.position.y);
                rotation = Quaternion.FromToRotation(-transform.up, _centrPosition - postion2D);
                transform.rotation = rotation * transform.rotation;

                _rigidbody2D.AddForce(directionToCentre * strength);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(other.gameObject.name);
            
            var _gravityParticles = other.gameObject.GetComponent<GravityParticles>();
            
            if (_gravityParticles != null)
            {
                if (_gravityParticles.variantsParticles == VariantsParticles.Grass)
                {
                    gameObject.transform.parent = other.gameObject.transform;
                    _rigidbody2D.bodyType = RigidbodyType2D.Static;
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    transform.rotation = rotation * transform.rotation;
                    ChangeSprite();
                }
            }
            else if (_gravityParticles == null && other.gameObject.GetComponent<GravityTrees>() == null)
            {
                Destroy(gameObject);
            }
        }

        private void ChangeSprite()
        {
            SeedSrite.SetActive(false);
            TreeSprite.SetActive(true);
            TreeSprite.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        }
    }
}
