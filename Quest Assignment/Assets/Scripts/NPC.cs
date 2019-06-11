using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    #region Varibles
    public GameObject startQuest; // Declaring the start quest object
    #endregion
    #region Start
    void Start()
    {
        startQuest.SetActive(false); // Bool for the quest feature
    }
    #endregion

}
