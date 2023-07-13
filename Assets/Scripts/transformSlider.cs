using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class transformSlider : MonoBehaviour
{
    private Slider sliderX;
    private Slider sliderY;
    private Slider sliderZ;
    private TMP_Dropdown dropdown;

    private string selectedTransform;

    const float a = 0.0155f, b = 0.0495f, c = 0.035f; 

    void Start()
    {
        sliderX = GameObject.FindGameObjectWithTag("SliderX").GetComponent<Slider>();
        sliderY = GameObject.FindGameObjectWithTag("SliderY").GetComponent<Slider>();
        sliderZ = GameObject.FindGameObjectWithTag("SliderZ").GetComponent<Slider>();
        dropdown = GameObject.FindGameObjectWithTag("TransDropdown").GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    void Update()
    {

        selectedTransform = dropdown.options[dropdown.value].text;

        float sliderXvalue = sliderX.value;
        float sliderYvalue = sliderY.value;
        float sliderZvalue = sliderZ.value;

        switch (selectedTransform)
        {
            case "Translacija":
                this.transform.localPosition = new Vector3(sliderXvalue * 0.03f, sliderYvalue * 0.03f, sliderZvalue * 0.03f);
                break;
            case "Rotacija":
                Quaternion xQuaternion = Quaternion.Euler(sliderXvalue * 90f, 0f, 0f);
                Quaternion yQuaternion = Quaternion.Euler(0f, sliderYvalue * 90f, 0f);
                Quaternion zQuaternion = Quaternion.Euler(0f, 0f, sliderZvalue * 90f);
                this.transform.localRotation = xQuaternion * yQuaternion * zQuaternion;
                break;
            case "Skaliranje":
                sliderXvalue = (float)(a * Math.Pow(sliderXvalue, 2) + b * sliderXvalue + c); //mapiranje slidera od -1, 1 na 0.001, 0.1
                sliderYvalue = (float)(a * Math.Pow(sliderYvalue, 2) + b * sliderYvalue + c);
                sliderZvalue = (float)(a * Math.Pow(sliderZvalue, 2) + b * sliderZvalue + c);
                this.transform.localScale = new Vector3(sliderXvalue, sliderYvalue, sliderZvalue);
                break;
            default:
                break;
        }
    }

    private void OnDropdownValueChanged(int value)
    {

        string selectedTransform = dropdown.options[dropdown.value].text;

        switch (selectedTransform)
        {
            case "Translacija":
                sliderX.value = this.transform.localPosition.x / 0.03f;
                sliderY.value = this.transform.localPosition.y / 0.03f;
                sliderZ.value = this.transform.localPosition.z / 0.03f;
                break;
            case "Rotacija":
                Vector3 eulerAngles = this.transform.localRotation.eulerAngles;
                sliderX.value = eulerAngles.x / 90f;
                sliderY.value = eulerAngles.y / 90f;
                sliderZ.value = eulerAngles.z / 90f;
                break;
            case "Skaliranje":
                sliderX.value = (-b + Mathf.Sqrt(b * b - 4 * a * (c - this.transform.localScale.x))) / (2 * a);
                sliderY.value = (-b + Mathf.Sqrt(b * b - 4 * a * (c - this.transform.localScale.y))) / (2 * a);
                sliderZ.value = (-b + Mathf.Sqrt(b * b - 4 * a * (c - this.transform.localScale.z))) / (2 * a);
                break;
            default:
                break;
        }
    }


}

