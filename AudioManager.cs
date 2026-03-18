using UnityEngine;
using System.Collections;
using System.Threading;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioInstance;

    private void Awake()
    {
        AudioInstance = this;
    }

    public void PlaySFX( AudioClip clip, float volume = 1f )
    {
        StartCoroutine( PlaySFXCoroutine( clip, volume ) );
    }

    IEnumerator PlaySFXCoroutine( AudioClip clip, float volume = 1f )
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        yield return new WaitForSeconds( clip.length );

        Destroy(audioSource);
    }
}
