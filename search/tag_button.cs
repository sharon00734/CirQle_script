using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;

public class tag_button : MonoBehaviour {
	public UILabel lbl;
	private UIScrollView scrollview;
	int count = 0;
	int i = 0;

	void OnClick () {
		lbl = GetComponentInChildren<UILabel>();
		string tag = lbl.text;
		Debug.Log (tag);
		scrollview = GameObject.Find("search_view 1").GetComponent<UIScrollView>();

		GameObject [] items = GameObject.FindGameObjectsWithTag ("Player");
		for (var i = 0; i < items.Length; i++) {
			
			Destroy (items [i]);
			
		}
	
		scrollview.ResetPosition ();

		Loom.RunAsync (() => {

		ArrayList post_Id = new ArrayList ();
			
			
			var query = ParseObject.GetQuery ("POST").WhereEqualTo ("Tag1", tag);
			query.FindAsync ().ContinueWith (t =>
			{
			IEnumerable<ParseObject> results = t.Result;
			foreach (var objs in results) {
					
				string id = objs.ObjectId;
				Debug.Log ("資料庫TAG:" + id);  
				post_Id.Add (id);
					
			}
				
			var queryT = ParseObject.GetQuery ("POST").WhereEqualTo ("Tag2", tag);
				
			queryT.FindAsync ().ContinueWith (t2 => 
			{
				IEnumerable<ParseObject> result2 = t2.Result;
					
				foreach (var obj in result2) {
					string id2 = obj.ObjectId;

				

				
					post_Id.Add (id2);
						
				}
					
					var queryY = ParseObject.GetQuery ("POST").WhereEqualTo ("Tag3", tag);
					
					queryT.FindAsync ().ContinueWith (t4 => 
					                                  {
						IEnumerable<ParseObject> result4 = t4.Result;
						
						foreach (var ob in result2) {
							string id3 = ob.ObjectId;
							

							
							
							post_Id.Add (id3);
							
						}

						for (int ii = 0; ii < post_Id.Count; ii++) 
					{ 
							for (int jj = ii + 1; jj < post_Id.Count; jj++) 
						{ 
								if(post_Id[ii].Equals(post_Id[jj])){
									Debug.Log ("del"+post_Id[jj]);
									post_Id.RemoveAt(jj); 
									jj--;
									//由于刚刚删除了一个，所以jj要后退一个
							}
						} 
					}


						ArrayList label_time = new ArrayList();
						ArrayList label_list = new ArrayList();
						ArrayList post_id = new ArrayList ();
						ArrayList photo_e = new ArrayList();
						ArrayList user = new ArrayList();
						ArrayList posttype = new ArrayList();
						ArrayList placein = new ArrayList();
					var arraypostid = (String[])post_Id.ToArray (typeof(string));
							
					var queryC = ParseObject.GetQuery ("POST").WhereContainedIn ("objectId", arraypostid);
					
						var queryTask = queryC.FindAsync();
						
						
						IEnumerable<ParseObject> post= queryTask.Result;
						foreach (var obj1 in post) {
							string id = obj1.ObjectId;
							string text1 = obj1["postfield"].ToString ();
							string post_type = obj1["foo"].ToString ();
							string usr = obj1["User"].ToString();
							string place = obj1["Location"].ToString();
							DateTime? updatedAt =obj1.CreatedAt;
							
							
							
							var imagefile = obj1.Get<ParseFile> ("file");
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
									o.transform.parent = GameObject.Find ("search_view 1").transform;
									
									UILabel post_text = GameObject.Find("search_view 1/"+o.name+"/PostContent").GetComponent<UILabel>();
									
									//picture = GameObject.Find("list View/"+o.name+"/photo").GetComponent<UITexture>();
									
									test1 postid = GameObject.Find("search_view 1/"+o.name+"/photo").GetComponent<test1>();
									postid.postid =postId[i];
									post_text.text = label_text[i];
									
									
									UILabel postplace = GameObject.Find("search_view 1/"+o.name+"/post_place").GetComponent<UILabel>();
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
									o.transform.parent = GameObject.Find ("search_view 1").transform;
									
									UILabel post_text = GameObject.Find("search_view 1/"+o.name+"/PostContent").GetComponent<UILabel>();
									UILabel postplace = GameObject.Find("search_view 1/"+o.name+"/post_place").GetComponent<UILabel>();
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
				});
				});

			});
		//});
	}
}
