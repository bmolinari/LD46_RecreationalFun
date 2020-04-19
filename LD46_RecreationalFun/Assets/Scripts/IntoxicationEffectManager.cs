using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class IntoxicationEffectManager : MonoBehaviour
{
    public PlayerToxicity playerToxicity;

    public PostProcessVolume globalPostProcessing;


    private ChromaticAberration chromaticAbberation;
    // Start is called before the first frame update
    void Start()
    {
        globalPostProcessing.profile.TryGetSettings(out chromaticAbberation);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            chromaticAbberation.intensity.value = .5f;
        }
    }
}
