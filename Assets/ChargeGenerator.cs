using UnityEngine;
using System.Collections;

public class ChargeGenerator : MonoBehaviour {
	GameObject charge;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator createSpecialItem(){
		while(true){
			yield return new WaitForSeconds(3f);
			GameObject chargeClone = Instantiate(charge) as GameObject;
			chargeClone.transform.position = getRandomPosition();
			chargeClone.SetActive(true);
			chargeClone.GetComponent<SpecialItemDead>().dieInSeconds();

			yield return new WaitForSeconds(10f);
		}
	}

	Vector3 getRandomPosition(){
		Vector3 pos;
		for(int i = 0; i < 10; i++){
			bool isGood = true;
			pos = new Vector3 ((float)Random.Range (-17, 17), 0.5f, (float)Random.Range (-17, 17));
			if(Distance2D(pos, spaceShip.transform.position) < 12){
				isGood &= false;
			}
			for(int j = 0; j < 4; j++){
				if(Distance2D(pos, specialItems[j].transform.position) < 10){
					// Debug.Log("item" + Distance2D(pos, specialItems[j].transform.position));
					isGood &= false;
				}
			}
			if(isGood){
				// Debug.Log("space" + Distance2D(pos, spaceShip.transform.position));
				// for(int j = 0; j < 4; j++){
				// Debug.Log("item" + Distance2D(pos, specialItems[j].transform.position));
				// }
				return pos;
			}
		}
		int ran = Random.Range(0, 4);
		switch(ran)
		{
		case 1:
			return new Vector3(0, 0.5f, 30);
		case 2:
			return new Vector3(0, 0.5f, -30);
		case 3:
			return new Vector3(30, 0.5f, 0);
		case 4:
			return new Vector3(-30, 0.5f, 0);
		}
		return new Vector3(0, 0.5f, 30);
	}


}
