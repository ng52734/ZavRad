using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void MainM()
    {
        SceneManager.LoadScene(0);
    }

    public void ARScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Lekc3D()
    {
        SceneManager.LoadScene(2);
    }

    public void LekcSl()
    {
        SceneManager.LoadScene(3);
    }

    public void Kviz3D()
    {
        SceneManager.LoadScene(4);
    }

    public void KvizSl()
    {
        SceneManager.LoadScene(5);
    }


}
