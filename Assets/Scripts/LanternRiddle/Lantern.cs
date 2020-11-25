using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    public int flag = 1;
    public Riddle.Mystery mystery;

    private Material[] materials;
    private Material material;

    private Light mlight;

    // Start is called before the first frame update
    void Start()
    {
        mlight = GetComponentInChildren<Light>();
        mystery = Riddle._instance.GetRiddle();
        materials = GetComponent<Renderer>().materials;
        material = materials[materials.Length - 1];
    }
    // Update is called once per frames
    void Update()
    {
        
    }

    public void ChangeState()
    {
        if(flag == 1)
        {
            flag <<= 1;
        }
    }

    public void ChangeLight()
    {
        mlight.intensity = 2000;
        mlight.range = 5;
        material.SetFloat("_UseEmissiveIntensity", 1);
        material.SetColor("_EmissiveColor", Color.red);
        material.SetFloat("_EmissiveIntensity", 20);
        material.SetFloat("_EmissiveExposureWeight", 0);
    }

    public string GetMystery()
    {
        return mystery.mystery;
    }

    public string GetTip()
    {
        return mystery.tip;
    }

    public string GetAnswer()
    {
        return mystery.answer;
    }
}
