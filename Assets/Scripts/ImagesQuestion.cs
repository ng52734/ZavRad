using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImagesQuestion : MonoBehaviour
{
    public Button pitanje, vektor, raster;
    public GameObject top, middle, bottom, middleText;
    public TMPro.TMP_Text topText, bottomText;
    public TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        pitanje.onClick.AddListener(OnPitanjeClicked);
        vektor.onClick.AddListener(OnVektorClicked);
        raster.onClick.AddListener(OnRasterClicked);

        middle.SetActive(false);
        middleText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnPitanjeClicked()
    {
        pitanje.onClick.RemoveAllListeners();
        top.SetActive(false);
        bottom.SetActive(false);
        middle.SetActive(true);
        middleText.SetActive(true);

        topText.text = "Je li trenutna slika prikladnija vektorskom ili rasterskom obliku?";
    }

    void OnVektorClicked()
    {

        switch (dropdown.value)
        {
            case 0:
                bottomText.text = "Točno! Sliku sacinjavaju samo oblici, ne boje, dakle savršena je za vektorski oblik.";
                bottomText.color = Color.green;
                StartCoroutine(EndQuestion());
                break;
            case 1:
                bottomText.text = "Netočno! Za realistične slike jako je bitno očuvanje boja u pikselima, što vektorskom obliku slabo ide.";
                bottomText.color = Color.red;
                break;
            default:
                break;
        }

    }

    void OnRasterClicked()
    {
        switch (dropdown.value)
        {
            case 0:
                bottomText.text = "Netočno! Sliku sačinjavaju samo oblici, ne boje, dakle nije prikladno sliku opisivati piksel po piksel.";
                bottomText.color = Color.red;
                break;
            case 1:
                bottomText.text = "Točno! Za realistične slike jako je bitno očuvanje boja u pikselima, što je upravo poanta rasterskog oblika slike.";
                bottomText.color = Color.green;
                StartCoroutine(EndQuestion());
                break;
            default:
                break;
        }
    }

    IEnumerator EndQuestion()
    {
        pitanje.onClick.AddListener(OnPitanjeClicked);

        yield return new WaitForSeconds(5f);

        bottomText.text = "";
        top.SetActive(true);
        bottom.SetActive(true);
        middle.SetActive(false);
        middleText.SetActive(false);
    }
}
