using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;

public class kao_q_loca : MonoBehaviour {
	public UIScrollView scrollview;
	public string city;
	public string PostType;
	public string geo_x;
	public string geo_y;
	int count = 0;
	int i = 0;
	List<ParseObject> post;
	private List<string> label_list;
	private int limit = 5;

	void Start () {
	

		scrollview = GameObject.Find("list View").GetComponent<UIScrollView>();
	}
	

	void OnClick()
	{	
		string big = "big_" + PostType;
		Debug.Log (big);

		xml_test geo = GameObject.Find("city_now").GetComponent<xml_test>();
		geo_x=geo.geo_x;
		geo_y=geo.geo_y;
		//通过标签名称找到多有对象，前提是给预设起一个tag，这里我叫它player
		GameObject []items =  GameObject.FindGameObjectsWithTag("Player");
		//当预设数量大于 0时
		
		//删除列表的item
		for (var i = 0; i < items.Length; i++) {
			
			Destroy(items[i]);
			
		}
		//刷新UI
		scrollview.ResetPosition ();
		
		
		Loom.RunAsync (() => {
			ArrayList label_place = new ArrayList();
			ArrayList label_time = new ArrayList();
			ArrayList label_list = new ArrayList();
			ArrayList post_Id = new ArrayList ();
			ArrayList photo_e = new ArrayList();
			ArrayList user = new ArrayList();
			ArrayList posttype = new ArrayList();

			double result = Convert.ToDouble(geo_x);
			double result2 = Convert.ToDouble(geo_y);
			var point = new ParseGeoPoint(result2, result);

			var query = ParseObject.GetQuery ("POST").WhereEqualTo("foo",PostType).WhereEqualTo("Location",city).WhereWithinDistance("Post_Geo", point, ParseGeoDistance.FromKilometers(2.5)).OrderByDescending ("createdAt");
			
			//query = query.Limit(limit);                   
			var queryTask = query.FindAsync();
			
			
			IEnumerable<ParseObject> post= queryTask.Result;
			foreach (var obj in post) {

				string id = obj.ObjectId;
				string text = obj ["postfield"].ToString ();
				string place =obj["geo_place"].ToString ();
				string post_type = obj ["foo"].ToString ();
				string usr = obj["User"].ToString();
				DateTime? updatedAt =obj.CreatedAt;
				int sum = obj.Get<int> ("Sum");
				
				Debug.Log ("資料庫TAG:" + sum); 
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
				label_place.Add(place);
				post_Id.Add (id);
				label_list.Add (text);
				user.Add(usr);
				posttype.Add(post_type);
				//Debug.Log (updatedAt);  
				Debug.Log ("資料庫傳回:" + text);  
				
			}

			String[] labelplace = (String[]) label_place.ToArray( typeof( string ) );
			String[] label_text = (String[]) label_list.ToArray( typeof( string ) );
			String[] labeltime = (String[]) label_time.ToArray( typeof( string ) );
			String[] postId = (String[])post_Id.ToArray (typeof(string));
			String[] photo = (String[]) photo_e.ToArray( typeof( string ) );
			String[] userId = (String[]) user.ToArray( typeof( string ) );
			String[] Posttype = (String[]) posttype.ToArray( typeof( string ) );


			Loom.QueueOnMainThread (() => {
				for (i=0; i < photo.Length; i++) {
					
					if (photo[i]=="1"){
						GameObject o = (GameObject)Instantiate (Resources.Load (big));
						//为每个预设设置一个独一无二的名称
						o.name = PostType+count;
						
						//将新预设放在Panel对象下面
						o.transform.parent = GameObject.Find ("list View").transform;
						
						UILabel post_text = GameObject.Find("list View/"+o.name+"/PostContent").GetComponent<UILabel>();
						
						//picture = GameObject.Find("list View/"+o.name+"/photo").GetComponent<UITexture>();
						
						test1 postid = GameObject.Find("list View/"+o.name+"/photo").GetComponent<test1>();
						postid.postid =postId[i];
						post_text.text = label_text[i];
						
						
						UILabel postplace = GameObject.Find("list View/"+o.name+"/post_place").GetComponent<UILabel>();
						postplace.text = labelplace[i];
						
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
								GameObject ex_item = GameObject.Find (PostType+ex_position);
								Debug.Log(ex_item.transform.position.x+","+ex_item.transform.position.y);
								
								item.transform.localPosition = new Vector3 (0, 0, 0);
								item.transform.localScale = new Vector3 (1, 1, 1);
								//列表添加后用于刷新listView 
								item.transform.position = ex_item.transform.position + temp;
								//item.transform.position += temp;
								Debug.Log(item.transform.position.x+","+item.transform.position.y);
								scrollview.ResetPosition ();
								count ++;
							}else{
								Vector3 temp = new Vector3(0,-1.3f,0);
								GameObject item = GameObject.Find (o.name);
								int ex_position= count-1;
								
								GameObject ex_item = GameObject.Find (PostType+ex_position);
								Debug.Log(ex_item.transform.position.x+","+ex_item.transform.position.y);
								
								item.transform.localPosition = new Vector3 (0,0, 0);
								item.transform.localScale = new Vector3 (1, 1, 1);
								//列表添加后用于刷新listView 
								item.transform.position = ex_item.transform.position + temp;
								Debug.Log(item.transform.position.x+","+item.transform.position.y);
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
						
						GameObject o = (GameObject)Instantiate (Resources.Load (PostType));
						//为每个预设设置一个独一无二的名称
						o.name = PostType+count;
						
						//将新预设放在Panel对象下面
						o.transform.parent = GameObject.Find ("list View").transform;
						
						UILabel post_text = GameObject.Find("list View/"+o.name+"/PostContent").GetComponent<UILabel>();
						UILabel postplace = GameObject.Find("list View/"+o.name+"/post_place").GetComponent<UILabel>();
						postplace.text = labelplace[i];
						
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
								GameObject ex_item = GameObject.Find (PostType+ex_position);
								Debug.Log(ex_item.transform.position.x+","+ex_item.transform.position.y);
								item.transform.localPosition = new Vector3 (0,0, 0);
								item.transform.localScale = new Vector3 (1, 1, 1);
								//列表添加后用于刷新listView 
								item.transform.position = ex_item.transform.position + temp;
								
								scrollview.ResetPosition ();
								Debug.Log(item.transform.position.x+","+item.transform.position.y);
								count ++;
							}else{
								Vector3 temp = new Vector3(0,-1.3f,0);
								GameObject item = GameObject.Find (o.name);
								
								int ex_position= count-1;
								//o.name="Q_list"+ex_position;
								GameObject ex_item = GameObject.Find (PostType+ex_position);
								Debug.Log(ex_item.transform.position.x+","+ex_item.transform.position.y);
								
								item.transform.localPosition = new Vector3 (0, 0, 0);
								item.transform.localScale = new Vector3 (1, 1, 1);
								//列表添加后用于刷新listView 
								item.transform.position = ex_item.transform.position + temp;
								
								scrollview.ResetPosition ();
								Debug.Log(item.transform.position.x+","+item.transform.position.y);
								count ++;
							}
						}else if(i==0){
							Vector3 temp = new Vector3(0,0,0);
							GameObject item = GameObject.Find (o.name);
							item.transform.localPosition = new Vector3 (0, 0, 0);
							item.transform.localScale = new Vector3 (1, 1, 1);
							//列表添加后用于刷新listView 
							item.transform.position += temp;
							Debug.Log(item.transform.position.x+","+item.transform.position.y);
							scrollview.ResetPosition ();
							count ++;
						}
					}
				}
			});
			//
			
			
		});
		
		
		
		
	}
}
