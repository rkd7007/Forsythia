using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMa : MonoBehaviour
{
    public static bool OptionClick = false;
    public static bool IsCameraClick = false;

    public void IsOption()
    {
        if (OptionClick)
            OptionClick = false;
        else if (!OptionClick)
            OptionClick = true;
    }

    public void CameraClick()
    {
        if (IsCameraClick)
            IsCameraClick = false;
        else if (!IsCameraClick)
            IsCameraClick = true;
    }
}
