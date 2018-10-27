using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;


public class Login : MonoBehaviour {
	public GameObject failed;
	public GameObject login;
	public UIInput email;
	public UIInput password;
	// Use this for initialization
	void OnClick(){
			
		if (email != null && password != null) {
			ParseUser.LogInAsync (email.value, password.value).ContinueWith (t =>
			{
				Loom.QueueOnMainThread (() => {
					if (t.IsFaulted || t.IsCanceled) {
						failed.SetActive (true);
					} else {
						login.SetActive(false);
					}
				});
			});
		}

	}
}
