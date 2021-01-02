using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CastleGameManager : IGameManager
{
#region Regular unity stuff

	public float levelChangeDuration = 1;
	[Space]
	public LevelSelector levels;
	public float levelSpawnHeight = 2;
	private Level _currentPrefab;
	private Level _currentLevel;
	[Space]
	public Player[] skins;
	private Player player;
	
	[Space]
	public CameraController camera;
	public TouchInputWidget touchWidget;
	public Bomb regularBombPrefab;
	public Bomb powerBombPrefab;
	
	[Space]
	public GameObject inGameUI;

	[Space(40)]
	public float testRadius;
	public bool applyRadius;

	private ObjectsPool<Bomb> regularBombs;
	private ObjectsPool<Bomb> powerBombs;
	
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
		
		regularBombs = ObjectsPoolUtils.CreateBehavioursPool(()=>
		{
			var bomb = Instantiate(regularBombPrefab);
			bomb.OnExplode += ()=> regularBombs.Free(bomb);
			return bomb;
		});
		
		powerBombs = ObjectsPoolUtils.CreateBehavioursPool(()=>
		{
			var bomb = Instantiate(powerBombPrefab);
			bomb.OnExplode += ()=> powerBombs.Free(bomb);
			return bomb;
		});
		
		player = Instantiate(skins[0]);
		camera.moveTarget = player.cameraAnchor;
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
			closure.DOMove(-levelSpawnHeight * Vector3.up * closure.radius, levelChangeDuration).OnComplete(()=>
			{
				HierarchyUtils.SafeDestroy(closure.gameObject, 0.1f);
			});
		}
		
		_currentLevel = Instantiate(
			prefab,
			levelSpawnHeight * Vector3.up * prefab.radius,
			Quaternion.identity,
			transform);
		_currentLevel.DOMove(Vector3.zero, levelChangeDuration);
		player.SetShootVelocity(_currentLevel.cannonBallStartVelocity);
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
		var bomb = regularBombs.Lock();
		player.Shoot(bomb);
	}

#endregion
}