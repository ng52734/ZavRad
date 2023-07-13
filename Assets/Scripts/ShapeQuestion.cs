using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShapeQuestion : MonoBehaviour
{
    public Button pitanje, nacrt, tlocrt, bokocrt;
    public GameObject middle, bottom, nContainer, tContainer, bContainer;
    public TMPro.TMP_Text topText, bottomText;

    // Start is called before the first frame update
    void Start()
    {
        pitanje.onClick.AddListener(OnPitanjeClicked);
        nacrt.onClick.AddListener(OnNacrtClicked);
        tlocrt.onClick.AddListener(OnTlocrtClicked);
        bokocrt.onClick.AddListener(OnBokocrtClicked);

        nContainer.SetActive(false);
        tContainer.SetActive(false);
        bContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnPitanjeClicked()
    {
        pitanje.onClick.RemoveAllListeners();
        bottom.SetActive(false);
        middle.SetActive(true);
        nContainer.SetActive(true);
        tContainer.SetActive(true);
        bContainer.SetActive(true);

        topText.text = "Koja projekcija je najpogodnija za prikaz plana prostora?";
    }

    void OnNacrtClicked()
    {
        bottomText.text = "Netočno!";
        bottomText.color = Color.red;
    }

    void OnTlocrtClicked()
    {
        bottomText.text = "Točno! Tlocrt je najpogodniji jer je za plan prostora najbitnije znati širinu i duljinu, ne nužno visinu.";
        bottomText.color = Color.green;
        StartCoroutine(WaitAndMoveToNextQuestion());
    }

    void OnBokocrtClicked()
    {
        bottomText.text = "Netočno!";
        bottomText.color = Color.red;
    }

    IEnumerator WaitAndMoveToNextQuestion()
    {

        yield return new WaitForSeconds(4f);

        topText.text = "Koja projekcija je najpogodnija za prikaz modela lika u igri?";

        bottomText.text = "";
        // Reassign click event listeners
        nacrt.onClick.RemoveAllListeners();
        tlocrt.onClick.RemoveAllListeners();
        bokocrt.onClick.RemoveAllListeners();
        nacrt.onClick.AddListener(OnNacrtClickedSecondQuestion);
        tlocrt.onClick.AddListener(OnTlocrtClickedSecondQuestion);
        bokocrt.onClick.AddListener(OnBokocrtClicked);
    }

    void OnNacrtClickedSecondQuestion()
    {
        bottomText.text = "Točno! Nacrt je ubiti pogled tijela sprijeda, što je korisno za prikazivanje visine i širine modela lika.";
        bottomText.color = Color.green;
        StartCoroutine(WaitAndDisableMiddle());

    }

    void OnTlocrtClickedSecondQuestion()
    {
        bottomText.text = "Netočno!";
        bottomText.color = Color.red;
    }

    IEnumerator WaitAndDisableMiddle()
    {
        nContainer.SetActive(false);
        tContainer.SetActive(false);
        bContainer.SetActive(false);

        yield return new WaitForSeconds(3f);

        bottomText.text = "";

        middle.SetActive(false);
        bottom.SetActive(true);

        nacrt.onClick.RemoveAllListeners();
        tlocrt.onClick.RemoveAllListeners();
        bokocrt.onClick.RemoveAllListeners();
        pitanje.onClick.AddListener(OnPitanjeClicked);
        nacrt.onClick.AddListener(OnNacrtClicked);
        tlocrt.onClick.AddListener(OnTlocrtClicked);
        bokocrt.onClick.AddListener(OnBokocrtClicked);
    }
}
