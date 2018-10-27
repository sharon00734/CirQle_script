using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;

public class Album : MonoBehaviour {
	public int Score;
	public string City;
	public GameObject Lock;
	public GameObject collection;
	public GameObject name;
	public GameObject unknown;
	// Use this for initialization
	void Start () {
		var query = ParseObject.GetQuery("Location").WhereEqualTo("City",City);
		query.FindAsync ().ContinueWith (t =>
		                                 {
			Loom.QueueOnMainThread (() => {
				IEnumerable<ParseObject> result = t.Result;
				foreach (var obj in result) {
					string score = obj ["Score"].ToString ();
					if(int.Parse (score)>=Score){
						//GameObject collection=GameObject.Find("Box").transform.FindChild("collection").gameObject;

						collection.SetActive(true);
						name.SetActive(true);
						Lock.SetActive(false);
						unknown.SetActive(false);


					}
				}
				
			});
		});
	}

}
