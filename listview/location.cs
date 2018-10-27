using UnityEngine;
using System.Collections;

public class location : MonoBehaviour {
	public GameObject ob1;
	// Use this for initialization
	void OnClick () {
		ob1=GameObject.Find("Q");
		item script = ob1.GetComponent<item> ();
		//Debug.Log (script.postcity);
		//Debug.Log (script.posttype);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
