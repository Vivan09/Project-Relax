using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnim : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate() {
        if(Screen.width/2 > transform.position.x){
            rectTransform.rotation = Quaternion.Euler(0, 0, (transform.position.x - Screen.width/2) * -0.02f );
        }
        else{
            rectTransform.rotation = Quaternion.Euler(0, 0, (transform.position.x - Screen.width/2) * -0.02f );
        }
    }
}
