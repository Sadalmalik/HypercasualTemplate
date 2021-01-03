using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInputWidget : MonoBehaviour
{
	public RectTransform screenArea;
	public CanvasGroup group;
	public RectTransform pointerBase;
	public RectTransform pointerStick;

	[Space]
	public float fadeDuration = 0.3f;

	public float maxRadius = 200;
	public float sensitivity = 10;

	public bool control;
	public Vector2 values;

	public event Action OnStartControl;
	public event Action OnEndControl;

	private Vector2 _startPoint;
	private Sequence _active;

	void Start()
	{
		control = false;
		
		group.alpha = 0;

		_active = null;
	}

	private void PlayAnimation(bool show)
	{
		_active?.Kill();
		_active = DOTween.Sequence();
		_active.Append(group.DOFade(show ? 1 : 0, fadeDuration));
		_active.Play();
	}

	void Update()
	{
		if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			UpdatePointer(Input.mousePosition);
		}
		else if (Input.touchCount > 0)
		{
			var touch = Input.touches[0];
			if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
			{
				UpdatePointer(touch.position);
			}
		}
		else if (control)
		{
			control = false;
			PlayAnimation(false);
			OnEndControl?.Invoke();
		}
	}

	private void UpdatePointer(Vector2 position)
	{
		position.x /= Screen.width;
		position.y /= Screen.height;
		position   =  Vector2.Scale(position, screenArea.rect.size);

		if (!control)
		{
			control     = true;
			values      = Vector2.zero;
			_startPoint = position;
			PlayAnimation(true);
			pointerBase.anchoredPosition3D  = position;
			pointerStick.anchoredPosition3D = position;
			OnStartControl?.Invoke();
		}

		values = position - _startPoint;
		var size = values.magnitude;
		if (size > 0)
		{
			values /= size;
			values *= Mathf.Min(size, maxRadius);

			pointerStick.anchoredPosition3D = _startPoint + values;

			values /= maxRadius;
		}
		values *= sensitivity;
	}
}