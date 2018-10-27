using UnityEngine;
using System.Collections;

public class PostIdSender : MonoBehaviour {
	public string Post_Id;
	// Use this for initialization
	void Start(){
		UIPlayTween TweenTarget=GetComponent<UIPlayTween>();
		GameObject g = GameObject.Find ("list View").transform.FindChild ("warning_block").gameObject;
		TweenTarget.tweenTarget=g;
	}
	void OnClick(){
		GameObject g = GameObject.Find ("warning_block");
		g.transform.position = new Vector3 (0, 0, 0);
		PostReport Report = g.GetComponentInChildren<PostReport> ();
		Report.Post_Id = Post_Id;
	}
}
