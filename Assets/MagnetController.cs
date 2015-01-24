using UnityEngine;
using System.Collections;

public class MagnetController : MonoBehaviour {
	public GameObject sign;

	public int quantityOfCharge = 1;
	public int typeOfCharge = 1; //1 is positive; -1 is nagetive

	private GameObject nearestMagnet = null;

	// Use this for initialization
	void Start () {
	 	LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(Color.yellow, Color.red);
        lineRenderer.SetWidth(0.1F, 0.1F);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		DrawLineToNearestMagnet();
		AddInstanceForce();
	}

	GameObject GetNearestMagnet(){
		GameObject[] magnets = GameObject.FindGameObjectsWithTag("Magnet");
		GameObject nearest = null;
		float min = float.MaxValue;
		for(int i = 0; i < magnets.Length; i++){
			float distance = Vector3.Distance(magnets[i].transform.position, transform.position);
			if(distance < min && !Mathf.Approximately(distance, 0)){
				nearest = magnets[i];
				min	= distance;
			}
		}
		return nearest;
	}

	void DrawLineToNearestMagnet(){
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		nearestMagnet = GetNearestMagnet();
		lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, nearestMagnet.transform.position);
	}

	void AddInstanceForce(){
		Vector3 direction = nearestMagnet.transform.position - transform.position;
		direction = direction * typeOfCharge * nearestMagnet.GetComponent<MagnetController>().typeOfCharge;
		if(direction.magnitude < 1f) return;
		float distance = Vector3.Distance(nearestMagnet.transform.position, transform.position);

		float force = nearestMagnet.GetComponent<MagnetController>().quantityOfCharge * quantityOfCharge / (distance * distance);
		Debug.Log(force);
		rigidbody.AddForce(direction.normalized * force * 5, ForceMode.Force);
	}
}
