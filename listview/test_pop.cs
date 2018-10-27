using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;


public class test_pop : MonoBehaviour {

	int i = 0;
	// Use this for initialization
	void Start () {
		//DateTime myData = new DateTime();
		ArrayList post_scoreid = new ArrayList ();
		DateTime? dat= DateTime.Now;
//		dat.AddDays(-1);//減少一天


		var queryT = ParseObject.GetQuery ("POST2").OrderByDescending("createdAt");
		
		queryT.FindAsync ().ContinueWith (t2 => 
		                                  {
			IEnumerable<ParseObject> result2 = t2.Result;
			
			foreach (var obj in result2) {
				string text = obj ["postfield"].ToString ();
				//string like  =obj ["sum"].ToString ();
				DateTime? updatedAt =obj.CreatedAt;
				//DateTime myData=obj.CreatedAt;
				//DateTime updatedAt1 = obj.Get<DateTime>("createdAt");
				
				Debug.Log ("資料庫TAG:" + text);  
				//Debug.Log ("資料庫TAG:" + like);  
				Debug.Log (updatedAt);  
				//post_scoreid.Add (text);
				
			}

			
		});
		
	}
}
