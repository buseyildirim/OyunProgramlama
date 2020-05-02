using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SesKontrol : MonoBehaviour
{
   

    public bool isMuted;
    //Toggle Game Object
    public Toggle SesiAc;
    //Toggle Active mi?
    void start() {

        
        isMuted= PlayerPrefs.GetInt("MUTED")==1;
        AudioListener.pause = isMuted;
    }
    public void ToggleAktifmi()
    {
      
        if (SesiAc.isOn)
        {
            isMuted = false;
            AudioListener.pause = isMuted;
            PlayerPrefs.SetInt("MUTED", isMuted ? 1 : 0);
            Debug.Log("Aktif");
        }
        else
        {
            
            isMuted = true;
            AudioListener.pause = isMuted;
            PlayerPrefs.SetInt("MUTED",isMuted ? 1 : 0);
            Debug.Log("pasif");

        }
      
        
    }

   
}
