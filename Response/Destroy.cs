using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	// Use this for initialization
	void OnClick(){
		GameObject post = GameObject.Find("MainPost");
		Destroy (post);
		GameObject []items =  GameObject.FindGameObjectsWithTag("PandR");
		for (var i = 0; i < items.Length; i++) {
			
			Destroy(items[i]);
			
		}
	}
}
