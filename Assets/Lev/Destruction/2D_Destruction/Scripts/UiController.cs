using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
    
namespace Destruction
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private RectTransform choseObject;
        [SerializeField] private GameObject arrow;
        [SerializeField] private Transform toggle;
        [SerializeField] private float moveDistance;

        [SerializeField] private Vector3 choseObjectScale;

        [SerializeField] private Button chooseButton;
        [SerializeField] private DestructibleObject[] destructibleObjects;

        [SerializeField] private Transform buttonsParent;

        [SerializeField] private bool isUp = true;

        void Start()
        {
            GenerateButtons();
            toggle.GetComponent<Button>()?.onClick.AddListener(OpenCloseMenu);
        }

        private void GenerateButtons()
        {
            for(int i = 0; i < destructibleObjects.Length; i++)
            {
                var newButton = Instantiate(chooseButton, buttonsParent);
                var i1 = i;
                newButton.onClick.AddListener(delegate
                {
                    choseObject.gameObject.transform.position = buttonsParent.transform.GetChild(i1).transform.position;
                    choseObject.localScale = choseObjectScale;
                    choseObject.gameObject.transform.parent = newButton.transform;
                    choseObject.gameObject.transform.SetAsFirstSibling();
                    Spawner.Instance.ActivePiece = destructibleObjects[i1];
                });
                var image = newButton.transform.GetChild(0).GetComponent<Image>();
                image.sprite = destructibleObjects[i].uiImage;
                if(i == 0)
                    for(int j = 0; j < 2; j++)
                        newButton.onClick.Invoke();
            }
        }

        private void OpenCloseMenu()
        {
            if (isUp)
            {
                Vector3 endValue = toggle.localPosition + Vector3.down * moveDistance;
                
                toggle.DOLocalMove(endValue, 0.5f)
                    .SetEase(Ease.InOutQuart)
                    .OnStart(() => arrow.transform.DORotate(new Vector3(0, 0, 180), 0.5f));
                isUp = false;
            }
            else
            {
                Vector3 endValue = toggle.localPosition + Vector3.up * moveDistance;
                
                toggle.DOLocalMove(endValue, 0.5f).SetEase(Ease.InOutQuart)
                    .OnStart(() => arrow.transform.DORotate(Vector3.zero, 0.5f));
                isUp = true;
            }
        }
    }
}

