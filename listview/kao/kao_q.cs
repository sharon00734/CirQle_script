using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;


public class kao_q : MonoBehaviour {
	private UIScrollView scrollview;
	public string city;

	int count = 0;
	int i = 0;
	//List<ParseObject> post;
	//private List<string> label_list;
	private int limit = 5;
	void Start () {
		
		scrollview = GameObject.Find("list View").GetComponent<UIScrollView>();
	}
	
	// Update is called once per frame
	void OnClick()
	{	
		GameObject loca=GameObject.Find("place(Clone)");
		GameObject hot=GameObject.Find("hot(Clone)");
		Destroy (loca);
		Destroy (hot);
		//通过标签名称找到多有对象，前提是给预设起一个tag，这里我叫它player
		GameObject []items =  GameObject.FindGameObjectsWithTag("Player");
		//当预设数量大于 0时
		
		//删除列表的item
		for (var i = 0; i < items.Length; i++) {
			
			Destroy(items[i]);
			
		}
		//刷新UI
		scrollview.ResetPosition ();
		


		GameObject location = (GameObject)Instantiate (Resources.Load ("place"));
		location.transform.parent = GameObject.Find ("location").transform;
		location.transform.localPosition = new Vector3(0,0,0);  
		location.transform.localScale= new Vector3(1,1,1);  
		location.AddComponent<kao_q_loca>();

		GameObject Hot = (GameObject)Instantiate (Resources.Load ("hot"));
		Hot.transform.parent = GameObject.Find ("hot").transform;
		Hot.transform.localPosition = new Vector3(0,0,0);  
		Hot.transform.localScale= new Vector3(1,1,1);  
		Hot.AddComponent<kao_q_pop>();



		Loom.RunAsync (() => {
			
			ArrayList label_list = new ArrayList();
			ArrayList label_time = new ArrayList();
			var query = ParseObject.GetQuery ("POST").WhereEqualTo("foo","q").WhereEqualTo("Location",city).WhereDoesNotExist("post_geo").OrderByDescending ("createdAt").Limit(limit);
			
			//query = query.Limit(limit);                   
			var queryTask = query.FindAsync();
			
			
			IEnumerable<ParseObject> post= queryTask.Result;
			foreach (var obj in post) {
				string text = obj ["postfield"].ToString ();
				DateTime? updatedAt =obj.CreatedAt;
				//string post = obj ["postfield"].ToString ();
				//labeltext.Add(post);
				Debug.Log (updatedAt);  
				string time=updatedAt.ToString();
				label_time.Add(time);
				Debug.Log ("資料庫傳回:" + text);  
				
				label_list.Add (text);
				
			}
			String[] label_text = (String[]) label_list.ToArray( typeof( string ) );
			String[] labeltime = (String[]) label_time.ToArray( typeof( string ) );

			Loom.QueueOnMainThread (() => {
				for (i=0; i < 5; i++) {
					
					GameObject o = (GameObject)Instantiate (Resources.Load ("Q_list"));
					//为每个预设设置一个独一无二的名称
					o.name = "Q_list"+ count;
					//将新预设放在Panel对象下面
					o.transform.parent = GameObject.Find ("list View").transform;

					//UILabel post_text = o.GetComponentInChildren<UILabel> ("PostContent");

					//UILabel post_time = o.GetComponentInChildren<UILabel> ("post_time");
					/*
					UILabel post_text = GameObject.Find("list View/"+o+"/PostContent").GetComponent<UILabel>();
						
					UILabel post_time = GameObject.Find("list View/"+o+"/post_time").GetComponent<UILabel>();

					post_text.text = label_text[i];
					post_time.text = labeltime[i];
					*/
					UILabel post_text = GameObject.Find("list View/"+o.name+"/PostContent").GetComponent<UILabel>();
					
					//UILabel post_time = GameObject.Find("list View/"+o.name+"/post_time").GetComponent<UILabel>();
					
					post_text.text = label_text[i];
					//post_time.text = labeltime[i];
					//o.FindChild("post_time").GetComponent<UILabel>().text =  labeltime[i];
					//UILabel INext = o.Find("post_time").<UILabel> ();
					//得到文字对象
					//UILabel label = o.GetComponentInChildren<UILabel> ();
					//修改文字内容
					//label.text = label_text[i];
					//Debug.Log (labeltext [i]);
					//Debug.Log (label.text);
					
					////下面这段代码是因为创建预设时 会自动修改旋转缩放的系数，
					//我不知道为什么会自动修改，所以MOMO重新为它赋值
					//有知道的朋友麻烦告诉我一下 谢谢！！！
					Vector3 temp = new Vector3(0,-0.44f*count,0);
					GameObject item = GameObject.Find (o.name);
					item.transform.localPosition = new Vector3 (0, 300, 0);
					item.transform.localScale = new Vector3 (1, 1, 1);
					//列表添加后用于刷新listView 
					item.transform.position += temp;
					
					scrollview.ResetPosition ();
					count ++;
				}	
				
			});
			//
			
			
		});
		
		
		
		
	}
}
