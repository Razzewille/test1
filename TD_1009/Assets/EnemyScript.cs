using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.tag = "Enemy";
		gameObject.renderer.material.color = new Color(0, 1, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(-0.1f, 0.0f, 0.0f));
	}
}
