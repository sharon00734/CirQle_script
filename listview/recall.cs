using UnityEngine;
using System.Collections;

public class recall : MonoBehaviour {


	void start() {

		UIButton name = GameObject.Find ("confirm").GetComponent<UIButton> ();
		name.name="更改成功!";
		Debug.Log (name.name);
	}

}
