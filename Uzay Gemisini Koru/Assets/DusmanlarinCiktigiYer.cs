using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanlarinCiktigiYer : MonoBehaviour {

    //Prefab'ın içerisinndeki düşmanı oyun alanında görebilmek içindir..
    public GameObject dusmanPrefabi;
    public float genislik; //düşmanların hepsini belli bir küme içine almak için genişlik ve yükseklik değeri belirliyorum
    public float yukseklik; //unity'nin kendi ara yüzüde değerleri görebilmek için public yaptık
    private float hiz = 5f;
    private bool SagaHareket = true; //sağa ve sola gidişte bunun kontrolünü yapabilmek için sagaHareketi true yapıyorum. Sağa çarptığında false olacak ve böylelikle sola gidecek.
    private float xmax;
    private float xmin;
    private int dusmanSayisi = 0;
    public float yaratmayiGeciktirmeSuresi = 0.7f;//Düşmanları 0.7 saniye arayla oluşturacak.

    // Use this for initialization

    void Start () {

        // z değerini belirliyorum çünkü kameramı 3 boyutlu olarak döndürdüğümde uzay gemimizi göremeyiz diğer türlü. Kameramı geriye alıyorum bu yüzden.
        float objeIleKameraninZsininfarki = transform.position.z - Camera.main.transform.position.z;
        //Kameramın sol tarafını tutması için bu değişkeni belirliyorum. Bunu yaparken kameramın ViewportToWorldPoint fonksiyonunu kullanarak yapıyorum. Ekranın solu 0'dır ve onu tutuyorum. 
        Vector3 kameraninSolTarafi = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, objeIleKameraninZsininfarki));
       //Kameranın sağ tarafını da aynı işlemleri yaparak tutuyorum. Ekranı [0,1] arasında tutarak ölçeklendiriyorum.
        Vector3 kameraninSagTarafi = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, objeIleKameraninZsininfarki));
        //Kameranın yerini xmax ve xmin olarak tutuyorum
        xmax = kameraninSagTarafi.x;
        xmin = kameraninSolTarafi.x;
        //Başlangıçta düşmanları yaratmak için Düşmanları yarattığımız fonksiyonu çağırırız.
        DusmanlarinTekTekYaratilmasi(); 

    }

    //Bu fonksiyonu yazmamızın sebebi 6 adet oluşturduğumuz child'lara erişmek içindir...
    //Yani, Düşmanların Düzeni altında bulunun childlara erişebilmem içindir..
    void DusmanlarinYaratilmasi()
    {
        //Düşmanların Düzeni içerisindeki çocukları çağırıyoruz..
        foreach (Transform cocuk in transform)
        {
            //Instantiate () : Yaratma fonksiyonudur. Bu fonksiyon sayesinde kullanmak istediğimiz GameObject'i oyun alanında oluşturmak/görmektir.
            //Burada as GameObject kullanmamızın sebebi, dusmnaPrefabını GameObject gibi yarat demektir..
            GameObject dusman = Instantiate(dusmanPrefabi, cocuk.transform.position, Quaternion.identity) as GameObject;
            
            //Objelerin Child-Parent ilişkisini transform ile sağlayabiliriz..
            //Oluşan düşmanın parent'ını belirlemek için...
            dusman.transform.parent = cocuk;
        }
    }
    //Düşmanları tek tek yaratan fonksiyon.
    void DusmanlarinTekTekYaratilmasi()
    {
        //Düşmanın Uygun pozisyonu bulmamız lazım.Çünkü ölen düşmanın pozisyonu artık uygun olacak.

        Transform uygunPozisyon = SonrakiUygunPozisyon();
        //Uygun Pozisyon varsa o pozisyona yeni düşman oluşturulur.
        if (dusmanSayisi < 30)
        {
            if (uygunPozisyon)
            {

                GameObject dusman = Instantiate(dusmanPrefabi, uygunPozisyon.transform.position, Quaternion.identity) as GameObject;
                dusman.transform.parent = uygunPozisyon;
                dusmanSayisi++;
            }

        }

        //Düşmanların yaratmayiGeciktirmeSuresi olarak belirlenen saniye arayla oluşması için.
        //Yani sonraki uygun pozisyon varsa bana 0,7 saniye arayla Düşmanların Tek Tek yaratılmasını fonksiyonunu çağır.
        //Bu saniyeyi sıfır yaparsam DüşmanlarınYaratılması fonksiyonu ile aynı olur.Yani hepsini aynı anda oluşturur.
        if (SonrakiUygunPozisyon())
        {
            Invoke("DusmanlarinTekTekYaratilmasi", yaratmayiGeciktirmeSuresi);

        }

    }
    public void OnDrawGizmos() 
    {
        //oyun başladığında değilde sahne ekranına geldiğinde kümenin görünmesini sağlıyoruz
        Gizmos.DrawWireCube(transform.position, new Vector3(genislik, yukseklik));
    }
    // Update is called once per frame
    void Update () { 
        //Eğer düşmanların hareketi sağa doğru ise buna göre position belirliyorum
        if (SagaHareket)
        {
            transform.position += hiz * Vector3.right * Time.deltaTime;
        }
        else
        {
            //  hiz * Vector3.left * Time.deltaTime yapmamın nedeni sola hareket ederken çok hızlı hareket edince ekranımdan çıkmasın ve ben ekranda düşmanları görebileyim diye
            transform.position += hiz * Vector3.left * Time.deltaTime;
        }

        //Sağ sınır belirliyorum çünkü düşmanlarım sağa geldiğinde sola gitmesini sağlayacağım. Position benim düşmanlarımın tam ortasını işaret ettiği için total genişliği iki böldüğümde düşmanlarım tam sağ sınıra gelmiş olacak.
        float sagSinir = transform.position.x +  genislik/2;
       // Aynı işlemleri sol sınır için de yapmam gerekiyor. Sadece bu sefer genişliğin yarısını çıkartıyorum. Çünkü sol tarafa ilerlemesi demek orta noktadan çıkarmak demektir.
        float solSinir = transform.position.x - genislik / 2;

        if( sagSinir>xmax )
        {
            //Sağ sınırım xmax geçtiği anda sağa hareketi false yapıyoruz. Böylelikle sola hareket edecek.
            SagaHareket = false;
          
        }
        else if(solSinir < xmin)
        {
            //Sol hareket belirlediğim min değerinden küçükse sağa hareketi true yapacak ve böylelikle sağa hareket edecek.
            SagaHareket = true;
        }
        //Bütün düşmanlar öldü true döndürürse yani düşmanların hepsi öldüyse düşmanları tek tek yarattığımız fonksiyonu burda tekrar çağırırız.
        if (ButunDusmanlarOlduMu())
        {

            DusmanlarinTekTekYaratilmasi();
            //DusmanlarinYaratilmasi();

        }

    }
    //Önceden düşmanların başlangıçta direk 6 tane çıkmasını sağlamıştık.Şimdi ise ekranda tek tek çıkmasını istiyoruz.
    //Ölen düşmanın pozisyonunu tutarak yeni düşmanı o pozisyonda oluşturacağız.
    Transform SonrakiUygunPozisyon()
    {
        //Ölrn düşmanın pozisyonunu tutup döndürcek.
        foreach (Transform CocuklarinPozisyonu in transform)
        {
            if (CocuklarinPozisyonu.childCount == 0)
            {
                return CocuklarinPozisyonu;
            }
        }
        //Eğer düşman ölmediyse hiç birşey yapmasın.
        return null;
    }
    //6 tane düşman var.Bu düşmanların hepsinin ölüp ölmediğini kontrol etmek için;
    bool ButunDusmanlarOlduMu()
    {
        //Düşman pozisyonu Düşmanların Düzeninin çocugudur. obje parent-child diye transform ile bağlanır.
        //in transform :düşmanların çıktığı yer scriptinin ait olduğu objenin(Düşmanların düzeni) transformu
        //Aslında ulaşmaya çalıştığım Düşmanın Pozisyonun çocuğu olan Düşman.Yani Düşmanlarının düzeninin çocuğunun çocuğu.
        foreach(Transform CocuklarinPozisyonu in transform)
        {
            //Düşman pozisyonu altında herhangi bir pozisyonda Düşman varsa yani child count>0 ise bütün düşmanlar ölmemiştir demektir.
            if (CocuklarinPozisyonu.childCount > 0)
            {
                return false;
            }
        }

        return true;
    }
}
