using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;


public class search_tag : MonoBehaviour {
	UIInput mInput;
	private UIScrollView scrollview;
	int count = 0;
	int i = 0;
	public bool fillWithDummyData = false;
	
	
	void Start ()
	{
		mInput = GetComponent<UIInput>();
		mInput.label.maxLineCount = 1;

		Debug.Log ("1");
	}

	
	public void OnClick ()
	{
		//scrollview = GameObject.Find("search_view").GetComponent<UIScrollView>();
		
		scrollview = GameObject.Find("search_view 1").GetComponent<UIScrollView>();

		// It's a good idea to strip out all symbols as we don't want user input to alter colors, add new lines, etc
		string text = NGUIText.StripSymbols(mInput.value);
		Debug.Log("hihi"+text);

		if (!string.IsNullOrEmpty (text)) {
			GameObject [] items = GameObject.FindGameObjectsWithTag ("Player");
			//当预设数量大于 0时
			
			//删除列表的item
			for (var i = 0; i < items.Length; i++) {
				
				Destroy (items [i]);
				
			}
			//刷新UI
			scrollview.ResetPosition ();
				
			
			Debug.Log (text);

			Loom.RunAsync (() => {
				
				ArrayList label_list = new ArrayList ();
				ArrayList label_type = new ArrayList ();
				ArrayList post_Id = new ArrayList ();
				var query = ParseObject.GetQuery ("TAG").WhereContains ("TagContent", text).OrderByDescending ("createdAt");
				
				query.FindAsync ().ContinueWith (t =>
				                                 {   
					
					IEnumerable<ParseObject> post = t.Result;
					foreach (var obj in post) {
						string id = obj.ObjectId;
						string result = obj ["TagContent"].ToString ();
						//string type = obj ["post_type"].ToString ();
						//labeltext.Add(post);
						Debug.Log ("資料庫傳回:" + result);  
						post_Id.Add (id);
						label_list.Add (result);
						//label_type.Add (type);
						
					}
					for (int ii = 0; ii < label_list.Count; ii++) 
					{ 
						for (int jj = ii + 1; jj < label_list.Count; jj++) 
						{ 
							if(label_list[ii].Equals(label_list[jj])){
								Debug.Log ("del"+label_list[jj]);
								label_list.RemoveAt(jj); 
								jj--;
								//由于刚刚删除了一个，所以jj要后退一个
							}
						} 
					}
					
					String[] label_text = (String[])label_list.ToArray (typeof(string));
					String[] postId = (String[])post_Id.ToArray (typeof(string));
					//String[] labeltype = (String[])label_type.ToArray (typeof(string));
					Loom.QueueOnMainThread (() => {
						
						for (i=0; i < postId.Length; i++) {
							
							//Debug.Log ("a");
							Debug.Log ("資料庫傳回:" + label_text [i]);

							GameObject o = (GameObject)Instantiate (Resources.Load ("tag"));
								//为每个预设设置一个独一无二的名称
							o.name = "tag" + count;
								//将新预设放在Panel对象下面
							o.transform.parent = GameObject.Find ("search_view 1").transform;
								
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
								
							Vector3 temp = new Vector3 (0, -0.18f * count, 0);
							GameObject item = GameObject.Find (o.name);
							item.transform.localPosition = new Vector3 (0, 152, 0);
							item.transform.localScale = new Vector3 (1, 1, 1);
								//列表添加后用于刷新listView 
							item.transform.position += temp;
								
							scrollview.ResetPosition ();
							count ++;

							
						}	
						
					});
				});
			});
			//mInput.value = "";
			mInput.isSelected = false;
		}
	}
}
