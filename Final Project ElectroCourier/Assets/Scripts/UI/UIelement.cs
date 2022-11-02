using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This is the base class for UIelements that are updated by the UI Manager

public class UIelement : MonoBehaviour
{

    // Updates the UI elements UI accordingly
    // This is a "virtual" function so it can be overridden by classes that inherit from the UIelement class
    public virtual void UpdateUI()
    {

    }
}
