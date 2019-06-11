using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonQuest : MonoBehaviour
{
    #region Varibles
    public GameObject button; // Declaring the button in the scene
    #endregion
    #region Start
    void Start()
    {
        button.SetActive(false); // Bool for the quest button

    }
    #endregion


}
