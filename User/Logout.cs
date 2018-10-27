using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

public class Logout : MonoBehaviour {
	public GameObject LoginPage;
	public GameObject Profile;
	public GameObject SettingPage;

	void OnClick(){
		ParseUser.LogOut();
		var currentUser = ParseUser.CurrentUser;
		SettingPage.SetActive (false);
		Profile.SetActive (false);
		LoginPage.SetActive (true);
	}

}
