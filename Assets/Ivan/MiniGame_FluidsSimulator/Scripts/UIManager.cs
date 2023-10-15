using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ivan.MiniGame_FluidsSimulator.Scripts
{
    public class UIManager : MonoBehaviour
    {
        private const int DEFAULT = -160;
        private const int SELECT = -300;

        [SerializeField] private float delayOpen = 2;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;

        private Animator animator;

        private bool isOpen = false;
        private float valFloat;

        private void Start()
        {
            if(gridLayoutGroup != null)
            {
                animator = gridLayoutGroup.GetComponent<Animator>();
                Close();
            }
        }

        public void Open()
        {
            animator.SetTrigger("isOpen");
            isOpen = true;
        }

        public void Close()
        {
            animator.SetTrigger("isClose");
            isOpen = false;
        }
    }
}
