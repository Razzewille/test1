﻿using UnityEngine;
using System.Collections;

// Common interface for damage, poison, slow or heal
public abstract class TDModifier
{
	public void setTarget(TDActor target)
	{
		m_target = target;
		m_startTime = Time.time;
		m_firstTime = true;
	}

	public void apply()
	{
		if (m_firstTime)
		{
			applyFirstTime();
			m_firstTime = false;
		}
		applyContinuous();
	}

	public bool finished()
	{
		return (Time.time - m_startTime > applyTime());
	}

	protected abstract void applyFirstTime();

	protected abstract void applyContinuous();

	protected abstract float applyTime();

	bool m_firstTime;
	float m_startTime;
	protected TDActor m_target;
}

public class TDDamage : TDModifier
{
	public enum Type
	{
		ePhysical = 0,
		eWaterMagic = 1,
		ePoison = 2,
	 	eElectricity = 3
	}

	public TDDamage(Type type, float firstDamage, float contDamagePerSec)
	{
		m_type = type;
		m_firstDamage = firstDamage;
		m_contDamagePerSec = contDamagePerSec;
	}

	protected override float applyTime() // Does instant damage if not overriden
	{
		return 0;
	}

	protected override void applyFirstTime()
	{
		if (m_target == null)
			return;
		m_target.receiveDamage(m_type, m_firstDamage);
		
	}
	
	protected override void applyContinuous()
	{
		if (m_target == null)
			return;
		m_target.receiveDamage(m_type, m_contDamagePerSec*Time.deltaTime);
	}

	Type m_type;
	float m_firstDamage;
	float m_contDamagePerSec;
}