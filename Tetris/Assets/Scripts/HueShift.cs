using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class HueShift : MonoBehaviour
{   
    [SerializeField]
    private Volume volume;

    private Bloom bloom;

    //private ColorGrading colorGrading;

    private ColorAdjustments colorAdjustments;

    [SerializeField]
    private float speed;

    private void Start()
    {
        volume = GetComponent<Volume>();
       
        volume.sharedProfile.TryGet<Bloom>(out bloom);
        volume.sharedProfile.TryGet<ColorAdjustments>(out colorAdjustments);
        
    }

    private void Update()
    {   
        colorAdjustments.hueShift.Interp(-180, 180, Time.time * speed);
        //colorGrading.hueShift.Interp(-180, 180, Time.deltaTime * speed);
    }
}
