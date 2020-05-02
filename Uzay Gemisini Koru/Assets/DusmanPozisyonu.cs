using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanPozisyonu : MonoBehaviour {

    private void OnDrawGizmos()
    {
        //Oyun alanında/başlatıldığın gözükmez ama sahne kısmında gözükür...
        //Gizmos objemizin görünüsünü görmemizi sağlamaktadır..
        //seçtiğimiz objeleri düzenlememizi kolaylaştırmaktadır.
        //Küre şeklinde belirliyoruz...
        //Oluşturduğumuz Gizmos'u prefab yapıp düsmanların düzeni kısmına
        //kaç adet düşman oluşturacaksak ona ekliyoruz..
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
