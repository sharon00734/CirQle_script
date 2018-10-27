using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

public class ChangePwd : MonoBehaviour {
	public UIInput email;
	// Use this for initialization
	void OnClick(){
		Task requestPasswordTask = ParseUser.RequestPasswordResetAsync(email.value);
		email.value = "信件已寄出!";
	}
}
