using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Nikita
{

    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject tutorial;
        [SerializeField] private TextMeshProUGUI distanceTravelledTxt;
        [SerializeField] private Slider soundSlider;
        [SerializeField] private AudioSource musicSource;
        
        public int DistanceTravelled;

        public void OnTutorialButtonPress()
        {
            tutorial.SetActive(false);
        }
        private void Update()
        {
            if(distanceTravelledTxt != null)
            {
                distanceTravelledTxt.text = DistanceTravelled.ToString();
            }
           
        }
        public void MusicValueChanged()
        {
           // musicSource.volume = soundSlider.value;
           //має бути пофікшено та заново додано після одобрення пула на плейліст музики

        }
    }
}
