using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SetDark : MonoBehaviour {
	public GameObject obj;
	public GameObject HotOrLoca;
	public string iden;
	// Use this for initialization
	void OnClick(){

		if (iden == "loca" || iden == "hot") {
			if(iden == "hot"){
				GameObject h= GameObject.Find("place(Clone)").transform.FindChild("dark").gameObject;
				h.SetActive(false);
			}

			if(iden == "loca"){
				GameObject g= GameObject.Find("hot(Clone)").transform.FindChild("dark").gameObject;
				g.SetActive(false);
			}

		} else {
			GameObject [] buttons = GameObject.FindGameObjectsWithTag("Dark");
			for (var j = 0; j < buttons.Length; j++) {			
				buttons[j].SetActive(false);			
			}
		}
		obj.SetActive (true);
	}
}
