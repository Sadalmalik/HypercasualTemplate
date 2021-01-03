using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ObjectsPoolUtils
{
	public static ObjectsPool<GameObject> CreateGameObjectsPool(
		Func<GameObject> factory = null,
		bool activeOnLock=true,
		bool activeOnFree=false)
	{
		return new ObjectsPool<GameObject>(factory,
			gameObject => gameObject.SetActive(activeOnLock),
			gameObject => gameObject.SetActive(activeOnFree)
		);
	}
	
	public static ObjectsPool<Transform> CreateTransformsPool(
		Func<Transform> factory = null,
		bool activeOnLock=true,
		bool activeOnFree=false)
	{
		return new ObjectsPool<Transform>(factory,
			transform => transform.gameObject.SetActive(activeOnLock),
			transform => transform.gameObject.SetActive(activeOnFree)
		);
	}
	
	public static ObjectsPool<T> CreateBehavioursPool<T>(
		Func<T> factory = null,
		bool activeOnLock=true,
		bool activeOnFree=false)
			where T : MonoBehaviour
	{
		return new ObjectsPool<T>(factory,
			transform => transform.gameObject.SetActive(activeOnLock),
			transform => transform.gameObject.SetActive(activeOnFree)
		);
	}
	
	public static ObjectsPool<T> CreateBehavioursPool<T>(
		T prefab,
		bool activeOnLock=true,
		bool activeOnFree=false)
			where T : MonoBehaviour
	{
		return new ObjectsPool<T>(
			() => Object.Instantiate(prefab),
			behaviour => behaviour.gameObject.SetActive(activeOnLock),
			behaviour => behaviour.gameObject.SetActive(activeOnFree)
		);
	}
}

public class ObjectsPool<T> where T : Object
{
	private readonly Queue<T> _objects;
	private readonly HashSet<T> _set;

	private readonly Func<T> _factory;
	private readonly Action<T> _onLock;
	private readonly Action<T> _onFree;
	
	public ObjectsPool(Func<T> factory = null, Action<T> onLock = null, Action<T> onFree = null)
	{
		_objects = new Queue<T>();
		_set = new HashSet<T>();

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
			{
				instance = _objects.Dequeue();
				_set.Remove(instance);
			}
		}

		if (instance == null)
			instance = _factory();

		_onLock?.Invoke(instance);
		
		return instance;
	}

	public void Free(T instance)
	{
		if (instance == null)
			return;

		_onFree?.Invoke(instance);

		lock (_objects)
		{
			if (!_set.Contains(instance))
			{
				_objects.Enqueue(instance);
				_set.Add(instance);
			}
			else
			{
				Debug.LogWarning($"Duplication object '{instance.name}' in pool!");
			}
		}
	}
}