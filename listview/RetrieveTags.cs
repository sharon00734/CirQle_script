using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;

public class RetrieveTags : MonoBehaviour {
	public string Post_Id;
	//List<ParseObject> results;
	//private List<string> TagContent;
	//List<ParseObject> result2;
	//private List<string> Tag_Id;
	public UILabel[] Label=new UILabel[3];

	//int i = 0;
	// Use this for initialization
	void Start () {
		ArrayList Tag_Id = new ArrayList();

		var query = ParseObject.GetQuery ("POST");
		
		query.GetAsync (Post_Id).ContinueWith (t =>
		{
			Loom.QueueOnMainThread(()=>{
				if (t.IsCanceled || t.IsFaulted) {
					
					Debug.Log ("NONON.");
					
				} else {
					ParseObject results = t.Result;
					//ArrayList TagContent = new ArrayList();
					foreach (var objs in results) {
						Label[0].text=results.Get<string>("Tag1");
						Label[1].text=results.Get<string>("Tag2");
						Label[2].text=results.Get<string>("Tag3");

						//Debug.Log ("資料庫TAG:" +id );  
						
						/*var queryT=ParseObject.GetQuery("TAG").WhereEqualTo("objectId",id);
						var queryTask=queryT.FindAsync().ContinueWith(t2 =>{
							
							IEnumerable<ParseObject> result2 = t2.Result;
							Loom.QueueOnMainThread(()=>{
								foreach(var obj in result2){
									string content=obj["TagContent"].ToString();
									Debug.Log("TagContent:"+ content);
									Label[i].text=content;
									TagContent.Add (content);
									i++;
								}
							});
						});
						
						Tag_Id.Add (id);*/
						
					}
				}
			});
		});		
	}
}
