using System;  
using UnityEngine;  
using System.IO;  
using System.Xml;  
using System.Linq;  
using System.Text;  
using System.Collections.Generic;  
using System.Collections;
using Parse;

public class lbs_test : MonoBehaviour {
	public string geo_x="120.284812";
	public string geo_y="22.734059";
	// Use this for initialization
	void Start () {
		double result = Convert.ToDouble(geo_x);
		double result2 = Convert.ToDouble(geo_y);
		var point = new ParseGeoPoint(result2, result);

	
		ParseQuery<ParseObject> query = ParseObject.GetQuery("POST")
			.WhereWithinDistance("Post_Geo", point, ParseGeoDistance.FromKilometers(2.5));
		query.FindAsync().ContinueWith(t =>		{
			IEnumerable<ParseObject> nearbyLocations = t.Result;

			foreach (var obj in nearbyLocations) {
				string text = obj ["postfield"].ToString ();
				string place =obj["geo_place"].ToString ();
				//string post = obj ["postfield"].ToString ();
				//labeltext.Add(post);
				Debug.Log (text);  
			
				Debug.Log ("資料庫傳回:" + place);  

				
			}
			// nearbyLocations contains PlaceObjects within 5 miles of the user's location
		});

	}

}
