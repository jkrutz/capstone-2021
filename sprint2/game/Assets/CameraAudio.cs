using UnityEngine;
using UnityEngine.UI;

public class CameraAudio : MonoBehaviour
{
    [SerializeField]
    private Slider volume_slider;

    private Component[] audioSources;

    public float proportional_volume_theme = 0.04f;
    public float proportional_volume_ambiance = 0.26f;

    // Start is called before the first frame update
    void Awake()
    {
        audioSources = GetComponents(typeof(AudioSource));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(audioSources[0].gameObject.name + " " + audioSources[1].gameObject.name);
        //audioSource.volume = proportional_volume * volume_slider.value;
    }
}
