using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CastleGameManager : IGameManager
{
#region Regular unity stuff
	
	public Level[]levels;
	
	public Level currentLevel;
	[Space]
	public GameObject buttons;
	public EventTrigger btnLeft;
	public EventTrigger btnShoot;
	public EventTrigger btnRight;
	[Space]
	public Transform axis;
	public float rotationSpeed;
	public Transform cannomAxis;
	public float cannonSpeed;
	public Cannon cannon;
	
	private float rotateDirection = 0;
	private float angle;
	private float cannonAngle;
	private bool rotateCannon;
	
    void Start()
    {
    }

    void Update()
    {
		angle += rotateDirection * rotationSpeed * Time.deltaTime;
        axis.rotation = Quaternion.Euler(0,angle,0);
        
        cannonAngle += rotateCannon ? cannonSpeed : 0 ;
        var finalAngle = -0.5f * (Mathf.Sin(cannonAngle*Mathf.Deg2Rad) + 1) * 70;
        cannomAxis.localRotation = Quaternion.Euler(finalAngle,0,0);
    }

#endregion

#region GameManager Api

	public override void Init()
    {
		EventTrigger.Entry entry;
		Debug.Log("[TEST] GameManager.Init()");
		buttons.SetActive(false);
		
		Subscride(btnLeft, EventTriggerType.PointerEnter, OnLeftPress);
		Subscride(btnLeft, EventTriggerType.PointerExit, OnLeftRelease);

		Subscride(btnShoot, EventTriggerType.PointerEnter, OnShootPress);
		Subscride(btnShoot, EventTriggerType.PointerExit, OnShootRelease);
		
		Subscride(btnRight, EventTriggerType.PointerEnter, OnRightPress);
		Subscride(btnRight, EventTriggerType.PointerExit, OnRightRelease);
    }
    
    private void Subscride(EventTrigger trigger, EventTriggerType type, Action callback)
    {
		EventTrigger.Entry entry = new EventTrigger.Entry {eventID = type};
		entry.callback.AddListener(data=>callback());
        trigger.triggers.Add(entry);
    }
	
    public override void LoadLevel(int index)
    {
		if (currentLevel!=null)
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
		buttons.SetActive(true);
    }

    public override void PauseLevel(bool pause)
    {
		Debug.Log("[TEST] GameManager.PauseLevel()");
		buttons.SetActive(!pause);
    }
    
#endregion

#region GameManager Api
	
	public void OnLeftPress()
	{
		Debug.Log("[TEST] GameManager.OnLeftPress()");
		rotateDirection += 1;
	}
	
	public void OnLeftRelease()
	{
		Debug.Log("[TEST] GameManager.OnLeftRelease()");
		rotateDirection -= 1;
	}
	
	public void OnShootPress()
	{
		Debug.Log("[TEST] GameManager.OnShootPress()");
		rotateCannon = true;
	}
	
	public void OnShootRelease()
	{
		Debug.Log("[TEST] GameManager.OnShootRelease()");
		rotateCannon = false;
		cannon.shoot = true;
	}
	
	public void OnRightPress()
	{
		Debug.Log("[TEST] GameManager.OnRightPress()");
		rotateDirection -= 1;
	}
	
	public void OnRightRelease()
	{
		Debug.Log("[TEST] GameManager.OnRightRelease()");
		rotateDirection += 1;
	}
	
#endregion

}
