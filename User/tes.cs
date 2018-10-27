using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;  
using System.Linq;  
using Parse;

public class tes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UILabel label = GetComponent<UILabel> ();
		label.text = ParseUser.CurrentUser.Username;
	}

}
