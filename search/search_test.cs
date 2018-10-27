using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;

public class search_test : MonoBehaviour {
	UIInput mInput;
	public UIScrollView scrollview;
	int count = 0;
	int i = 0;
	public bool fillWithDummyData = false;

	
	void Start ()
	{
		mInput = GetComponent<UIInput>();
		mInput.label.maxLineCount = 1;
		scrollview = GameObject.Find("search_view").GetComponent<UIScrollView>();

		Debug.Log("1");
	}

	
	public void OnSubmit ()
	{
		
		Debug.Log("2");
		// It's a good idea to strip out all symbols as we don't want user input to alter colors, add new lines, etc
		string text = NGUIText.StripSymbols(mInput.value);
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
				ArrayList label_time = new ArrayList();
				ArrayList label_list = new ArrayList();
				ArrayList post_Id = new ArrayList ();
				ArrayList photo_e = new ArrayList();
				ArrayList user = new ArrayList();
				ArrayList posttype = new ArrayList();
				ArrayList placein = new ArrayList();
				var query = ParseObject.GetQuery ("POST").WhereContains ("postfield", text).OrderByDescending ("createdAt");
	
				var queryTask = query.FindAsync();
				
				
				IEnumerable<ParseObject> post= queryTask.Result;
				foreach (var obj in post) {
					string id = obj.ObjectId;
					string text1 = obj ["postfield"].ToString ();
					string post_type = obj ["foo"].ToString ();
					string usr = obj["User"].ToString();
					string place = obj["Location"].ToString();
					DateTime? updatedAt =obj.CreatedAt;
					
					
					
					var imagefile = obj.Get<ParseFile> ("file");
					if (imagefile.Name.Contains("none")){
						Debug.Log ("none");
						photo_e.Add ("0");
					}else{
						Debug.Log ("have");
						photo_e.Add ("1");
					}
					
					string time=updatedAt.ToString();
					label_time.Add(time);
					post_Id.Add (id);
					label_list.Add (text1);
					user.Add(usr);
					posttype.Add(post_type);
					placein.Add(place);
					//Debug.Log (updatedAt);  
					Debug.Log ("資料庫傳回:" + text);  
				}

				String[] label_text = (String[]) label_list.ToArray( typeof( string ) );
				String[] labeltime = (String[]) label_time.ToArray( typeof( string ) );
				String[] postId = (String[])post_Id.ToArray (typeof(string));
				String[] photo = (String[]) photo_e.ToArray( typeof( string ) );
				String[] userId = (String[]) user.ToArray( typeof( string ) );
				String[] Posttype = (String[]) posttype.ToArray( typeof( string ) );
				String[] place_in = (String[]) placein.ToArray( typeof( string ) );
					Loom.QueueOnMainThread (() => {
						
						for (i=0; i < photo.Length; i++) {
							string type = Posttype[i];
							string big="big_"+type;
							Debug.Log (big);
							if (place_in[i] != null) {
								if (place_in[i] == "Kaohsiung") {
									place_in[i]= "高雄文";
								} else if (place_in[i] == "Taichung") {
									place_in[i] = "臺中文";
								} else if (place_in[i] == "Taipei") {
									place_in[i] = "臺北文";
								} else if (place_in[i] == "NewTaipei") {
									place_in[i] = "新北文";
								} else if (place_in[i] == "Tainan") {
									place_in[i] = "臺南文";
								} else if (place_in[i] == "Taoyuan") {
									place_in[i] = "桃園文";
									}
							}
							
							if (photo[i]=="1"){
								GameObject o = (GameObject)Instantiate (Resources.Load (big));
								//为每个预设设置一个独一无二的名称
								o.name = "all"+count;
								
								//将新预设放在Panel对象下面
								o.transform.parent = GameObject.Find ("search_view").transform;
									
								UILabel post_text = GameObject.Find("search_view/"+o.name+"/PostContent").GetComponent<UILabel>();
								
								//picture = GameObject.Find("list View/"+o.name+"/photo").GetComponent<UITexture>();
								
								test1 postid = GameObject.Find("search_view/"+o.name+"/photo").GetComponent<test1>();
								postid.postid =postId[i];
								post_text.text = label_text[i];
								
								
								UILabel postplace = GameObject.Find("search_view/"+o.name+"/post_place").GetComponent<UILabel>();
								postplace.text = place_in[i];
								
								AddLike Like=o.GetComponentInChildren<AddLike> ();
								Like.Post_Id = postId[i];
								
								AddDislike DisLike=o.GetComponentInChildren<AddDislike>();
								DisLike.Post_Id=postId[i];
								
								RetrieveTags Tags=o.GetComponentInChildren<RetrieveTags>();
								Tags.Post_Id=postId[i];
								
								PostIdSender Sender=o.GetComponentInChildren<PostIdSender>();
								Sender.Post_Id=postId[i];
								
								ParameterSender PSender=o.GetComponentInChildren<ParameterSender>();
								PSender.Post_Id=postId[i];
								
								GetPoster poster=o.GetComponentInChildren<GetPoster>();
								poster.UserAccount = userId[i];
								
								Getuserphoto userphoto=o.GetComponentInChildren<Getuserphoto>();
								userphoto.UserAccount = userId[i];
								if(i>0){
									if(photo[i-1]=="0"){
										Vector3 temp = new Vector3(0,-0.6f,0);
										GameObject item = GameObject.Find (o.name);
										int ex_position= count-1;
										//o.name="Q_list"+ex_position;
										GameObject ex_item = GameObject.Find ("all"+ex_position);
										Debug.Log(ex_item.transform.position.x+","+ex_item.transform.position.y);
										
										item.transform.localPosition = new Vector3 (0, 0, 0);
										item.transform.localScale = new Vector3 (1, 1, 1);
										//列表添加后用于刷新listView 
										item.transform.position = ex_item.transform.position + temp;
										//item.transform.position += temp;
										//	Debug.Log(item.transform.position.x+","+item.transform.position.y);
										scrollview.ResetPosition ();
										count ++;
									}else{
										Vector3 temp = new Vector3(0,-1.3f,0);
										GameObject item = GameObject.Find (o.name);
										int ex_position= count-1;
										
										GameObject ex_item = GameObject.Find ("all"+ex_position);
										Debug.Log(ex_item.transform.position.x+","+ex_item.transform.position.y);
										
										item.transform.localPosition = new Vector3 (0,0, 0);
										item.transform.localScale = new Vector3 (1, 1, 1);
										//列表添加后用于刷新listView 
										item.transform.position = ex_item.transform.position + temp;
										//Debug.Log(item.transform.position.x+","+item.transform.position.y);
										scrollview.ResetPosition ();
										count ++;
									}
								}else if(i==0){
									Vector3 temp = new Vector3(0,0,0);
									GameObject item = GameObject.Find (o.name);
									
									item.transform.localPosition = new Vector3 (0, 0, 0);
									item.transform.localScale = new Vector3 (1, 1, 1);
									//列表添加后用于刷新listView 
									//item.transform.position += temp;
									
									scrollview.ResetPosition ();
									count ++;
								}
								
								
							}else{
								
								GameObject o = (GameObject)Instantiate (Resources.Load (type));
								//为每个预设设置一个独一无二的名称
								o.name = "all"+count;
								
								//将新预设放在Panel对象下面
								o.transform.parent = GameObject.Find ("search_view").transform;
								
								UILabel post_text = GameObject.Find("search_view/"+o.name+"/PostContent").GetComponent<UILabel>();
								UILabel postplace = GameObject.Find("search_view/"+o.name+"/post_place").GetComponent<UILabel>();
								postplace.text = place_in[i];
								
								//picture = GameObject.Find("list View/"+o.name+"/photo").GetComponent<UITexture>();
								
								//test1 postid = GameObject.Find("list View/"+o.name+"/photo").GetComponent<test1>();
								//postid.postid =postId[i];
								post_text.text = label_text[i];
								
								AddLike Like=o.GetComponentInChildren<AddLike> ();
								Like.Post_Id = postId[i];
								
								AddDislike DisLike=o.GetComponentInChildren<AddDislike>();
								DisLike.Post_Id=postId[i];
								
								RetrieveTags Tags=o.GetComponentInChildren<RetrieveTags>();
								Tags.Post_Id=postId[i];
								
								PostIdSender Sender=o.GetComponentInChildren<PostIdSender>();
								Sender.Post_Id=postId[i];
								
								ParameterSender PSender=o.GetComponentInChildren<ParameterSender>();
								PSender.Post_Id=postId[i];
								
								GetPoster poster=o.GetComponentInChildren<GetPoster>();
								poster.UserAccount = userId[i];
								
								Getuserphoto userphoto=o.GetComponentInChildren<Getuserphoto>();
								userphoto.UserAccount = userId[i];
								if(i>0){
									if(photo[i-1]=="0"){
										Vector3 temp = new Vector3(0,-0.57f,0);
										GameObject item = GameObject.Find (o.name);
										
										
										int ex_position= count-1;
										//o.name="Q_list"+ex_position;
										GameObject ex_item = GameObject.Find ("all"+ex_position);
										Debug.Log(ex_item.transform.position.x+","+ex_item.transform.position.y);
										item.transform.localPosition = new Vector3 (0,0, 0);
										item.transform.localScale = new Vector3 (1, 1, 1);
										//列表添加后用于刷新listView 
										item.transform.position = ex_item.transform.position + temp;
										
										scrollview.ResetPosition ();
										//Debug.Log(item.transform.position.x+","+item.transform.position.y);
										count ++;
									}else{
										Vector3 temp = new Vector3(0,-1.3f,0);
										GameObject item = GameObject.Find (o.name);
										
										int ex_position= count-1;
										//o.name="Q_list"+ex_position;
										GameObject ex_item = GameObject.Find ("all"+ex_position);
										Debug.Log(ex_item.transform.position.x+","+ex_item.transform.position.y);
										
										item.transform.localPosition = new Vector3 (0, 0, 0);
										item.transform.localScale = new Vector3 (1, 1, 1);
										//列表添加后用于刷新listView 
										item.transform.position = ex_item.transform.position + temp;
										
										scrollview.ResetPosition ();
										//Debug.Log(item.transform.position.x+","+item.transform.position.y);
										count ++;
									}
								}else if(i==0){
									Vector3 temp = new Vector3(0,0,0);
									GameObject item = GameObject.Find (o.name);
									item.transform.localPosition = new Vector3 (0, 0, 0);
									item.transform.localScale = new Vector3 (1, 1, 1);
									//列表添加后用于刷新listView 
									item.transform.position += temp;
									//Debug.Log(item.transform.position.x+","+item.transform.position.y);
									scrollview.ResetPosition ();
									count ++;
								}
							}
						}
					});
				});
			//});
			//mInput.value = "";
			mInput.isSelected = false;
		}
	}
}
