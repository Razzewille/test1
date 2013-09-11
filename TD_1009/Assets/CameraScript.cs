using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		m_startTime = -1;
		Random.seed = 1;
		m_frequency = 1;
		m_created = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_startTime != (int) ((float)(m_frequency)*Time.time))
		{
			Instantiate(m_enemyPrefab, new Vector3(40, 2, (int)(60.0f*Random.value) - 50), Quaternion.identity);
			m_startTime = (int) ((float)(m_frequency)*Time.time);
		}
		if (Input.GetMouseButtonDown(0))
		{
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(mouseRay, out hit))
			{
				if (hit.transform.gameObject.Equals(GameObject.Find("Terrain")))
				{
					Instantiate(m_towerPrefab, new Vector3(hit.point.x, 2, hit.point.z), Quaternion.identity);
					m_created++;
					m_frequency = (6*m_created)/10 + 1;
				}
			}
		}
	}
	
	public GameObject m_towerPrefab;
	public GameObject m_enemyPrefab;
	int m_created;
	int m_frequency;
    int m_startTime;
}
