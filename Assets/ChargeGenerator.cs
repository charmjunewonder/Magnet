using UnityEngine;
using System.Collections;

public class ChargeGenerator : MonoBehaviour {
	public GameObject charge;
	public ObjectPool objectPool;
	// Use this for initialization
	void Start () {
		StartCoroutine(createSpecialItem());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator createSpecialItem(){
		while(true){
			GameObject chargeClone = objectPool.GetObjectFromPool();
			chargeClone.transform.position = getRandomPosition();
			chargeClone.SetActive(true);
			chargeClone.GetComponent<ChargeController>().quantityOfCharge = Random.Range (-8, 8);

			yield return new WaitForSeconds(2);
		}
	}

	Vector3 getRandomPosition(){
		Vector3 pos;
		GameObject[] magnets = GameObject.FindGameObjectsWithTag("Magnet");
		GameObject[] charges = GameObject.FindGameObjectsWithTag("Charge");
		for(int i = 0; i < 10; i++){
			bool isGood = true;
			pos = new Vector3 ((float)Random.Range (-8, 8), 0.5f, (float)Random.Range (-5, 5));

			for(int j = 0; j < magnets.Length; j++){
				if(Vector3.Distance(pos, magnets[j].transform.position) < 3){
					// Debug.Log("item" + Distance2D(pos, specialItems[j].transform.position));
					isGood &= false;
				}
			}

			for(int j = 0; j < charges.Length; j++){
				if(charges[j].activeSelf == false) continue;
				if(Vector3.Distance(pos, charges[j].transform.position) < 3){
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
			return new Vector3(0, 0.5f, 5);
		case 2:
			return new Vector3(0, 0.5f, -5);
		case 3:
			return new Vector3(8, 0.5f, 0);
		case 4:
			return new Vector3(-8, 0.5f, 0);
		}
		return new Vector3(0, 0.5f, 8);
	}


}
