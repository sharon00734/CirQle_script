using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;

public class runtime : MonoBehaviour {
	public UIScrollView scrollview;
	int count = 0;
	int i = 0;
	string[] labeltext = new string[100];
	int[] label_num = new int[10];

	//public string post_text = null;
	//int post_num=0;
	// Use this for initialization

	void Start (){

		scrollview = GameObject.Find ("list View").GetComponent<UIScrollView> ();

		Loom.RunAsync (() => {
			var query = ParseObject.GetQuery ("POST").OrderByDescending ("createdAt").Limit (15);
			query.FindAsync ().ContinueWith (t =>
			{                          

				IEnumerable<ParseObject> results = t.Result;

				foreach (var obj in results) {
					labeltext [i] = obj ["postfield"].ToString ();
					Debug.Log ("資料庫傳回:" + labeltext [i]);  
					
					i++;
					
				}
				Debug.Log (labeltext [0]);  
				Debug.Log (labeltext [1]);  
				Debug.Log (labeltext [2]);  
				Debug.Log (labeltext [3]);  
				Debug.Log (labeltext [4]);  
				Debug.Log (labeltext [5]);  
				Debug.Log (labeltext [6]);  
				Loom.QueueOnMainThread (() => {
					for (i=0; i < 15; i++) {
						
						GameObject o = (GameObject)Instantiate (Resources.Load ("Q_list"));
						//为每个预设设置一个独一无二的名称
						o.name = "Q_list" + count;
						//将新预设放在Panel对象下面
						o.transform.parent = GameObject.Find ("list View").transform;
						
						//得到文字对象
						UILabel label = o.GetComponentInChildren<UILabel> ();
						//修改文字内容
						label.text = labeltext [i];
						Debug.Log (labeltext [i]);
						Debug.Log (label.text);
						
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
		
				Debug.Log ("hi" + i);	
			
		
			});
		});
	}



	
	
	
	
	
}
//





// Update is called once per frame


