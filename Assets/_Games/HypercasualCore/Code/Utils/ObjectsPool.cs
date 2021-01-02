using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ObjectsPoolUtils
{
	public static ObjectsPool<GameObject> CreateGameObjectsPool(Func<GameObject> factory = null)
	{
		return new ObjectsPool<GameObject>(factory,
			gameObject => gameObject.SetActive(true),
			gameObject => gameObject.SetActive(false)
		);
	}
	
	public static ObjectsPool<Transform> CreateTransformsPool(Func<Transform> factory = null)
	{
		return new ObjectsPool<Transform>(factory,
			transform => transform.gameObject.SetActive(true),
			transform => transform.gameObject.SetActive(false)
		);
	}
	public static ObjectsPool<T> CreateBehavioursPool<T>(Func<T> factory = null) where T : MonoBehaviour
	{
		return new ObjectsPool<T>(factory,
			transform => transform.gameObject.SetActive(true),
			transform => transform.gameObject.SetActive(false)
		);
	}
}

public class ObjectsPool<T> where T : Object
{
	
	private readonly Queue<T> _objects;

	private readonly Func<T> _factory;
	private readonly Action<T> _onLock;
	private readonly Action<T> _onFree;

	public ObjectsPool(Func<T> factory = null, Action<T> onLock = null, Action<T> onFree = null)
	{
		_objects = new Queue<T>();

		if (factory == null)
			factory = () => (T) Activator.CreateInstance(typeof(T));
		_factory = factory;

		_onLock = onLock;
		_onFree = onFree;
	}

	public T Lock()
	{
		T instance = null;

		lock (_objects)
		{
			if (_objects.Count > 0)
				instance = _objects.Dequeue();
		}

		if (instance == null)
			instance = _factory();

		_onLock(instance);
		return instance;
	}

	public void Free(T instance)
	{
		if (instance == null)
			return;

		_onFree(instance);

		lock (_objects)
		{
			_objects.Enqueue(instance);
		}
	}
}