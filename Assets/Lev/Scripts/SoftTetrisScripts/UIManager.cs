using System.Collections.Generic;
using DG.Tweening;
using Tetris;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SoftTetris
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private float animationDuration, fallDistance, scaleAmount, squishAmount, squishOffset, targetAlpha, bounceScale;

        [System.Serializable]
        class GameOverMenuProperties
        {
            public TMP_Text lastScore;
            public TMP_Text maxScore;
            public GameObject menu;
        }

        [SerializeField] private GameOverMenuProperties gameOverMenu;

        [SerializeField] private GameObject[] buttons;

        private readonly List<GameObject> _objectsToDisable = new List
            <GameObject>();

        private Score _score;

        public UnityEvent OnGameOver { get; set; }

        public static UIManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(this);
            
            OnGameOver = new UnityEvent();

            _objectsToDisable.Add(transform.GetChild(0).gameObject);
            _objectsToDisable.Add(transform.GetChild(1).gameObject);

            OnGameOver.AddListener(ShowGameOverMenu);

            _score = FindObjectOfType<Score>();
        }

        private void ShowGameOverMenu()
        {
            gameOverMenu.menu.SetActive(true);
            foreach (var o in _objectsToDisable) o.SetActive(false);
            gameOverMenu.lastScore.text = _score.scoreText.text;
            gameOverMenu.maxScore.text = "Max score: " + _score.scoreText.text;
            BounceAnimation(gameOverMenu.lastScore);
        }

        private void BounceAnimation(TMP_Text textToAnimate)
        {
            Vector3 initialScale = textToAnimate.transform.localScale;
            Vector3 initialPosition = textToAnimate.transform.localPosition;
            
            Vector3 endPosition = initialPosition + Vector3.down * fallDistance;

            textToAnimate.transform.DOLocalMove(endPosition, animationDuration)
                .SetEase(Ease.OutQuad).OnStart(action: () => textToAnimate.transform.DOScale(initialScale * scaleAmount, animationDuration * 0.5f)
                    .SetEase(Ease.OutQuad))
                .OnComplete(() =>
                {
                    gameOverMenu.maxScore.gameObject.SetActive(true);
                    AnimateAlphaTextIncrease(gameOverMenu.maxScore);
                    BounceWithScale(gameOverMenu.maxScore.gameObject, true);
                });
        }

        void AnimateAlphaTextIncrease(TMP_Text textComponent)
        {
            Color initialColor = textComponent.color;
            initialColor.a = 0f;
            textComponent.color = initialColor;
            textComponent.DOFade(targetAlpha, animationDuration);
        }

        private void AnimateAlphaGameObjectIncrease(GameObject gameObject)
        {
            gameObject.SetActive(true);
            var image = gameObject.GetComponent<Image>();
            if(image != null)
            {
                Color initialColor = image.color;
                initialColor.a = 0f;
                image.color = initialColor;
                image.DOFade(targetAlpha, animationDuration);
            }
        }
        
        private void BounceWithScale(GameObject textComponent, bool showButtons = false)
        {
            Vector3 initialScale = textComponent.transform.localScale;
            textComponent.transform.localScale = initialScale;

            textComponent.transform.DOScale(initialScale * bounceScale, animationDuration)
                .SetEase(Ease.OutBounce).OnComplete(() =>
                {
                    if (showButtons)
                    {
                        var sequence = DOTween.Sequence();
                        foreach (var button in buttons)
                        {
                            sequence.AppendCallback(() =>
                            {
                                AnimateAlphaGameObjectIncrease(button.gameObject);
                                BounceWithScale(button);
                            });
                            sequence.AppendInterval(animationDuration);
                            sequence.Play();
                        }
                        sequence.Play();
                    }
                });
        }
    }
}

