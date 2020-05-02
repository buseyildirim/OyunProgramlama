using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SahneKontrol : MonoBehaviour
{
   
    public void SonrakiSahne()
    {
       //Mevcutt sahnenin indexini alabilmek için
        int mevcutSahneİndeksi = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(mevcutSahneİndeksi+1);
    }

    public void OyunSahnesineYonlen() {
        SceneManager.LoadScene(2);


    }
    public void AyarlarSahnesineYonlen()
    {
        SceneManager.LoadScene(1);

    }
    public void OyundanCikis()
    {
        Application.Quit();

    }
}
