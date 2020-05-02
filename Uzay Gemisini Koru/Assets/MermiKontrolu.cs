using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermiKontrolu : MonoBehaviour {

    //Merminin bir zarar verme özelliği vardır. Bu özellikle düşmana her vurduğunda canını 10f kadar götürmesine yarıyor.
    public float verdigiZarar = 10f;

    public void CarptigindaYokOl()
    {
        //Mermi düşmana çarptığında merminin yok olmasını sağlıyorum. gameObject bu şekilde yazarsa benim içinde bulunduğum objeyi tanımlıyor.
        Destroy(gameObject);
    }

    public float ZararVerme()
    {
        return verdigiZarar;
    }
}
