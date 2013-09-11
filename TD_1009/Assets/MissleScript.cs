using UnityEngine;
using System.Collections;

public class MissleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.renderer.material.color = new Color(1, 0, 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (m_target == null)
		{
			Destroy (gameObject);
			return;
		}
		Vector3 dir = m_target.transform.position - transform.position;
		if (dir.magnitude < 0.2)
		{
			Destroy(m_target);
			Destroy(gameObject);
		}
		else
		{
			dir.Normalize();
			dir *= 0.2f;
			transform.Translate(dir);
		}
		
	}
	public GameObject m_target;
}
