using UnityEngine;
using System.Collections;

public abstract class TDEnemy : MonoBehaviour {

	enum Type
	{
		eBasic = 0,
		eBoss  = 1
	};

	// Use this for initialization
	void Start () {
		m_HP = (int) getStartHP();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected abstract uint getStartHP();
	protected abstract float getSpeed();

	void receiveDamage(uint damage)
	{
		m_HP -= (int) damage;
		if (m_HP <= 0)
			Destroy (gameObject);
	}
	
	TDGrid.Cell[] m_path;

	int m_HP;
}
