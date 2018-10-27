using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;  
using System.Linq; 

public class Reply : MonoBehaviour {
	public string Post_Id;
	private string Res_ID;
	private string initialamount="0";

	// Use this for initialization

	void OnClick(){
		UIInput inputfield = GameObject.Find ("Input").GetComponentInChildren<UIInput> ();
		string str = inputfield.value;
		ParseObject RESPONSE = new ParseObject ("RESPONSE");
		RESPONSE ["R_Content"] = inputfield.value;
		RESPONSE ["Post_Id"] = Post_Id;
		RESPONSE ["User"] = ParseUser.CurrentUser.Username;
		RESPONSE ["Like"] = initialamount;
		RESPONSE ["DisLike"] = initialamount;

		RESPONSE.SaveAsync ().ContinueWith(t =>
		                                   {
			Loom.QueueOnMainThread (() => {
				Res_ID = RESPONSE.ObjectId;
				Debug.Log ("res:"+RESPONSE.ObjectId);
				UIScrollView scrollview = GameObject.Find ("Scroll View_Res").GetComponent<UIScrollView> ();
				GameObject []items =  GameObject.FindGameObjectsWithTag("PandR");
				GameObject o = (GameObject)Instantiate (Resources.Load ("Response"));
				//为每个预设设置一个独一无二的名称
				o.name = "Response"+items.Length;
				
				o.transform.parent = GameObject.Find ("Scroll View_Res").transform;		
				
				UILabel label_R = o.GetComponentInChildren<UILabel> ();
				label_R.text = str;
				
				AddLike_R Like_R=o.GetComponentInChildren<AddLike_R> ();
				Like_R.Response_Id = Res_ID;
				
				AddDislike_R DisLike_R=o.GetComponentInChildren<AddDislike_R>();
				DisLike_R.Response_Id=Res_ID;		

				GetPoster Poster=o.GetComponentInChildren<GetPoster>();
				Poster.UserAccount=ParseUser.CurrentUser.Username;

				Getuserphoto userphoto=o.GetComponentInChildren<Getuserphoto>();
				userphoto.UserAccount = ParseUser.CurrentUser.Username;
				
				//下面这段代码是因为创建预设时 会自动修改旋转缩放的系数，
				int i = items.Length - 1;
				Vector3 temp = new Vector3(0,-0.29f*i,0);
				GameObject item = GameObject.Find (o.name);
				item.transform.localPosition = new Vector3 (0, 140, 0);
				item.transform.localScale = new Vector3 (1, 1, 1);
				//列表添加后用于刷新listView 
				item.transform.position += temp;
				
				scrollview.ResetPosition ();
			});
		});

		inputfield.value = "";


	}
}
