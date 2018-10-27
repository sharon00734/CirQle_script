using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;  
using System.Linq;  
using Parse;

public class PostReport : MonoBehaviour {
	public string Post_Id;
	private int amount; 
	// Use this for initialization
	void OnClick () {
		var query = ParseObject.GetQuery("POST");
		query.GetAsync(Post_Id).ContinueWith(t =>
		                                     {
			ParseObject obj = t.Result;
			string str=obj["Post_report"].ToString();
			amount=int.Parse(str);
			amount++;
			obj["Post_report"]=amount.ToString();
			obj.SaveAsync ();
			Debug.Log("done!");

		});

	}
	/*IEnumerator UpdatePostReport(){
		
		var query = ParseObject.GetQuery("JUDGE").WhereEqualTo("Post_Id",Post_Id);
		
		var queryTask=query.FindAsync();
		while (!queryTask.IsCompleted) yield return null;
		
		IEnumerable<ParseObject> result = queryTask.Result;
		amount++;
		Debug.Log(amount);
		
		foreach (var obj in result) {
			string str=amount.ToString();
			obj["Like"] = str;
			obj.SaveAsync();
			Label.text=str;	
		}
		
		Debug.Log("done");
	}*/

}
