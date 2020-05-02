using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DusmanSayiKontrolu : MonoBehaviour
{
    public int DusmanSayisi;
    private Text dusmanMetnim;

    // Start is called before the first frame update
    private void Start()
    {
        dusmanMetnim = GetComponent<Text>();

        dusmanSifirla();
    }

    public void dusmanAzalt(int eksilendusman)
    {
        DusmanSayisi -= eksilendusman;
        dusmanMetnim.text = DusmanSayisi.ToString();
    }

    public void dusmanSifirla()
    {
        DusmanSayisi = 30;
        dusmanMetnim.text = DusmanSayisi.ToString();
    }
}
