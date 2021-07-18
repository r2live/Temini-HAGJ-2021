using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VcaController : MonoBehaviour
{
    private FMOD.Studio.VCA VcaControl;
    public string VcaName;

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        VcaControl = FMODUnity.RuntimeManager.GetVCA("vca:/" + VcaName);
        slider = GetComponent<Slider>();
    }

    public void SetVolume(float volume)
    {
        VcaControl.setVolume(volume);
    }

}
