using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mermileriYokEt : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //içinden geçebiliyorsa triggger özelliği vardır.Biz burada trigger özelliğini kullandık. Trigger gerçekleştiği anda yani bu alana mermi geldiği anda o objeyi yok et diyorum.
        Destroy(collision.gameObject);
    }
}
