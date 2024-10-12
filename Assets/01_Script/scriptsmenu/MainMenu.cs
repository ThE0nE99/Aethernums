using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    AudioManager audioref;
     void Start()
    {
        audioref = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void jugar()
    {
        StartCoroutine(PlaySoundAndLoadScene());
    }

    private IEnumerator PlaySoundAndLoadScene()
    {
        // Reproducir el sonido
        audioref.PlaySFX(audioref.buton);

        // Esperar a que el sonido termine
        yield return new WaitForSeconds(audioref.buton.length);

        // Cargar la nueva escena
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    public void salir()
    {
        audioref.PlaySFX(audioref.buton);
        Debug.Log("saliendo");
        Application.Quit();
    }
}
