using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DrawGame
{
    public class UIManager : MonoBehaviour
    {
        public GameObject showColorPickerButton;

        public void ShowColorPicker()
        {
            showColorPickerButton.SetActive(true);
        }

        public void ExitColorPicker()
        {
            showColorPickerButton.SetActive(false);
        }

        public void DoneButton()
        {
            ScreenshotHandler.TakeScreenshot_Static(1920, 1080);
        }
    }
}