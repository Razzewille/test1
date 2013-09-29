﻿using UnityEngine;
using System.Collections;

public abstract class TDEnemy : TDActor {

	public enum Type
	{
		eImp = 0,
		eGargoyle  = 1
	};

	public delegate void EventHandler(TDEnemy enemy);
	public event EventHandler OnEventDestroy;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		GameObject enemyHealthPrefab = (GameObject) Resources.Load("EnemyHealthBarPrefab");
		m_healthBar = (GameObject) Instantiate(enemyHealthPrefab, new Vector3(0.5f, 0.5f), new Quaternion());
		updateHealthBar();
		
		TDWorld world = TDWorld.getWorld();
		GameObject player = world.getPlayer();
		hasPathTo(player);

		m_timer = Time.time;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update();		
		updateHealthBar();
		
		TDWorld world = TDWorld.getWorld();
		if (Time.time - m_timer > world.m_configuration.enemyRecalcPathTime)
		{
			GameObject player = world.getPlayer();
			m_timer = Time.time;
			hasPathTo(player);
		}
	}

	void updateHealthBar()
	{
		if (m_healthBar == null)
			return;
		Vector3 txtPos = Camera.main.WorldToViewportPoint(transform.position);
		m_healthBar.transform.position = txtPos;
		float hpLeft = ((float) m_HP)/((float) getStartHP());
		hpLeft *= 30;
		m_healthBar.guiTexture.pixelInset = new Rect(0, 0, hpLeft, 10);
	}

	void OnDestroy()
	{
		Destroy(m_healthBar);
		if (OnEventDestroy != null)
			OnEventDestroy(this);
		TDWorld world = TDWorld.getWorld();
		if (world == null)
			return;
		TDPlayer player = world.getTDPlayer();
		if (player == null)
			return;
		player.reward(killReward());
	}

	protected override void onTargetReached(GameObject obj)
	{
		TDWorld world = TDWorld.getWorld();
		GameObject player = world.getPlayer();
		if (obj == player)
		{
			TDPlayer tdP = world.getTDPlayer();
			tdP.receiveDamage(1);
		}
		DestroyObject(gameObject);
	}

	protected override void onTargetDestroyed()
	{
	}

	protected abstract uint killReward();

	GameObject m_healthBar;
	float m_timer;
}
