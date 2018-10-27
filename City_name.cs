using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
public class City_name : MonoBehaviour {
	public UILabel city_name;
	// Use this for initialization
	void Start () {
		city_name.text=ParseUser.CurrentUser.Get<string>("place");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
