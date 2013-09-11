using UnityEngine;
using System.Collections;

public class TowerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		m_aTargeted = new ArrayList();
		m_luckyBastard = (Random.value < 0.15f);
		if (!m_luckyBastard)
		{
			gameObject.renderer.material.color = new Color(0, 1, 0, 1);
			m_distance = 30.0f;
		}
		else
		{
			gameObject.renderer.material.color = new Color(1, 0.7f, 0, 1);
			m_distance = 70.0f;
		}
		m_restTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float currentTime = Time.time;
		float respawn = m_luckyBastard ? 0.1f : 1.0f;
		if (currentTime < (m_restTime + respawn))
			return;
		GameObject [] aAllEnemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject thisObject in aAllEnemies)
		  	{
				if (m_aTargeted.Contains(thisObject))
					continue;
			   	if ((transform.position - thisObject.transform.position).magnitude < m_distance)
				{
					GameObject missle = (GameObject) Instantiate(m_missleFactory, transform.position, Quaternion.identity);
					MissleScript mscript = (MissleScript) missle.GetComponent("MissleScript");
					mscript.m_target = thisObject;
					m_aTargeted.Add (thisObject);
					m_restTime = Time.time;
					return;
				}
			}
	}
	bool m_luckyBastard;
	float m_distance;
	float m_restTime;
	ArrayList m_aTargeted;		
	public GameObject m_missleFactory;
}
