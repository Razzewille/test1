using UnityEngine;
using System.Collections.Generic;

public abstract class TDActor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (TDModifier m in m_aModifier)
		{
			if (m != null)
			{
				m.apply();
				if (m_HP <= 0)
				{
					die();
					break;
				}
				if (m.finished())
					m_aModifier.Remove(m);
			}
			else
				m_aModifier.Remove(m);
		}
	}

	void die()
	{
		Destroy(gameObject);
	}

	public void receiveDamage(TDDamage damage)
	{
		m_aModifier.Add(damage);
	}

	public abstract uint getStartHP();
	public abstract float getSpeed();

	public void receiveDamage(TDDamage.Type type, float val)
	{
		m_HP -= (1.0f - getResistance(type))*val;
	}

	public abstract float getResistance(TDDamage.Type type);

	public abstract Color getColor();

	protected List<TDModifier> m_aModifier;
	protected float m_HP;
}
