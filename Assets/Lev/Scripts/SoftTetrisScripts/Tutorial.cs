using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SoftTertris
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer tutorialImage;
        [SerializeField] private float timeToWait, fadeDuration;

        void Awake()
        {
            StartCoroutine(WaitForTouch());
            StartCoroutine(CloseMenu());
        }
        
        IEnumerator WaitForTouch()
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Input.touchCount > 0);
            StopCoroutine(CloseMenu());
            FadeOutImage();
        }

        IEnumerator CloseMenu()
        {
            yield return new WaitForSeconds(timeToWait);
            StopCoroutine(WaitForTouch());
            FadeOutImage();
        }
        
        private void FadeOutImage()
        {
            Color targetColor = tutorialImage.color;
            targetColor.a = 0f;

            tutorialImage.DOColor(targetColor, fadeDuration);
        }
    }
}