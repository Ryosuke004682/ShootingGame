using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TitleLogoAnimation : MonoBehaviour
{
    [SerializeField] private float openSpeed = 0.005f;
    [SerializeField] private float closeSpeed = 0.01f;

    public bool isMaxAlpha;

    public VideoPlayer vp;//vp = videoplayer

    private void Awake()
    {
        vp = GetComponent<VideoPlayer>();
        vp.targetCameraAlpha = 0.0f;
        
    }

    private void Update()
    {
        vp.targetCameraAlpha += openSpeed / 1000;

        if (vp.targetCameraAlpha >= 1)
        {
            isMaxAlpha = true;
            vp.targetCameraAlpha = 1.0f;
        }

        if(isMaxAlpha == true)
        {
            vp.targetCameraAlpha -= closeSpeed / 1000;

            if (vp.targetCameraAlpha <= 0.0f)
            {
                vp.targetCameraAlpha = 0.0f;
                SceneManager.LoadScene("SampleScene");
            }
        }

    }

}
