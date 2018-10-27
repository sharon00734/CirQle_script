using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleScene : MonoBehaviour {

	public Toggle togglebad;
	public Toggle togglegood;
	public Toggle toggleQ;
	public string post_type;
	void Start()
	{
		togglebad.onValueChanged.AddListener(OnValChangedbad);
		togglegood.onValueChanged.AddListener(OnValChangedgood);
		toggleQ.onValueChanged.AddListener(OnValChangedQ);
	}

	void OnValChangedbad(bool check)
	{
		if (check != false) {
			post_type = "devil";
			Debug.Log (post_type);
		}
		else 
			Debug.Log ("not:"+post_type);
	}
	void OnValChangedgood(bool check)
	{

		if (check != false) {
			post_type = "angel";
			Debug.Log (post_type);
		}
		else 
			Debug.Log ("not:"+post_type);
		//return post_type;
	}
	void OnValChangedQ(bool check)
	{

		if (check != false) {
			post_type = "q";
			Debug.Log (post_type);
		}
		else 
			Debug.Log ("not:"+post_type);
		//return post_type;
	}
	

//checkbox.value = false;
//Debug.Log("do it! " + checkbox.value);
}
