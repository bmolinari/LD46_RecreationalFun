using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class IntoxicationEffectManager : MonoBehaviour
{
    public PlayerToxicity playerToxicity;
    public CameraFollow cameraFollow;
    public Volume globalPostProcessing;

    private ChromaticAberration chromaticAbberation;
    private Bloom bloom;
    private MotionBlur motionBlur;
    private LensDistortion lensDistortion;
    private Vignette vignette;
    private bool zoomOut;

    // Start is called before the first frame update
    void Awake()
    {
        globalPostProcessing.profile.TryGet(out chromaticAbberation);
        globalPostProcessing.profile.TryGet(out bloom);
        globalPostProcessing.profile.TryGet(out motionBlur);
        globalPostProcessing.profile.TryGet(out lensDistortion);
        globalPostProcessing.profile.TryGet(out vignette);
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
        else if (playerToxicity.CurrentToxcicity >= 450 && playerToxicity.CurrentToxcicity < 500)
        {
            NinthSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 500 && playerToxicity.CurrentToxcicity < 550)
        {
            TenthSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 550 && playerToxicity.CurrentToxcicity < 600)
        {
            EleventhSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 600 && playerToxicity.CurrentToxcicity < 650)
        {
            TwelvethSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 650 && playerToxicity.CurrentToxcicity < 700)
        {
            ThirteenthSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 700 && playerToxicity.CurrentToxcicity < 750)
        {
            FourteenthSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 750 && playerToxicity.CurrentToxcicity < 700)
        {
            FifteenthSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 800 && playerToxicity.CurrentToxcicity < 850)
        {
            SixteenthSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 850 && playerToxicity.CurrentToxcicity < 900)
        {
            SeventeenthSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 900 && playerToxicity.CurrentToxcicity < 950)
        {
            EighteenthSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 950 && playerToxicity.CurrentToxcicity < 1000)
        {
            NineteenthSettings();
        }
        else if (playerToxicity.CurrentToxcicity >= 1000)
        {
            FinalSettings();
        }
    }

    private void SoberSettings()
    {
        cameraFollow.smoothSpeed = 1;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = false;
        motionBlur.active = false;
        lensDistortion.active = true;
        chromaticAbberation.active = false;
        vignette.active = false;

        if (lensDistortion.intensity.value > 0)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
        }
        else if (lensDistortion.intensity.value < 0)
        {
            lensDistortion.intensity.value += Time.deltaTime;
        }
        else
        {
            return;
        }
    }

    private void FirstSettings()
    {
        cameraFollow.smoothSpeed = 0.075f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = false;
        lensDistortion.active = true;
        chromaticAbberation.active = false;
        vignette.active = false;

        if (lensDistortion.intensity.value > 0)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
        }
        else if (lensDistortion.intensity.value < 0)
        {
            lensDistortion.intensity.value += Time.deltaTime;
        }
        else
        {
            return;
        }
    }

    private void SecondSettings()
    {
        cameraFollow.smoothSpeed = 0.075f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = true;
        chromaticAbberation.active = false;
        vignette.active = false;
        if (lensDistortion.intensity.value > 0)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
        }
        else if (lensDistortion.intensity.value < 0)
        {
            lensDistortion.intensity.value += Time.deltaTime;
        }
        else
        {
            return;
        }
    }

    private void ThirdSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = true;
        chromaticAbberation.active = false;
        vignette.active = false;

        if (lensDistortion.intensity.value > 0)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
        }
        else if (lensDistortion.intensity.value < 0)
        {
            lensDistortion.intensity.value += Time.deltaTime;
        }
        else
        {
            return;
        }
    }

    private void FourthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        lensDistortion.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .2f;
        vignette.active = false;
        motionBlur.active = true;

        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.2f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.2f)
                zoomOut = !zoomOut;
        }
    }

    private void FifthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .2f;
        vignette.active = false;
        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.2f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.2f)
                zoomOut = !zoomOut;
        }
    }

    private void SixthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .2f;
        vignette.active = false;
        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.2f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.2f)
                zoomOut = !zoomOut;
        }
    }

    private void SeventhSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .2f;
        vignette.active = false;
        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.2f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.2f)
                zoomOut = !zoomOut;
        }
    }

    private void EighthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
        vignette.active = false;
        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.2f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.2f)
                zoomOut = !zoomOut;
        }
    }

    private void NinthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
        vignette.active = false;
        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.2f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.2f)
                zoomOut = !zoomOut;
        }
    }

    private void TenthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        lensDistortion.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
        vignette.active = false;
        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.2f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.2f)
                zoomOut = !zoomOut;
        }
    }

    private void EleventhSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
        vignette.active = false;

        lensDistortion.active = true;
        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.2f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.2f)
                zoomOut = !zoomOut;
        }

    }

    private void TwelvethSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
        lensDistortion.active = true;
        vignette.active = false;

        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.5f)
            zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.5f)
                zoomOut = !zoomOut;
        }
    }

    private void ThirteenthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
        lensDistortion.active = true;
        vignette.active = false;

        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.5f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.5f)
                zoomOut = !zoomOut;
        }
    }

    private void FourteenthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
        vignette.active = true;
        vignette.intensity.value = .5f;

        lensDistortion.active = true;

        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.5f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.5f)
                zoomOut = !zoomOut;
        }
    }

    private void FifteenthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
        lensDistortion.active = true;
        vignette.active = true;
        vignette.intensity.value = .5f;


        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.5f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.5f)
                zoomOut = !zoomOut;
        }
    }

    private void SixteenthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
        lensDistortion.active = true;
        vignette.active = true;
        vignette.intensity.value = .5f;


        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.5f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.5f)
                zoomOut = !zoomOut;
        }
    }

    private void SeventeenthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
        lensDistortion.active = true;
        vignette.active = true;
        vignette.intensity.value = .5f;


        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.65f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.65f)
                zoomOut = !zoomOut;
        }
    }

    private void EighteenthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
        lensDistortion.active = true;
        vignette.active = true;
        vignette.intensity.value = .5f;


        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.65f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.65f)
                zoomOut = !zoomOut;
        }
    }

    private void NineteenthSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
        lensDistortion.active = true;
        vignette.active = true;
        vignette.intensity.value = .5f;

        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.65f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.65f)
                zoomOut = !zoomOut;
        }
    }

    private void FinalSettings()
    {
        cameraFollow.smoothSpeed = 0.035f;
        cameraFollow.offset = new Vector3(0, 0, -10);
        bloom.active = true;
        motionBlur.active = true;
        chromaticAbberation.active = true;
        chromaticAbberation.intensity.value = .5f;
        lensDistortion.active = true;
        vignette.active = true;
        vignette.intensity.value = .5f;


        if (zoomOut)
        {
            lensDistortion.intensity.value -= Time.deltaTime;
            if (lensDistortion.intensity.value <= -0.65f)
                zoomOut = !zoomOut;
        }
        else
        {
            lensDistortion.intensity.value += Time.deltaTime;
            if (lensDistortion.intensity.value >= 0.65f)
                zoomOut = !zoomOut;
        }
    }

}
