using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;


public class Runtime_Res : MonoBehaviour {
	public string Post_Id;
	public UIScrollView scrollview;
	//int count = 0;
	int i = 0;
	string PostText;
	string[] labeltext = new string[100];
	string[] ResID=new string[15];
	int[] label_num = new int[10];
	
	//public string post_text = null;
	//int post_num=0;
	// Use this for initialization
	
	void Start (){
		
		scrollview = GameObject.Find ("Scroll View").GetComponent<UIScrollView> ();
		
		Loom.RunAsync (() => {

			var queryP = ParseObject.GetQuery ("POST");
			queryP.GetAsync(Post_Id).ContinueWith(t =>
			                                     {

				ParseObject obj = t.Result;
				PostText =obj["postfield"].ToString();

				Loom.QueueOnMainThread (() => {

						//產出POST
					GameObject g = (GameObject)Instantiate (Resources.Load ("Post"));
					g.name = "MainPost";
					g.transform.parent = GameObject.Find ("Scroll View").transform;
						//得到文字对象
					UILabel label = g.GetComponentInChildren<UILabel> ();
					label.text = PostText;
						
					AddLike Like=g.GetComponentInChildren<AddLike> ();
					Like.Post_Id = Post_Id;
						
					AddDislike DisLike=g.GetComponentInChildren<AddDislike>();
					DisLike.Post_Id=Post_Id;
						
					RetrieveTags Tags=g.GetComponentInChildren<RetrieveTags>();
					Tags.Post_Id=Post_Id;
						
					PostIdSender Sender=g.GetComponentInChildren<PostIdSender>();
					Sender.Post_Id=Post_Id;

					GameObject item = GameObject.Find (g.name);
					item.transform.localPosition = new Vector3 (0, 300, 0);
					item.transform.localScale = new Vector3 (1, 1, 1);
						
					scrollview.ResetPosition ();

				});

			});
		});

	}
}
