using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public AudioSource audioSourcePrefab;
	public AudioClip[] explosionSounds;

	private ObjectsPool<AudioSource> audioSources;

	void Awake()
	{
		audioSources = new ObjectsPool<AudioSource>(
			() => Instantiate(audioSourcePrefab),
			source => source.gameObject.SetActive(true),
			source => source.gameObject.SetActive(false));
	}

	public void PlayExplosion(Vector3 position)
	{
		PlayAudio(position, RandomUtils.Choice(explosionSounds));
	}

	public void PlayAudio(Vector3 position, AudioClip clip)
	{
		StartCoroutine(PlayAudioCoroutine(position, clip));
	}

	private IEnumerator PlayAudioCoroutine(Vector3 position, AudioClip clip)
	{
		var source = audioSources.Lock();
		source.transform.position = position;
		source.clip               = clip;
		source.Play();
		yield return new WaitForSeconds(clip.length);
		audioSources.Free(source);
	}
}