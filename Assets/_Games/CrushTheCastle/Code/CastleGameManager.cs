using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CastleGameManager : IGameManager
{
#region Regular unity stuff

	public LevelSelector levels;

	public Level currentLevel;

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
	public float radius;

	public float duration;

	public bool applyRadius;

	private float _radius;

	void Start()
	{
		_radius = radius;
		pooledRingBuilder.Rebuild(_radius);
		tankController.SetRadius(_radius, pooledRingBuilder.steps);
	}

	void Update()
	{
		if (touchWidget.control)
		{
			tankController.Move(-touchWidget.values);
		}

		if (applyRadius)
		{
			applyRadius = false;

			DOTween.To(
					() => _radius,
					rad =>
					{
						_radius = rad;
						pooledRingBuilder.Rebuild(_radius);
						tankController.SetRadius(_radius, pooledRingBuilder.steps);
					}, radius, duration
				);
				
		}
	}

#endregion

#region GameManager Api

	public override void Init()
	{
		EventTrigger.Entry entry;
		Debug.Log("[TEST] GameManager.Init()");
		inGameUI.SetActive(false);

		touchWidget.OnStartControl += HandleStartControl;
		touchWidget.OnEndControl   += HandleEndControl;
	}

	public override void LoadLevel(int index)
	{
		if (currentLevel != null)
			HierarchyUtils.SafeDestroy(currentLevel.gameObject);
		currentLevel = Instantiate(levels[index % levels.Length]);
		Debug.Log("[TEST] GameManager.LoadLevel()");
	}

	public override void ResetLevel()
	{
		Debug.Log("[TEST] GameManager.ResetLevel()");
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