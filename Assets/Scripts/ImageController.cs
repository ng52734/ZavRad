using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageController : MonoBehaviour
{
    private FixedJoystick joystick;
    private Slider scaleSlider;
    private TMP_Dropdown imageDropdown;
    private Button VecButton, RasButton;
    public MeshRenderer planeRenderer;
    public Texture[] texturesRas;
    public Texture[] texturesVec;
    private bool isRasterActive = true;

    private void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("MoveStick").GetComponent<FixedJoystick>();
        scaleSlider = GameObject.FindGameObjectWithTag("SliderScale").GetComponent<Slider>();
        imageDropdown = GameObject.FindGameObjectWithTag("ImageDropdown").GetComponent<TMP_Dropdown>();
        VecButton = GameObject.FindGameObjectWithTag("VectorButton").GetComponent<Button>();
        RasButton = GameObject.FindGameObjectWithTag("RasterButton").GetComponent<Button>();

        planeRenderer.material.mainTexture = texturesRas[imageDropdown.value];

        imageDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        VecButton.onClick.AddListener(OnVectorButtonClick);
        RasButton.onClick.AddListener(OnRasterButtonClick);
    }

    private void Update()
    {
        float joystickX = joystick.Horizontal;
        float joystickY = joystick.Vertical;
        float scale = scaleSlider.value;
        int selectedImageIndex = imageDropdown.value;

        Vector3 movementDirection = new Vector3(joystickX, 0f, joystickY);
        float movementSpeed = 0.3f;
        transform.position += movementDirection * movementSpeed * Time.deltaTime;

        this.transform.localScale = new Vector3(1.03f + scale, 1.03f + scale, 1.03f + scale);
    }

    private void OnDropdownValueChanged(int index)
    {
        if (index >= 0 && index < texturesRas.Length)
        {
            if (isRasterActive)
            {
                planeRenderer.material.mainTexture = texturesRas[index];
            }
            else
            {
                planeRenderer.material.mainTexture = texturesVec[index];
            }
        }
    }

    private void OnVectorButtonClick()
    {
        isRasterActive = false;
        OnDropdownValueChanged(imageDropdown.value);
    }

    private void OnRasterButtonClick()
    {
        isRasterActive = true;
        OnDropdownValueChanged(imageDropdown.value);
    }
}
