using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;

public class Score : MonoBehaviour {

	public string City;
	public UILabel label;
	
	private string score;
	// Use this for initialization
	void Start () {
		var query = ParseObject.GetQuery("Location").WhereEqualTo("City",City);
		query.FirstAsync ().ContinueWith (t =>
		                                  {
			ParseObject obj=t.Result;
			score = obj.Get<string>("Score");

			label.text=score;
				

		});
	}

}
