using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UzayGemimizinKontrolu : MonoBehaviour {
    //private SesKontrol seskontrol;
  
//Geminin hızı
    private float hiz = 10f;
    public float inceAyar = 0.7f;
    public GameObject Mermi;
    public float mermininHizi = 100f;
    public float atesEtmeAraligi = 2f;
    public float can = 300f;
    private CanKontrolu canKontrolu;

    //Uzay Gemisinin oyun alanında dışarı çıkmaması için belirlediğimiz değişkenler (Static)
    float xmin ;
    float xmax ;

    public AudioClip AtesSesi;
    public AudioClip Olumsesi;
    
	// Use this for initialization
	void Start () {
        canKontrolu = GameObject.Find("Can").GetComponent<CanKontrolu>();
        //seskontrol = GameObject.Find("SesKontrol").GetComponent<SesKontrol>();
       // bool pause = seskontrol.isMuted;
        //uzaklık değişkenini tanımlamımızın sebebi;
        //Öncelikle uzay gemisinin dinamik olarak ekran içerisindeki hareketini kısıtlayabilmek için kameranın görüntüsünü elde etmemiz gerekmektedir. 
        //Yani kameranın uzay gemisini z ekseninde görmesi lazım. Sonrasında uzay gemisinin z pozisyonundan kameranın pozisyonunu çıkardığımız zaman aradaki mesafeyi hesaplamış olduk..
        float uzaklik = transform.position.z - Camera.main.transform.position.z;

        //Kameranın sol-sağ uç değerlerini alıyoruz..
        //ViewportToWorldPoint görüntümüzü gerçek dünyaya aktarmak gibi..
        //Burada belirlediğimizde cetvel gibi düşün aralığı biz veriyoruz. (0 ile 1 arasında)
        //Fakat gerçek dünya ise yaklaşık -8 ile +8 gibi düşün...
        Vector3 solUc = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, uzaklik));
        Vector3 sagUc = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, uzaklik));
        
        //ince ayar eklememizin sebebi bu değeri eklemeden önce uzay gemisinin yarısı ekrandan çıkıyordu. Bunu önlemiş olduk.
        xmin = solUc.x + inceAyar;
        xmax = sagUc.x - inceAyar;


	}
	void AtesEtme()
    {
        //Bir tane gemi mermisi için gameObject yaratıyoruz. 
        GameObject gemimizinMermisi = Instantiate(Mermi, transform.position+ new Vector3(0,1f,0), Quaternion.identity) as GameObject;
        //Mermiye öncelikle bir rigidBody ekliyorum. Mermimin hareketi çin x ve z koordinatları benim için önemli olmadığı için onları 0 yaptım . Benim ihtiyacım olan y eksenidir.
        gemimizinMermisi.GetComponent<Rigidbody2D>().velocity = new Vector3(0, mermininHizi, 0);
        AudioSource.PlayClipAtPoint(AtesSesi, transform.position);
    }
	// Update is called once per frame
	void Update ()
    { //oyun içerisinde etki eden her şeyi update içerisinde tanımlıyoruz
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Input.GetKeyDown(KeyCode.Space yapmamın nedeni ben space bastığımda elimi kaldırmadan diğer mermiyi atmama izin vermemek içindir. Eğer GetKey yapsaydım elimi kaldırmadan otomatik birden fazla mermi yollayabilirdim.
            //InvokeRepeating ile  elimi space çektiğimde 0.00001 saniye merminin atmasını sağlıyor.Otomatik olarak kendiliğinden bu şekilde yaparsam mermiyi atıyor.
            InvokeRepeating("AtesEtme", 0.0000001f, atesEtmeAraligi);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            // InvokeRepeating fonksiyonunun iptal edilmesi için söylüyorum.Yani Space tuşundan elimi çektiğimde bitiriyorum ateş etmeyi kesiyor.
            CancelInvoke("AtesEtme");
        }
        // gemimizin x eksenindeki değeri eğer -8 ile 8 arasındaysa yeniX e ata.
        float yeniX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(yeniX, transform.position.y, transform.position.z);

        //Klavyeden alınan girdiyi almak için
        if (Input.GetKey(KeyCode.LeftArrow))
        {
             //Sol ok tuşuna bastığında objenin pozisyonunu değiştirmek için
             //- olmasının sebebi x-y koordinatına göre ayarlanmasıdır. Sol için -
             //Time.deltaTime : 2 Frame arasındaki süreyi bize gösterir..

             //Her sol tuşa basıldığında süre ile hızın çarpımıysa uzay gemisinin pozisyonunu değiştirmiş olduk... 
             //transform.position += new Vector3(-hiz*Time.deltaTime, 0, 0);
           
            
            // Vector3.left -> (-0.1,0,0)     *   10  *  0,016 -> (-0.16,0,0)
            //Her sola bastığımda uzay gemisinin pozisyonuna  (-0.16,0,0) 'lık bir vektör ekleniyor.. 
            transform.position += Vector3.left * hiz * Time.deltaTime;

        }
        //Klavyeden alınan girdiyi almak için

        else if (Input.GetKey(KeyCode.RightArrow))
        {
        //Sağ ok tuşuna bastığında objenin pozisyonunu değiştirmek için
        //+ olmasının sebebi x-y koordinatına göre ayarlanmasıdır. Sağ için +
        
        //Her sağ tuşa basıldığında süre ile hızın çarpımıysa uzay gemisinin pozisyonunu değiştirmiş olduk... 
        //transform.position += new Vector3(hiz * Time.deltaTime, 0, 0);

        //Vector3.right -> (1,0,0)     *   10  *  0,016 -> (0.16,0,0)
        //Her sağa bastığımda uzay gemisinin pozisyonuna  (0.16,0,0) 'lık bir vektör ekleniyor.. 

            transform.position += Vector3.right * hiz * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MermiKontrolu carpanMermi = collision.gameObject.GetComponent<MermiKontrolu>();
        if (carpanMermi)
        {
            carpanMermi.CarptigindaYokOl();
            can -= carpanMermi.ZararVerme();
            canKontrolu.canAzalt((int)carpanMermi.ZararVerme());
            if (can <= 0)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(Olumsesi, transform.position);
            }
        }

    }
}
