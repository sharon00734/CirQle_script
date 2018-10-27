using UnityEngine;
using System.Collections;

public class send_gps : MonoBehaviour {
	xml_test geo_y; 
	xml_test geo_x;	
	// Use this for initialization
	void Start () {
		double x = 120.299991;
		double y = 22.612185;
		string x1= x.ToString();
		string y1= y.ToString();
		Debug.Log(x1);
		Debug.Log(y1);
		xml_test geo_x = GameObject.Find("city_now").GetComponent<xml_test>();
		geo_x.geo_x = x1;
		
		xml_test geo_y = GameObject.Find("city_now").GetComponent<xml_test>();
		geo_y.geo_y = y1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
