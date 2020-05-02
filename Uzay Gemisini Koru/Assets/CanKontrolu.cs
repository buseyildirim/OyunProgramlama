using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanKontrolu : MonoBehaviour
{
    public int can;
    private Text canMetnim;
    // Start is called before the first frame update
    private void Start()
    {
        canMetnim = GetComponent<Text>();
        canSifirla();
    }

    public void canAzalt(int eksilenCan)
    {
        can -= eksilenCan;
        canMetnim.text = can.ToString();
    }

    public void canSifirla()
    {
        can = 300;
        canMetnim.text = can.ToString();
    }
}
