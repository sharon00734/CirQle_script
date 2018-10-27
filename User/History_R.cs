using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

public class History_R : MonoBehaviour {
	int i=0;
	int count=0;
	// Use this for initialization
	void OnClick(){
		UIScrollView scrollview = GameObject.Find("Response_view").GetComponent<UIScrollView>();
		GameObject [] items = GameObject.FindGameObjectsWithTag ("Player");
		
		for (var i = 0; i < items.Length; i++) {
			
			Destroy (items [i]);
			
		}
		Loom.RunAsync (() => {
			
			ArrayList label_content = new ArrayList ();
			//ArrayList label_type = new ArrayList ();
			ArrayList post_Id = new ArrayList ();
			var query = ParseObject.GetQuery ("RESPONSE").WhereEqualTo("User",ParseUser.CurrentUser.Username).OrderByDescending ("createdAt").Limit(5);
			
			query.FindAsync ().ContinueWith (t =>
			                                 {   
				
				IEnumerable<ParseObject> result = t.Result;
				foreach (var obj in result) {
					string id = obj.ObjectId;
					string content = obj ["R_Content"].ToString();
					//string type = obj ["post_type"].ToString ();
					//labeltext.Add(post);
					Debug.Log ("資料庫傳回:" + result);  
					post_Id.Add (id);
					label_content.Add (content);
					//label_type.Add (type);
					
				}
				
				String[] label_text = (String[])label_content.ToArray (typeof(string));
				String[] postId = (String[])post_Id.ToArray (typeof(string));
				//String[] labeltype = (String[])label_type.ToArray (typeof(string));
				Loom.QueueOnMainThread (() => {
					
					for (i=0; i < 5; i++) {
						
						//Debug.Log ("a");
						Debug.Log ("資料庫傳回:" + label_text [i]);
						
						GameObject o = (GameObject)Instantiate (Resources.Load ("Response_Profile"));
						//为每个预设设置一个独一无二的名称
						o.name = "Response" + count;

						//将新预设放在Panel对象下面
						o.transform.parent = GameObject.Find ("Response_view").transform;
						
						//得到文字对象
						UILabel label = o.GetComponentInChildren<UILabel> ();
						//修改文字内容
						label.text = label_text [i];
						
						/*	AddLike Like=o.GetComponentInChildren<AddLike> ();
							Like.Post_Id = postId[i];
							
							AddDislike DisLike=o.GetComponentInChildren<AddDislike>();
							DisLike.Post_Id=postId[i];
							
							RetrieveTags Tags=o.GetComponentInChildren<RetrieveTags>();
							Tags.Post_Id=postId[i];
							
							PostIdSender Sender=o.GetComponentInChildren<PostIdSender>();
							Sender.Post_Id=postId[i];
							
							ParameterSender PSender=o.GetComponentInChildren<ParameterSender>();
							PSender.Post_Id=postId[i];*/
						
						Vector3 temp = new Vector3 (0, -0.27f * count, 0);
						GameObject item = GameObject.Find (o.name);
						item.transform.localPosition = new Vector3 (0, 152, 0);
						item.transform.localScale = new Vector3 (0.93f, 0.93f, 1);
						//列表添加后用于刷新listView 
						item.transform.position += temp;
						
						scrollview.ResetPosition ();
						count ++;
						
						
					}	
					
				});
			});
		});
	
	}
}
