using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHundler : MonoBehaviour
{
    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
