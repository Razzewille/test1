using UnityEngine;
using System.Collections;

public abstract class TDEnemy : MonoBehaviour {

	public enum Type
	{
		eBasic = 0,
		eBoss  = 1
	};

	// Use this for initialization
	void Start () {
		m_HP = (int) getStartHP();
		gameObject.renderer.material.color = getColor();
	}

	// Update is called once per frame
	void Update () {
		Vector3 dir = new Vector3(1f, 0f, 0f);
		dir *= getSpeed()*Time.deltaTime;
		transform.Translate(dir);
	}

	protected abstract uint getStartHP();
	protected abstract float getSpeed();
	protected abstract Color getColor();

	void receiveDamage(uint damage)
	{
		m_HP -= (int) damage;
		if (m_HP <= 0)
			Destroy (gameObject);
	}
	
	TDGrid.Cell[] m_path;

	int m_HP;
}
