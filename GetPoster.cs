using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;

public class GetPoster : MonoBehaviour {
	public string UserAccount;
	private UILabel Label;
	// Use this for initialization
	void Start () {
		Label = GetComponent<UILabel> ();
		var query = ParseUser.Query.WhereEqualTo ("username", UserAccount);
		query.FirstAsync ().ContinueWith (t =>
		{
			ParseUser result = t.Result;
			Label.text=result["name"].ToString();
		});
	}

}
