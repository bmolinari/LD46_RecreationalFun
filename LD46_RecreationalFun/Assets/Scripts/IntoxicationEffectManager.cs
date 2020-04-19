using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class IntoxicationEffectManager : MonoBehaviour
{
    public PlayerToxicity playerToxicity;
    public CameraFollow cameraFollow;
    public PostProcessVolume globalPostProcessing;

    private ChromaticAberration chromaticAbberation;
    private Bloom bloom;
    private MotionBlur motionBlur;
    private LensDistortion lensDistortion;

    // Start is called before the first frame update
    void Awake()
    {
        globalPostProcessing.profile.TryGetSettings(out chromaticAbberation);
        globalPostProcessing.profile.TryGetSettings(out bloom);
        globalPostProcessing.profile.TryGetSettings(out motionBlur);
        globalPostProcessing.profile.TryGetSettings(out lensDistortion);
    }

    // Update is called once per frame
    void Update()
    {

        if (playerToxicity.CurrentToxcicity <= 0)
        {
            SoberSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 1 && playerToxicity.CurrentToxcicity < 100)
        {
            FirstSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 100 && playerToxicity.CurrentToxcicity < 150)
        {
            SecondSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 150 && playerToxicity.CurrentToxcicity < 200)
        {
            ThirdSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 200 && playerToxicity.CurrentToxcicity < 250)
        {
            FourthSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 250 && playerToxicity.CurrentToxcicity < 300)
        {
            FifthSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 300 && playerToxicity.CurrentToxcicity < 350)
        {
            SixthSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 350 && playerToxicity.CurrentToxcicity < 400)
        {
            SeventhSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 400 && playerToxicity.CurrentToxcicity < 450)
        {
            EighthSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 450)// && playerToxicity.CurrentToxcicity < 500)
        {
            NinthSettings();
        }



        if (Input.GetKeyDown(KeyCode.J))
        {
            chromaticAbberation.intensity.value = .5f;
        }
    }

    private void SoberSettings()
    {
        cameraFollow.smoothSpeed = 1;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = false;
        motionBlur.active = false;
        lensDistortion.active = false;
        chromaticAbberation.active = false;

    }

    private void FirstSettings()
    {
        cameraFollow.smoothSpeed = 0.075f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = false;
        lensDistortion.active = false;
        chromaticAbberation.active = false;
    }

    private void SecondSettings()
    {
        cameraFollow.smoothSpeed = 0.075f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = false;
        chromaticAbberation.active = false;
    }

    private void ThirdSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = false;
        chromaticAbberation.active = false;
    }

    private void FourthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = false;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .2f;
    }

    private void FifthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = false;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .2f;
    }

    private void SixthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = false;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .2f;
    }

    private void SeventhSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = false;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .2f;
    }

    private void EighthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = false;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
    }

    private void NinthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = false;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
    }
}
