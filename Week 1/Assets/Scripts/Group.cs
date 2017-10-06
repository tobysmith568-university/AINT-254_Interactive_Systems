using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using Assets.Scripts;

public class Group : MonoBehaviour
{
    private CanvasGroup group;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Text text;

    [SerializeField]
    private Slider slider;
    
    void Start()
    {
        group = GetComponent<CanvasGroup>();

        group.alpha = 0;

        SetAmbientvolume(80f);
        slider.value = 80f;

        SoundManager.SetManagers(audioMixer);
    }

    public void SetAmbientvolume(float volume)
    {
        audioMixer.SetFloat("AmbientVolume", volume - 80);
        text.text = volume + "%";
    }

	void Update()
    {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(group.alpha == 0)
            {
                group.alpha = 1;
                SoundManager.MenuMode();
            }
            else
            {
                group.alpha = 0;
                SoundManager.GameMode();
            }
        }
	}
}
