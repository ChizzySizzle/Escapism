
using UnityEngine;
using UnityEngine.UI;

public class SFXVol_Slider_Controller : MonoBehaviour
{
    private Slider musicSlider;

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.onValueChanged.AddListener(Audio_Manager.instance.ChangeSFXVolume);
    }

    void OnEnable()
    {
        musicSlider = GetComponent<Slider>();

        musicSlider.value = DeconvertDBValue(PlayerPrefs.GetFloat("SFXVolume", 0));
    }

    private float DeconvertDBValue(float value)
    {
        return Mathf.Pow(10f, value / 20f);
    }
}
