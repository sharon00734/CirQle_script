using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

public class changeName : MonoBehaviour {
	public UIInput name;
	// Use this for initialization
	void OnClick(){
		string str = name.value;
		ParseUser.Query.WhereEqualTo ("username", ParseUser.CurrentUser.Username).FindAsync ().ContinueWith (t =>
		{
			IEnumerable<ParseUser> Name = t.Result;
			foreach(var obj in Name){
				obj["name"] = str;
				obj.SaveAsync();
			}

		});
		name.value="更改成功!";
	}
}
