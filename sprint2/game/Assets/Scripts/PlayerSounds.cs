using UnityEngine;
using UnityEngine.UI;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] step_clips;

    [SerializeField]
    private Slider volume_slider;

    private AudioSource audioSource;

    public float proportional_volume = 1;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        AudioClip clip = GetRandomClip(step_clips);
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }

    private void Update()
    {
        audioSource.volume = proportional_volume * volume_slider.value;
    }
}