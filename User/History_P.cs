using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

public class History_P : MonoBehaviour {
	int count=0;
	public string city_ch;
	// Use this for initialization
	void Start(){
		Run ();
	}

	void OnClick(){
		UIScrollView scrollview = GameObject.Find("Post_view").GetComponent<UIScrollView>();
		GameObject [] items = GameObject.FindGameObjectsWithTag ("Player");

		for (var i = 0; i < items.Length; i++) {
			
			Destroy (items [i]);
			
		}
		//刷新UI
		scrollview.ResetPosition ();
		Run ();

	}

	void Run(){
		UIScrollView scrollview = GameObject.Find("Post_view").GetComponent<UIScrollView>();
		Loom.RunAsync (() => {			
			
			ArrayList post_content = new ArrayList ();
			ArrayList post_type = new ArrayList ();
			ArrayList postID = new ArrayList();
			ArrayList user = new ArrayList();
			var query = ParseObject.GetQuery ("POST").WhereEqualTo ("User", ParseUser.CurrentUser.Username).OrderByDescending("creatAt").Limit(5);
			
			query.FindAsync ().ContinueWith (t => 
			                                 {
				IEnumerable<ParseObject> result = t.Result;
				
				foreach (var ob in result) {
					string id= ob.ObjectId;
					string post_text = ob ["postfield"].ToString ();
					string type = ob ["foo"].ToString ();
					string usr = ob["User"].ToString();
					post_content.Add (post_text);
					post_type.Add (type);
					postID.Add (id);
					user.Add(usr);
				}
				String[] postcontent = (String[])post_content.ToArray (typeof(string));
				String[] posttype = (String[])post_type.ToArray (typeof(string));
				String[] postId = (String[]) postID.ToArray( typeof( string ) );
				String[] userId = (String[]) user.ToArray( typeof( string ) );
				Loom.QueueOnMainThread (() => {
					
					for (int i=0; i < posttype.Length; i++) {
						
						if (posttype [i] == "q") {
							GameObject o = (GameObject)Instantiate (Resources.Load ("q"));
							//为每个预设设置一个独一无二的名称
							o.name = "q" + count;
							//将新预设放在Panel对象下面
							o.transform.parent = GameObject.Find ("Post_view").transform;
							GameObject res = o.transform.FindChild("response").gameObject;
							res.SetActive(false);
							//得到文字对象
							UILabel postplace = GameObject.Find("Post_view/"+o.name+"/post_place").GetComponent<UILabel>();
							postplace.text = city_ch;
							UILabel label = o.GetComponentInChildren<UILabel> ();
							//修改文字内容
							label.text = postcontent [i];
							
							AddLike Like=o.GetComponentInChildren<AddLike> ();
							Like.Post_Id = postId[i];
							
							AddDislike DisLike=o.GetComponentInChildren<AddDislike>();
							DisLike.Post_Id=postId[i];
							
							RetrieveTags Tags=o.GetComponentInChildren<RetrieveTags>();
							Tags.Post_Id=postId[i];

							PostIdSender Sender=o.GetComponentInChildren<PostIdSender>();
							Sender.Post_Id=postId[i];
							
							//ParameterSender PSender=o.GetComponentInChildren<ParameterSender>();
							//PSender.Post_Id=postId[i];

							GetPoster poster=o.GetComponentInChildren<GetPoster>();
							poster.UserAccount = userId[i];

							Getuserphoto userphoto=o.GetComponentInChildren<Getuserphoto>();
							userphoto.UserAccount = userId[i];
							
							Vector3 temp = new Vector3 (0, -0.46f * count, 0);
							GameObject item = GameObject.Find (o.name);
							item.transform.localPosition = new Vector3 (0, 300, 0);
							item.transform.localScale = new Vector3 (0.925f, 0.925f, 1);
							//列表添加后用于刷新listView 
							item.transform.position += temp;
							
							scrollview.ResetPosition ();
							count ++;
						} else if (posttype [i] == "angel") {
							GameObject o = (GameObject)Instantiate (Resources.Load ("angel"));
							//为每个预设设置一个独一无二的名称
							o.name = "angel" + count;
							//将新预设放在Panel对象下面
							o.transform.parent = GameObject.Find ("Post_view").transform;
							
							//得到文字对象
							UILabel post_text = GameObject.Find("Post_view/"+o.name+"/PostContent").GetComponent<UILabel>();
							
							//修改文字内容
							post_text.text = postcontent [i];
							GameObject res = o.transform.FindChild("response").gameObject;
							res.SetActive(false);
							UILabel postplace = GameObject.Find("Post_view/"+o.name+"/post_place").GetComponent<UILabel>();
							postplace.text = city_ch;
							AddLike Like=o.GetComponentInChildren<AddLike> ();
							Like.Post_Id = postId[i];
							
							AddDislike DisLike=o.GetComponentInChildren<AddDislike>();
							DisLike.Post_Id=postId[i];
							
							RetrieveTags Tags=o.GetComponentInChildren<RetrieveTags>();
							Tags.Post_Id=postId[i];
							
							PostIdSender Sender=o.GetComponentInChildren<PostIdSender>();
							Sender.Post_Id=postId[i];
							
							//ParameterSender PSender=o.GetComponentInChildren<ParameterSender>();
							//PSender.Post_Id=postId[i];

							Getuserphoto userphoto=o.GetComponentInChildren<Getuserphoto>();
							userphoto.UserAccount = userId[i];

							GetPoster poster=o.GetComponentInChildren<GetPoster>();
							poster.UserAccount = userId[i];
									
							Getuserphoto userphoto1 =o.GetComponentInChildren<Getuserphoto>();
							userphoto1.UserAccount = userId[i];

							Vector3 temp = new Vector3 (0, -0.46f * count, 0);
							GameObject item = GameObject.Find (o.name);
							item.transform.localPosition = new Vector3 (0, 300, 0);
							item.transform.localScale = new Vector3 (0.925f, 0.925f, 1);
							//列表添加后用于刷新listView 
							item.transform.position += temp;
							
							scrollview.ResetPosition ();
							count ++;
						} else if (posttype [i] == "devil") {
							GameObject o = (GameObject)Instantiate (Resources.Load ("devil"));
							//为每个预设设置一个独一无二的名称
							o.name = "devil" + count;
							//将新预设放在Panel对象下面
							o.transform.parent = GameObject.Find ("Post_view").transform;
							
							//得到文字对象
							UILabel label = o.GetComponentInChildren<UILabel> ();
							//修改文字内容
							label.text = postcontent [i];
							GameObject res = o.transform.FindChild("response").gameObject;
							res.SetActive(false);
							UILabel postplace = GameObject.Find("Post_view/"+o.name+"/post_place").GetComponent<UILabel>();
							postplace.text = city_ch;
							
							AddLike Like=o.GetComponentInChildren<AddLike> ();
							Like.Post_Id = postId[i];
							
							AddDislike DisLike=o.GetComponentInChildren<AddDislike>();
							DisLike.Post_Id=postId[i];
							
							RetrieveTags Tags=o.GetComponentInChildren<RetrieveTags>();
							Tags.Post_Id=postId[i];
							
							PostIdSender Sender=o.GetComponentInChildren<PostIdSender>();
							Sender.Post_Id=postId[i];
							
							//ParameterSender PSender=o.GetComponentInChildren<ParameterSender>();
							//PSender.Post_Id=postId[i];
							
							GetPoster poster=o.GetComponentInChildren<GetPoster>();
							poster.UserAccount = userId[i];
							
							Getuserphoto userphoto=o.GetComponentInChildren<Getuserphoto>();
							userphoto.UserAccount = userId[i];

							Vector3 temp = new Vector3 (0, -0.46f * count, 0);
							GameObject item = GameObject.Find (o.name);
							item.transform.localPosition = new Vector3 (0, 300, 0);
							item.transform.localScale = new Vector3 (0.925f, 0.925f, 1);
							//列表添加后用于刷新listView 
							item.transform.position += temp;
							
							scrollview.ResetPosition ();
							count ++;
							
						} else {
							Debug.Log ("error");
						}
						
					}	
					
				});
			});
		});
	}

}
