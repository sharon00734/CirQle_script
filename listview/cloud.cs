using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;  
using System.Linq;  

public class cloud : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Debug.Log ("hi");
		IDictionary<string, object> parms = new Dictionary<string, object>
		{
			{ "tag1", "停車" },
			{ "tag1_city","kaoshiung" }
		};
		ParseCloud.CallFunctionAsync<IDictionary<string, object>>("tag_computing", parms).ContinueWith(t => {
			var score = t.Result;
			// ratings is 4.5
		}); 
	



		/*  兩種情境使用 沒按過讚 按讚 或是按過噓 收回噓
		IDictionary<string, object> parms = new Dictionary<string, object>
		{
			{ "score", "Eh8OzBTKDP" }
		};
		ParseCloud.CallFunctionAsync<IDictionary<string, object>>("score_computing_add", parms).ContinueWith(t => {
			var score = t.Result;
			// ratings is 4.5
		}); 
		*/  
		/*
		 //兩種情境使用 收回讚 或是 按噓
		IDictionary<string, object> parms = new Dictionary<string, object>
		{
			{ "score", "zl7w1y1VFL" }
		};
		ParseCloud.CallFunctionAsync<IDictionary<string, object>>("score_computing_addquit", parms).ContinueWith(t => {
			var score = t.Result;
			// ratings is 4.5
		});*/

	}
	/* score_computing_addquit
		Debug.Log ("hi");
		var query = ParseObject.GetQuery ("Judge")
			.WhereEqualTo ("judge_type", "like");
		query.CountAsync ().ContinueWith (t =>
		{
			int count = t.Result;
			Debug.Log (count);
		});
	 */ 
	// Update is called once per frame

}
