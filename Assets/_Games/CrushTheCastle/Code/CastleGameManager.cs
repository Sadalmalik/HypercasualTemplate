using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CastleGameManager : IGameManager
{
#region Regular unity stuff

	public float levelChangeDuration;
	[Space]
	public LevelSelector levels;
	private Level _currentPrefab;
	private Level _currentLevel;
	[Space]
	public Player[] skins;
	private Player player;
	
	[Space]
	public GameObject inGameUI;

	public Transform shootAnchor;
	public Transform cameraAnchor;

	[Space]
	public TouchInputWidget touchWidget;

	public PooledRingBuilder pooledRingBuilder;
	public TankController tankController;
	public Cannon cannon;
	public Bomb bomb;

	[Space(40)]
	public float testRadius;
	public bool applyRadius;

	void Update()
	{
		if (touchWidget.control)
		{
			player.Move(-touchWidget.values);
		}

		if (applyRadius)
		{
			applyRadius = false;
			player.DORadius(testRadius, levelChangeDuration);
		}
	}

#endregion

#region GameManager Api

	public override void Init()
	{
		Debug.Log("[TEST] GameManager.Init()");
		inGameUI.SetActive(false);

		touchWidget.OnStartControl += HandleStartControl;
		touchWidget.OnEndControl   += HandleEndControl;
		
		player = Instantiate(skins[0]);
	}

	public override void LoadLevel(int index)
	{
		Debug.Log("[TEST] GameManager.LoadLevel()");
		_currentPrefab = levels.GetLevel(index);
		LoadLevel(_currentPrefab);
	}
	
	public override void ResetLevel()
	{
		Debug.Log("[TEST] GameManager.ResetLevel()");
		LoadLevel(_currentPrefab);
	}
	
	private void LoadLevel(Level prefab)
	{
		if (_currentLevel != null)
		{
			var closure = _currentLevel;
			closure.DOMove(-Vector3.up * closure.radius, levelChangeDuration).OnComplete(()=>
			{
				HierarchyUtils.SafeDestroy(closure.gameObject, 0.1f);
			});
		}
		
		_currentLevel = Instantiate(
			prefab,
			Vector3.up * prefab.radius,
			Quaternion.identity,
			transform);
		_currentLevel.DOMove(Vector3.zero, levelChangeDuration);
		player.DORadius(prefab.radius, levelChangeDuration);
	}

	public override void StartLevel()
	{
		Debug.Log("[TEST] GameManager.StartLevel()");
		inGameUI.SetActive(true);
	}

	public override void PauseLevel(bool pause)
	{
		Debug.Log("[TEST] GameManager.PauseLevel()");
		inGameUI.SetActive(!pause);
	}

	private void HandleStartControl()
	{
		Debug.Log("[TEST] HandleStartControl");
	}

	private void HandleEndControl()
	{
		Debug.Log("[TEST] HandleEndControl");
		bomb.Shoot();
		cannon.Shoot(bomb.body);
	}

#endregion
}