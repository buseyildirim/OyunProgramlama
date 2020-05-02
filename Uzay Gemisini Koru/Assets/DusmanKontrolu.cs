using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanKontrolu : MonoBehaviour {
    //Düşman kontrolu scriptine mermiyi ilave ettik.
    public GameObject mermi;
    public float mermiHizi = 8f;//Merminin hızını ayarlıyorum.
    public float can = 100f;
    public float saniyeBasinaMermiAtma = 0.6f;//Surekli mermi akmasın diye saniye başına mermi atma değeri
    public int skorDegeri = 200;
    private SkorKontrolu skorKontrolu;
    private DusmanSayiKontrolu dusmanKontrolu;
    private int eskilendusmanSayisi = 0;

    public AudioClip AtesSesi;
    public AudioClip OlumSesi;

    private void Start()
    {
        skorKontrolu = GameObject.Find("Skor").GetComponent<SkorKontrolu>();
        dusmanKontrolu = GameObject.Find("DusmanSayisi").GetComponent<DusmanSayiKontrolu>();
    }
    //OLASILIK  0,1   0-> false 1->true   (0,1) - > olasılık   0.99 - > %99
    //Düşman gemiyi vurdugunda gerçekleşecek işlemler için;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision gerçekleştiği anda o collision olan objeyi alabilmek için yaptık.
        MermiKontrolu carpanMermi = collision.gameObject.GetComponent<MermiKontrolu>();
        if (carpanMermi)
        {
            //mermi düşmana çarptıysa düşmanın canını azalttık ve çarpan mermiyi de yok ettik.
            carpanMermi.CarptigindaYokOl();
            can -= carpanMermi.ZararVerme();
            if (can <= 0)
            {
                //canı biterse düşmanın o objeyi yok ediyorum destroy methodu ile
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(OlumSesi, transform.position);
                skorKontrolu.SkoruArttir(skorDegeri);
                eskilendusmanSayisi++;
                dusmanKontrolu.dusmanAzalt(eskilendusmanSayisi);
            }
        }
        
    }
    
	
	// Update is called once per frame
	void Update () {
        //0,016*0.6 = 0,0096
        //Düşmanın mermiyi belli aralıklarla atması için;
        float atmaOlasiligi = Time.deltaTime * saniyeBasinaMermiAtma;
        if(Random.value < atmaOlasiligi)
        {
            //Düşmanın ateş etmesi için olan fonksiyon burada çağrılır.
            AtesEt();

        }
       
	}

    void AtesEt()
    {
        //Merminin düşmanın içinden çıkması için mermiyi başlattık.
        Vector3 baslangicPozisyonu = transform.position + new Vector3(0, -0.8f, 0);
        GameObject dusmaninMermisi = Instantiate(mermi, baslangicPozisyonu, Quaternion.identity) as GameObject;
        //Düşmanın mermisine hız ataak için rigid body özelliğine hız atadık.
        dusmaninMermisi.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -mermiHizi);
        AudioSource.PlayClipAtPoint(AtesSesi, transform.position);
    }
}
