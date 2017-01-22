using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelSwitch : MonoBehaviour {

    public GameObject start;
    public GameObject Menu1;
    public GameObject Menu2;

    // Use this for initialization
    public void ChangePanel()
    {
        if (start.activeSelf)
        {

            Menu1.SetActive(true);
            start.SetActive(false);

        }

        else if (Menu1.activeSelf)
        {
            Menu2.SetActive(true);
            Menu1.SetActive(false);
        }
        else
        {
            Menu2.SetActive(false);
            Menu1.SetActive(true);
        }

    }
}
