using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject button;

    private void OnMouseDown()
    {
        if (button.activeSelf)
        {
            button.SetActive(false); //Button off
        }
        else if (!button.activeSelf)
        {
            button.SetActive(true); //Button on
        }
    }
}
