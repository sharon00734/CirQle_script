using UnityEngine;
using System.Collections;

public class ChangeCity : MonoBehaviour {
	public string City;
	public string exist;
	void OnClick(){
		GameObject []items =  GameObject.FindGameObjectsWithTag("buildings");
		for (var i = 0; i < items.Length; i++) {			
			Destroy(items[i]);			
		}
		ScorePrinciple iniobj = GameObject.Find ("background_main").GetComponent<ScorePrinciple> ();
		Debug.Log (iniobj);
		iniobj.City = City;
		iniobj.Rank (City);
		/*RingValue value= GameObject.Find ("radar").GetComponent<RingValue> ();
		value.city = City;


		if (exist != null) {
			q_ca q_city = GameObject.Find ("Q_button").GetComponent<q_ca> ();
			q_city.city = City;
			q_city.exist = exist;

			kao_angel angel_city = GameObject.Find ("angel_button").GetComponent<kao_angel> ();
			angel_city.city = City;
			angel_city.exist = exist;

			kao_devil devil_city = GameObject.Find ("devil_button").GetComponent<kao_devil> ();
			devil_city.city = City;
			devil_city.exist = exist;

			allpost all_city = GameObject.Find ("all_button").GetComponent<allpost> ();
			all_city.city = City;
			all_city.exist = exist;

			kao_q_pop pop_city = GameObject.Find ("hot").GetComponent<kao_q_pop> ();
			pop_city.city = City;
			pop_city.exist = exist;
		}*/

	}
}
