using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelSwitch : MonoBehaviour {

    public GameObject start;
    public GameObject Menu1;
    public GameObject Menu2;
	public GameObject Menu3;
	public GameObject Menu4;

    // Use this for initialization
    public void ChangePanelNext()
    {
		if (start.activeSelf) {
			start.SetActive (false);
			Menu1.SetActive (true);
		} else if (Menu1.activeSelf) {
			Menu1.SetActive (false);
			Menu2.SetActive (true);
		} else if (Menu2.activeSelf) {
			Menu2.SetActive (false);
			Menu3.SetActive (true);
		} else if (Menu3.activeSelf) {
			Menu3.SetActive (false);
			Menu4.SetActive (true);
		}

    }

	public void ChangePanelPrevious(){
		if (Menu1.activeSelf) {
			Menu1.SetActive (false);
			start.SetActive (true);
		} else if (Menu2.activeSelf) {
			Menu2.SetActive (false);
			Menu1.SetActive (true);
		} else if (Menu3.activeSelf) {
			Menu3.SetActive (false);
			Menu2.SetActive (true);
		} else if (Menu4.activeSelf) {
			Menu4.SetActive (false);
			Menu3.SetActive (true);
		}
	}
}
