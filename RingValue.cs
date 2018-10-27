using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;

public class RingValue : MonoBehaviour {
	//public List<float> values;
	public GameObject g;
	public string City;
	// Use this for initialization
	void Start () {
		GetValue (City);
	}

	public void GetValue(string city){
		g.transform.rotation = Quaternion.Euler(0,0,0);
		var query = ParseObject.GetQuery("POST").WhereEqualTo("foo","angel").WhereEqualTo("Location",city);
		query.CountAsync ().ContinueWith(t =>
		                                 {
			int count_a = t.Result;
			Debug.Log("a:"+count_a);
			
			var query2 = ParseObject.GetQuery("POST").WhereEqualTo("foo","devil").WhereEqualTo("Location",city);
			
			query2.CountAsync ().ContinueWith(t2 =>
			                                  {
				int count_d = t2.Result;				
				Debug.Log("d:"+count_d);				
				int ad=count_a+count_d;
				decimal dec = Math.Round((decimal)count_a / ad,1);
				float rate= (float)dec;
				float x=120*rate;
				Debug.Log("x"+x);
				float result = 60-x;
				Debug.Log("re:"+result);
				Loom.QueueOnMainThread(()=>
				                       {
					//g.transform.rotation = Quaternion.Euler(0,0,60);
					g.transform.Rotate(0, 0, result, Space.World);
				});
				//var rotationVector = transform.rotation.eulerAngles;
				//rotationVector.z = result;
				
				//g.transform.Rotate(0, 0, result, Space.World);
				
			});
			
		});
	}

}
