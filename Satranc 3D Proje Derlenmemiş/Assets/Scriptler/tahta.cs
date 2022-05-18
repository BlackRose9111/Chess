using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OzelHareket
{
    Yok = 0,
    EnPassant = 1,
    Rok= 2,
    PiyonYukseltme= 3


}


public class tahta : MonoBehaviour

    
{
    //Görsel Deðiþkenler
    [Header("Görsel Deðiþkenler")]

    [SerializeField] public Material kareMateryali;
    [SerializeField] public float KareBoyutu=1.0f;
    [SerializeField] public float Yoffset = 0.2f;
    [SerializeField] public Vector3 tahtaOrtasý = Vector3.zero;

    //UI Ýçin gerekli bileþenler

    [SerializeField] public GameObject zaferEkraný;

    [Header("Prefablar Ve Materyaller")]

    //Aþaðýdaki kod enum olarak atanan ID numaralarýna göre taþ spawnlamayý saðlýyor. Takým renginin belirlenmesi için de önemli.
    [SerializeField] public GameObject[] prefabs;
    [SerializeField] public Material[] takimMateryalleri;



    //MANTIKSAL KOD:
    //her sýrada kameranýn yönünü belirle.
    Vector3 kameraKonumuBeyaz = new Vector3(0, 52.2f, -50);
    Vector3 kameraYonuBeyaz = new Vector3(50,0,0);
    Vector3 kameraKonumuSiyah = new Vector3(0, 52.2f, 50);
    Vector3 kameraYonuSiyah = new Vector3(130, 0,180);

    private taslar[,] tahtadakiTaslar;
    //aþaðýdaki deðiþken, taþýnýp býrakýlan taþý tutar.
    private taslar AnlýkTasýnanTas;
    //Aþaðýdaki kod sanal tahta büyüklüðünü belirler. Bu deðerleri deðiþtirip daha büyük tahta elde edebilirsiniz ancak model deðiþmez.
    private const int KARE_X= 8;
    private const int KARE_Y= 8;
    //Kareler dizisi tüm karelerin indexlerini tutar.
    private GameObject[,] kareler;
    private Camera anlýkKamera;
    private Vector2Int anlýkHover;
    private Vector3 Limit;
    public bool BeyazSirasi;
    //Aþaðýdaki kozmetik için. Bunun yerine sadece taþý silebilirdik -Baran
    private List<taslar> yenmisSiyah = new List<taslar>();
    private List<taslar> yenmisBeyaz = new List<taslar>();
    [SerializeField] public float yenmeBoyutu = 1f;
    [SerializeField] public float UcusOffet = 3f;

    //Hangi hamleleri yapabiliriz?
    private List<Vector2Int> yapýlabilinenHamleler = new List<Vector2Int>();

    //Özel harektler En Passant, Rok ve Piyon yükseltme
    //Hareket listesi önceden yapýlan hareketleri tutar. Her taþ için ayrý tutulmasý lazým o yüzden List içinde array.
    private List<Vector2Int[]> HareketListesi = new List<Vector2Int[]>();
    private OzelHareket OzelHareket;


    //Awake, sahne baþlatýldýðýnda çalýþýr
    private void Awake()
    {
        BeyazSirasi = true;
        //Tüm karalerde taþ bulunabilir. Eðer bir karede taþ yoksa deðeri nulldur.
        TumKaraleriOlustur(KareBoyutu, KARE_X, KARE_Y);
        TumTaslarSpawn();
        TumTaslariKonumlandir();
        

        

    }
    //Update her frame de çalýþýr
    private void Update()
    {
        if (!anlýkKamera)
        {
            anlýkKamera = Camera.main;
            return;

        }
        //High light özelliði için, kameradan fare konumuna ray gönderiyoruz
        RaycastHit info;
        Ray ray = anlýkKamera.ScreenPointToRay(Input.mousePosition);
       

        if (Physics.Raycast(ray, out info, 100, LayerMask.GetMask("Hover","Kare","Highlight"))) {

            Vector2Int temasNoktasý = KareIndexBul(info.transform.gameObject);

            //Eðer daha önce bir karenin üzerinden geçmediysek.

            if (anlýkHover == -Vector2Int.one)
            {


                anlýkHover = temasNoktasý;
                kareler[temasNoktasý.x, temasNoktasý.y].layer = LayerMask.NameToLayer("Hover");
                //kareler[temasNoktasý.x, temasNoktasý.y].layer = LayerMask.NameToLayer("Hover");
                //bu farenin üstünde olduðu karenin katmanýný deðiþtiriyor, böylece highlight oluþturuyoruz.

                //update: Yukarýdaki geçici olarak deðiþti, diðer highlight efektini bozuyor.
            }


            //Eðer daha önce bir Karenin üstünden geçtyiysek 

            if (anlýkHover != -Vector2Int.one)
            {

                kareler[anlýkHover.x, anlýkHover.y].layer = (DogruHaraketVar(ref yapýlabilinenHamleler, anlýkHover)) ? LayerMask.NameToLayer("Highlight") : LayerMask.NameToLayer("Kare");
                anlýkHover = temasNoktasý;
                kareler[temasNoktasý.x, temasNoktasý.y].layer = LayerMask.NameToLayer("Hover");
              
            }

            //Eðer mouse týklandýysa
            if (Input.GetMouseButtonDown(0))
            {
                //eðer týkladýðýmýz yerde taþ varsa
                if (tahtadakiTaslar[temasNoktasý.x, temasNoktasý.y] !=null)
                {
                    //Sýra bizim mi? Beyaz( 0 indexli takým) için true, Siyah(1 indexli takým) için false.
                    if ((tahtadakiTaslar[temasNoktasý.x, temasNoktasý.y].takim == 0 && BeyazSirasi == true)|| (tahtadakiTaslar[temasNoktasý.x, temasNoktasý.y].takim == 1 && BeyazSirasi == false))
                    {

                        AnlýkTasýnanTas = tahtadakiTaslar[temasNoktasý.x, temasNoktasý.y];
                        //gidilebilecek konumlarýn listesini al ve o kareleri gridde belirginleþtir.
                        yapýlabilinenHamleler = AnlýkTasýnanTas.GidilebilecekKonumlarýBul(ref tahtadakiTaslar,KARE_X,KARE_Y);
                        //Yapýlabilinen özel hareketlerin listesini al. Bunun için tahtada bulunan taþlarýn arrayi, önceden yapýlmýþ hareketlerin listesini ve yapýlabilinen hamleler listesinin referanslarýný gönder.
                        OzelHareket = AnlýkTasýnanTas.OzelHareketlerGet(ref tahtadakiTaslar,ref HareketListesi, ref yapýlabilinenHamleler);

                        MatEngelle();

                        //Mat otomatik engelleme þimdilik baþarýsýz. Simülasyon çok karmaþýk, bir sürü bug vardý.

                        //Simulasyon kodu çýkarýlmýþ kod.txt içerisinde


                        KareBelirginlestir();

                    }

                }

            }
            //Eðer týklamayý býrkatýysak ve elimizde bir taþ varsa.
            if (AnlýkTasýnanTas!=null && Input.GetMouseButtonUp(0))
            {
                Vector2Int oncekiKonum = new Vector2Int(AnlýkTasýnanTas.AnlýkX,AnlýkTasýnanTas.AnlýkY);
                //Hareket yapýlabilir mi?
                bool dogruHaraket = TasýHareketEttir(AnlýkTasýnanTas,temasNoktasý.x,temasNoktasý.y);
                //Yapýlamazsa, taþý eski konumuna geri getir.
                if (!dogruHaraket)
                {
                    AnlýkTasýnanTas.KonumSet(kareOrtasýnýBul(oncekiKonum.x,oncekiKonum.y));
                    AnlýkTasýnanTas = null;

                    KareBelirginKaldýr();

                }
                else
                {
                    KareBelirginKaldýr();
                    AnlýkTasýnanTas = null;
                    //Referansý null yapmazsak, týkladýðýmýz her yere gitmeye çalýþýyor.

                }

            }

            //Eðer bir taþ elimizde varsa bunun görünmesini saðla. Bu çok karmaþýk -Baran
            //Bunun için, yatay bir düzlem oluþturup, taþý tuttuðumuz sürece düzlemde tut. 

            if (AnlýkTasýnanTas)
            {
                //Düzlemin yukarý baktýðýna emin ol. Üzerine ray düþür ve mesafeyi ölç.
                Plane Yatay = new Plane(Vector3.up,Vector3.up*Yoffset);
                float distance = 0.0f;
                if (Yatay.Raycast(ray, out distance))
                {

                    AnlýkTasýnanTas.KonumSet(ray.GetPoint(distance)+Vector3.up*UcusOffet);


                }
            }


        }
        //Eðer tahtanýn dýþýna çýkarsak hover efekti kaldýr
        else
        {   //Daha önce  bir karenin üzerinde deðilsek, gidilebilinecek hamle var mý kontrol et
           if (anlýkHover != -Vector2Int.one)
          {

              kareler[anlýkHover.x, anlýkHover.y].layer = (DogruHaraketVar(ref yapýlabilinenHamleler,anlýkHover)) ? LayerMask.NameToLayer("Highlight") : LayerMask.NameToLayer("Kare");
                anlýkHover = -Vector2Int.one;
            }

           //Tahtanýn dýþýna, elimizde bir taþ varken çýkarsak ve tuþtan parmaðýmýzý çekersek referansý kaldýr.
           //Böylece taþ bir sonraki týkladýðýmýz yere gitmesin. Ayrýca taþýn doðru konumda kaldýðýndan emin ol.

           if(AnlýkTasýnanTas && Input.GetMouseButtonUp(0))
            {
                AnlýkTasýnanTas.KonumSet(kareOrtasýnýBul(AnlýkTasýnanTas.AnlýkX, AnlýkTasýnanTas.AnlýkY));
                AnlýkTasýnanTas = null;
                

            }



        } //bu kodda hata vardý, sonradan düzelttim, -Baran


        //Kamera yönünü deðiþtir. Tarafa göre kameranýn bakýþ açýsý ve yönü deðiþsin.
        if (BeyazSirasi==true)
        {
            anlýkKamera.transform.position = kameraKonumuBeyaz;
            anlýkKamera.transform.eulerAngles = kameraYonuBeyaz;

        }
        if (BeyazSirasi==false)
        {
            anlýkKamera.transform.position = kameraKonumuSiyah;
            anlýkKamera.transform.eulerAngles = kameraYonuSiyah;

        }



    }

    private void TumKaraleriOlustur(float kareBoyutu,int KareSayisiX, int KareSayisiY)
    {

        Yoffset += transform.position.y;
        Limit = new Vector3((KareSayisiX / 2) * kareBoyutu, 0, (KareSayisiY / 2) * kareBoyutu) + tahtaOrtasý;

        kareler = new GameObject[KareSayisiX,KareSayisiY];
        for (int x = 0; x < KareSayisiX; x++)
        {
            for (int y = 0; y < KareSayisiY; y++)
            {
                kareler[x, y] = TekKareOlustur(kareBoyutu,x,y);
            }
        }

    }
    //Bu fonksiyon tek kare oluþturmayý saðlýyor. Ayrý ayrý kare oluþturulabildiði gibi, TumKaraleriOluþtur Fonksiyonu ile tüm tahta gridi oluþturulabilinir.
    private GameObject TekKareOlustur(float kareBoyutu, int x, int y)
    {
        GameObject kareObjesi = new GameObject(string.Format("X:{0}, Y:{1},",x,y));
        kareObjesi.transform.parent = transform;
        //Yukaridaki kod tüm tahtayý kapsayan objeyi her karenin atasý olarak atar.

        Mesh mesh = new Mesh();
        kareObjesi.AddComponent<MeshFilter>().mesh= mesh;
        kareObjesi.AddComponent<MeshRenderer>().material=kareMateryali;
        //Mesh renderi, karelerin görsel olarak oluþturulmasýný saðlar.

        Vector3[] koseler = new Vector3[4];
        koseler[0] = new Vector3(x*kareBoyutu,Yoffset,y*kareBoyutu)-Limit;
        koseler[1] = new Vector3(x*kareBoyutu, Yoffset, (y+1) *kareBoyutu)-Limit; 
        koseler[2] = new Vector3((x+1)*kareBoyutu, Yoffset, y *kareBoyutu)-Limit;
        koseler[3] = new Vector3((x+1)*kareBoyutu, Yoffset, (y+1) *kareBoyutu)-Limit;

        int[] tris = new int[] { 0,1,2,1,3,2};

        mesh.vertices = koseler;
        mesh.triangles = tris;
        mesh.RecalculateNormals();

        kareObjesi.AddComponent<BoxCollider>();
        kareObjesi.layer = LayerMask.NameToLayer("Kare");
        //görsel katman atamasý
        

        //Tahta geometrisi

        return kareObjesi;
    }

    //Operasyonlar

    //Özel Hareketler

    private void OzelHareketIncele()
    {
        //Özel hareket En Passant mý?

        switch (OzelHareket)
        {

            case OzelHareket.EnPassant:
                //En passant yapýldýktan sonra yenilen piyonun ortadan kaldýrýlmasý lazým.

                //Karþýlaþtýracaðýmýz hareketleri ve üzerinde iþlem yaptýðýmýz piyonu kolaylýk olsun diye deðiþken haline getirdik.
                var YeniHareket = HareketListesi[HareketListesi.Count - 1];
                taslar BizimPiyon = tahtadakiTaslar[YeniHareket[1].x, YeniHareket[1].y];
                var HedefPiyonKonum = HareketListesi[HareketListesi.Count - 2];
                taslar HedefPiyon = tahtadakiTaslar[HedefPiyonKonum[1].x, HedefPiyonKonum[1].y];

                //En passant hareketini yaptýysak hedefin üstündeyizdir. Eðer üstündeysek onu ortadan kaldýr.

                if (BizimPiyon.AnlýkX == HedefPiyon.AnlýkX) //Piyonlar ayný hizada mý?
                {
                    //Aþaðýdaki kontrolu muhtemelen yapmasak da olur ama yine de emin olmak istedim. -Baran

                    if (BizimPiyon.AnlýkY == HedefPiyon.AnlýkY + 1 || BizimPiyon.AnlýkY == HedefPiyon.AnlýkY - 1)
                    {
                        //Düþmanýn takýmý ne? Ona göre yeme hareketini gerçekleþtir.

                        switch (HedefPiyon.takim)
                        {
                            //Beyaz takým
                            case 0:

                                yenmisBeyaz.Add(HedefPiyon);
                                //Ölen taþý küçült ve tahtanýn kenarýna koy.
                                HedefPiyon.BoyutSet(Vector3.one * yenmeBoyutu, true);
                                HedefPiyon.KonumSet(new Vector3(8 * KareBoyutu, Yoffset, -1 * KareBoyutu) - Limit + new Vector3(KareBoyutu / 2, 0, KareBoyutu / 2) + (Vector3.forward * 2.5f) * yenmisBeyaz.Count);
                                //Bu kodu daha önceden yazdýk, aþaðýda taþý hareket ettir fonksiyonu içerisinde.
                                break;
                            //siyah takým:
                            case 1:

                                yenmisSiyah.Add(HedefPiyon);
                                HedefPiyon.BoyutSet(Vector3.one * yenmeBoyutu, true);
                                HedefPiyon.KonumSet(new Vector3(-1 * KareBoyutu, Yoffset, 8 * KareBoyutu) - Limit + new Vector3(KareBoyutu / 2, 0, KareBoyutu / 2) + (Vector3.back * 2.5f) * yenmisSiyah.Count);

                                break;
                        }

                        //aþaðýdakini yapmazsak, taþýn hayaleti o noktadan geçiþi engelliyor.
                        tahtadakiTaslar[HedefPiyon.AnlýkX, HedefPiyon.AnlýkY] = null;
                    }

                }

                break;
            //Rok hareketinin þartý kral üzerinde. Þah oynadýktan sonra bu fonksiyonda kalenin yerini deðiþtir.
            case OzelHareket.Rok:

                //Rok hangi tarafa yapýldý? Hangi kalenin hareket etmesi lazým? Hangi takým?
                //Kolaylýk olsun diye bazý deðerleri deðiþken içinde tutuyoruz.

                Vector2Int[] SonHareket = HareketListesi[HareketListesi.Count - 1];

                //Muhtemelen Y deðerini kontrol etmeye gerek yok.
                //Sol Rok
                if (SonHareket[1].x == 2) //&& (SonHareket[1].y == 0 || SonHareket[1].y == 7))
                {
                    //Beyaz rok
                    if (SonHareket[1].y == 0)
                    {
                        //üzerinde iþlem yaptýðýmýz kaleleri tutmamýz lazým. Aslýnda tutmasak da olur ama iþlemi daha anlaþýlýr yapýyor.
                        taslar kale = tahtadakiTaslar[0, 0];
                        //Bu kaleyi hareket ettir.
                        tahtadakiTaslar[3, 0] = kale;
                        TekTasKonumlandir(3, 0);

                        //önceki konumu temizle
                        tahtadakiTaslar[0, 0] = null;

                    }

                    //Siyah Rok

                    if (SonHareket[1].y == 7)
                    {
                        //üzerinde iþlem yaptýðýmýz kaleleri tutmamýz lazým. Aslýnda tutmasak da olur ama iþlemi daha anlaþýlýr yapýyor.
                        taslar kale = tahtadakiTaslar[0, 7];
                        //Bu kaleyi hareket ettir.
                        tahtadakiTaslar[3, 7] = kale;
                        TekTasKonumlandir(3, 7);

                        //önceki konumu temizle
                        tahtadakiTaslar[0, 7] = null;

                    }




                }

                //Sað Rok:

                else if (SonHareket[1].x == 6)
                {
                    //Beyaz rok
                    if (SonHareket[1].y == 0)
                    {
                        //üzerinde iþlem yaptýðýmýz kaleleri tutmamýz lazým. Aslýnda tutmasak da olur ama iþlemi daha anlaþýlýr yapýyor.
                        taslar kale = tahtadakiTaslar[7, 0];
                        //Bu kaleyi hareket ettir.
                        tahtadakiTaslar[5, 0] = kale;
                        TekTasKonumlandir(5, 0);

                        //önceki konumu temizle
                        tahtadakiTaslar[7, 0] = null;

                    }

                    //Siyah Rok

                    if (SonHareket[1].y == 7)
                    {
                        //üzerinde iþlem yaptýðýmýz kaleleri tutmamýz lazým. Aslýnda tutmasak da olur ama iþlemi daha anlaþýlýr yapýyor.
                        taslar kale = tahtadakiTaslar[7, 7];
                        //Bu kaleyi hareket ettir.
                        tahtadakiTaslar[5, 7] = kale;
                        TekTasKonumlandir(5, 7);

                        //önceki konumu temizle
                        tahtadakiTaslar[7, 7] = null;

                    }




                }

                break;

            case OzelHareket.PiyonYukseltme:

                //Eðer piyon son kareye ulaþýrsa, piyonu kraliçe yap. Þimdilik sadece Kraliçe -Baran

                var sonHareket = HareketListesi[HareketListesi.Count - 1];
                taslar piyon = tahtadakiTaslar[sonHareket[1].x, sonHareket[1].y];

                //muhtemelen bu kontrolu yapmaya gerek yok ama üzerinde iþlem yaptýðýmýz taþýn piyon olduðudan emin olmak istiyorum.
                if (piyon.tip == Tastipi.Piyon)
                {
                    //Beyaz piyon
                    //piyon beyaz mý ve son noktaya ulaþtý mý?
                    if (piyon.takim == 0 && sonHareket[1].y == 7)
                    {
                        //Yeni kralice spawn et
                        taslar Kralice = TekTasSpawn(Tastipi.Kralice, 0);
                        //önceki piyonu yok et
                        Destroy(tahtadakiTaslar[sonHareket[1].x, sonHareket[1].y].gameObject);
                        //Bu konumda Kraliçe olduðunu kaydet
                        tahtadakiTaslar[sonHareket[1].x, sonHareket[1].y] = Kralice;
                        //Tüm spawnlar fiziksel olarak tahtanýn ortasýnda. Unity koordinatlarý ile, oyun koordinatlarý arasýnda fark olduðu için, hareket etme fonksiyonu ile hareket ettir
                        TekTasKonumlandir(sonHareket[1].x, sonHareket[1].y, true);

                    }

                    //Siyah Piyon
                    if (piyon.takim == 1 && sonHareket[1].y == 0)
                    {
                        //Yeni kralice spawn et
                        taslar Kralice = TekTasSpawn(Tastipi.Kralice, 1);
                        //önceki piyonu yok et
                        Destroy(tahtadakiTaslar[sonHareket[1].x, sonHareket[1].y].gameObject);
                        //Bu konumda Kraliçe olduðunu kaydet
                        tahtadakiTaslar[sonHareket[1].x, sonHareket[1].y] = Kralice;
                        //Tüm spawnlar fiziksel olarak tahtanýn ortasýnda. Unity koordinatlarý ile, oyun koordinatlarý arasýnda fark olduðu için, hareket etme fonksiyonu ile hareket ettir
                        TekTasKonumlandir(sonHareket[1].x, sonHareket[1].y, true);

                    }


                }


                break;

            case OzelHareket.Yok:

                break;
        }


    }

    private void MatEngelle()
    {
        //Eðer Þahý tehlikeye atan bir hareket varsa bu harekete izin verme. Bunun için önceden tüm taþlarýn yapabileceði hareketleri þahýn konumu ile karþýlaþtýrmamýz gerekiyor

        taslar HedefKral = null;
        //Kral nerede?
        for (int x = 0; x < KARE_X; x++)
        {

            for (int y = 0; y < KARE_Y; y++)
            {


                 if((tahtadakiTaslar[x, y]!=null))       // hata, bunu kullanma if(tahtadakiTaslar[x, y].tip==Tastipi.Kral) 
                {
                    //Bulduðumuz þah bizim mi?

                    if (AnlýkTasýnanTas.takim == tahtadakiTaslar[x, y].takim)
                    {
                        //bizim kral bu koordinatlarda

                        HedefKral = tahtadakiTaslar[x, y];

                    }



                }
            }

        }
        //referans olarak göndermemizin sebebi, kralý tehlikeye atan adýmlarý yapýlabilinen hamlelerden çýkaracaðýz.
        //TekTasHareketSimuleEt(AnlýkTasýnanTas,ref yapýlabilinenHamleler,HedefKral);


    }
   

    //Hareket ettir

    private bool DogruHaraketVar(ref List<Vector2Int> hareketler,Vector2 pos)
    {
        //Yapýlabilen hareketleri belirlemek için kontrol ediyoruz, eski highlight sistemini deðiþtirmek için.

        //Eðer kontrol ettiðimiz yerde yapýlabilen bir hamle varsa highlight efektini bozma.

        //Ayrýca simulasyonda da kullanýlýyor. 
        for (int i = 0; i < hareketler.Count; i++)
        {
            if(hareketler[i].x==pos.x && hareketler[i].y == pos.y)
            {

                return true;
            }
        }
        
        //Bu fonksiyon, hamle tutan bir listedeki hamleleri istenilen konum ile karþýlaþtýrýr. Eðer istenen konum bu listede ise true dönderir.

        return false;

    }
    private bool TasýHareketEttir(taslar cp, int x, int y)
    {

        //Gitmek istediðimiz yer hareket kurallarýna uyuyor mu?

        if (!DogruHaraketVar(ref yapýlabilinenHamleler,new Vector2Int(x,y)))

        { return false; }

            //gittiðimiz yerde baþka bir taþ var mý?

            if (tahtadakiTaslar[x, y] != null)
        {
            taslar HedefTas = tahtadakiTaslar[x, y];
            //Eðer gitmeye çalýþtýðýmýz konumda bizim takýmdan taþ varsa bu konuma gidilemez
            if (cp.takim == HedefTas.takim)
            {
                return false;

            }
            switch (HedefTas.takim)
            {
                //hedef taþýn takýmý ne?
                case 0:
                    //beyaz indexi 0
                    //Eðer þah ölürse, oyun biter.
                    if (HedefTas.tip == Tastipi.Kral)
                    {
                        Mat(1);
                    }
                    yenmisBeyaz.Add(HedefTas);
                    //Ölen taþý küçült ve tahtanýn kenarýna koy.
                    HedefTas.BoyutSet(Vector3.one * yenmeBoyutu, true);
                    HedefTas.KonumSet(new Vector3(8 * KareBoyutu, Yoffset, -1 * KareBoyutu) - Limit + new Vector3(KareBoyutu / 2, 0, KareBoyutu / 2) + (Vector3.forward * 2.5f) * yenmisBeyaz.Count);
                    //Yukarýdakinde yeniden orta bulmakla uðraþmamýzýn sebebi grid'in dýþýnda bir konuma taþ gönderiyoruz. Aralýnda boþluk olduðundan emin ol. 0.5f~
                    break;

                case 1:
                    //Eðer þah ölürse, oyun biter.
                    if (HedefTas.tip == Tastipi.Kral)
                    {
                        Mat(0);
                    }
                    yenmisSiyah.Add(HedefTas);
                    HedefTas.BoyutSet(Vector3.one * yenmeBoyutu, true);
                    HedefTas.KonumSet(new Vector3(-1 * KareBoyutu, Yoffset, 8 * KareBoyutu) - Limit + new Vector3(KareBoyutu / 2, 0, KareBoyutu / 2) + (Vector3.back * 2.5f) * yenmisSiyah.Count);

                    break;

            }

            //Eðer gittiðimiz yerde düþman taþ varsa onu ye.

           



        }

        Vector2Int oncekiKonum = new Vector2Int(cp.AnlýkX,cp.AnlýkY);
        tahtadakiTaslar[x, y] = cp;
        tahtadakiTaslar[oncekiKonum.x, oncekiKonum.y] = null;
        TekTasKonumlandir(x, y);
        //Sýrayý ters çevir.
        BeyazSirasi = !BeyazSirasi;
        //Yapýlan hareketi hafýzada kaydet. 
        HareketListesi.Add(new Vector2Int[] { oncekiKonum, new Vector2Int(x, y) });

        //Özel hareket koþulunu incele:
        OzelHareketIncele();

        return true;
    }

    private Vector2Int KareIndexBul(GameObject hitInfo) {

        for (int x = 0; x < KARE_X; x++)
        {

            for (int y = 0; y < KARE_Y; y++)
            {

                if (kareler[x, y] == hitInfo)
                {

                    return new Vector2Int(x, y);
                }
               

            }
           

        }
        return -Vector2Int.one;  // x= -1 && y= -1


    }
    //Bu fonksiyon tek tas oluþturmayý saðlýyor. Ayrý ayrý tas oluþturulabildiði gibi, TumTasSpawn Fonksiyonu ile tüm tahta dolusu taþ oluþturulabilinir.
    private void TumTaslarSpawn()
    {
        tahtadakiTaslar = new taslar[KARE_X,KARE_Y];
        int beyaz = 0, siyah = 1;

        //beyaz takým baþlangýç yerleþtirmeleri

        tahtadakiTaslar[0, 0] = TekTasSpawn(Tastipi.Kale, beyaz);
        tahtadakiTaslar[1, 0] = TekTasSpawn(Tastipi.At, beyaz);
        tahtadakiTaslar[2, 0] = TekTasSpawn(Tastipi.Fil, beyaz);
        tahtadakiTaslar[3, 0] = TekTasSpawn(Tastipi.Kralice, beyaz);
        tahtadakiTaslar[4, 0] = TekTasSpawn(Tastipi.Kral, beyaz);
        tahtadakiTaslar[5, 0] = TekTasSpawn(Tastipi.Fil, beyaz);
        tahtadakiTaslar[6, 0] = TekTasSpawn(Tastipi.At, beyaz);
        tahtadakiTaslar[7, 0] = TekTasSpawn(Tastipi.Kale, beyaz);
        for (int x = 0; x < KARE_X; x++)
        {

            tahtadakiTaslar[x, 1] = TekTasSpawn(Tastipi.Piyon, beyaz);

        }
        //Siyah Takim
        tahtadakiTaslar[0, 7] = TekTasSpawn(Tastipi.Kale, siyah);
        tahtadakiTaslar[1, 7] = TekTasSpawn(Tastipi.At, siyah);
        tahtadakiTaslar[2, 7] = TekTasSpawn(Tastipi.Fil, siyah);
        tahtadakiTaslar[3, 7] = TekTasSpawn(Tastipi.Kralice, siyah);
        tahtadakiTaslar[4, 7] = TekTasSpawn(Tastipi.Kral, siyah);
        tahtadakiTaslar[5, 7] = TekTasSpawn(Tastipi.Fil, siyah);
        tahtadakiTaslar[6, 7] = TekTasSpawn(Tastipi.At, siyah);
        tahtadakiTaslar[7, 7] = TekTasSpawn(Tastipi.Kale, siyah);
        for (int x = 0; x < KARE_X; x++)
        {

            tahtadakiTaslar[x, 6] = TekTasSpawn(Tastipi.Piyon, siyah);

        }

    }

    private taslar TekTasSpawn(Tastipi tip,int takim) {



        //instantiate obje yaratan unity codu. Önceden belirlenmiþ prefablarý yaratýr.
        taslar tas = Instantiate(prefabs[(int) tip-1],transform).GetComponent<taslar>();

        tas.tip = tip;
        tas.takim = takim;
        tas.GetComponent<MeshRenderer>().material = takimMateryalleri[takim];
        //Siyahlarý ters döndür yoksa yanlýþ tarafa bakýyorlar.
        if (tas.takim == 1)
        {
            tas.transform.Rotate(0,0,180f);
        }

        return tas;
    }


    private void TumTaslariKonumlandir() 
    { //bu fonksiyon oyun baþladýðýnda tüm taþlarýn yerinde olmasýný tek taþ konumlandýrma fonksiyonu ile saðlar. Tek taþ konumlandýrma
      //bu fonksiyonun dýþýnda taþlarý hareket ettirmek için kullnaýlýr. bool false veya true olmasý bu taþlarýn nasýl hareket edeceðini belirler.

        for (int x = 0; x < KARE_X; x++)
        {

            for (int y = 0; y < KARE_Y; y++)
            {
                if (tahtadakiTaslar[x, y] != null)
                {

                    TekTasKonumlandir(x, y, true);
                }

            }

        }
    
    
    }

    private void TekTasKonumlandir(int x, int y, bool force = false) 
    {
        tahtadakiTaslar[x, y].AnlýkX = x;
        tahtadakiTaslar[x, y].AnlýkY = y;
        tahtadakiTaslar[x, y].KonumSet(kareOrtasýnýBul(x,y),force);
    
    }

    private Vector3 kareOrtasýnýBul(int x,int y)
    {
        //bu kod taþlarýn karenin ortasýnda olmasýný saðlýyor, yoksa sadece köþelere gidiyorlar
        //bunu fonksiyon yapmasak da olurdu, ileride lazým olur belki
        return new Vector3(x * KareBoyutu, Yoffset, y * KareBoyutu) - Limit + new Vector3(KareBoyutu/2,0,KareBoyutu/2);


    }


    //Yapýlabilen tüm hareketleri belirginleþtiren fonksiyonlar:

    private void KareBelirginlestir()
    {
        for (int i = 0; i < yapýlabilinenHamleler.Count; i++)
        {
            kareler[yapýlabilinenHamleler[i].x, yapýlabilinenHamleler[i].y].layer = LayerMask.NameToLayer("Highlight");
            //hoverdaki gibi, katmanlar render öncelliðini ve nasýl render yapýlacaðýný belirlemek için kullanýlýr.
            
        }

    }
    private void KareBelirginKaldýr()
    {
        for (int i = 0; i < yapýlabilinenHamleler.Count; i++)
        {
            kareler[yapýlabilinenHamleler[i].x, yapýlabilinenHamleler[i].y].layer = LayerMask.NameToLayer("Kare");
            //hoverdaki gibi, katmanlar render öncelliðini ve nasýl render yapýlacaðýný belirlemek için kullanýlýr.
            //Yukarýdakinin aynýsý, önceki highlightlarý sil.
           

        }
        yapýlabilinenHamleler.Clear();

    }


    //Þah Mat

    private void Mat(int KazananTakim)
    {

        ZaferGoster(KazananTakim);


    }
    private void ZaferGoster(int KazananTakim)
    {

        zaferEkraný.SetActive(true);
        //Doðru takýmýn zafer yazýsýnýn görünmesi için indexi beyaz için(0), siyah için(1) olmasý gerekir.
        //Bu düzen editörden ayarlanmýþtýr.
        zaferEkraný.transform.GetChild(KazananTakim).gameObject.SetActive(true);


    }

    //Çýk veya sýfýrla düðmeleri

    public void SifirlaDugmesi()
    {
        //Bu düðmeye basýldýðýnda oyun baþa dönsün.

        //UI
        zaferEkraný.SetActive(false);
        zaferEkraný.transform.GetChild(0).gameObject.SetActive(false);
        zaferEkraný.transform.GetChild(1).gameObject.SetActive(false);

        //Bazen yanlýþlýkla, hýzlý basarken elde taþ kalabiliyor bu yüzden ne olur ne olmaz diye null attým -Baran
        AnlýkTasýnanTas = null;
        yapýlabilinenHamleler.Clear();
        //Özel hareketlerin temizlenmesi bir sonraki oyun için önemli!
        HareketListesi.Clear();



        //temizlik

        //Tüm kareleri temizle
        for (int x = 0; x < KARE_X; x++)
        {

            for (int y = 0; y < KARE_Y; y++)
            {
                //Eðer taranan yerde bir taþ varsa, taþý sil.
                if (tahtadakiTaslar[x, y] != null)
                {
                    //taþý yok et
                    Destroy(tahtadakiTaslar[x, y].gameObject);

                    //bu koordinatlarda artýk taþ yok.

                    tahtadakiTaslar[x, y] = null;
                }
                //bu sadece hayatta kalan taþlarý temizler, bir de ölen taþlar var.
                        

            }

            for (int i = 0; i < yenmisBeyaz.Count; i++)
            {
                //yenmiþ beyazlarý sil.
                Destroy(yenmisBeyaz[i].gameObject);

            }
            for (int i = 0; i < yenmisSiyah.Count; i++)
            {
                //yenmiþ beyazlarý sil.
                Destroy(yenmisSiyah[i].gameObject);

            }

            //listeleri temizle
            yenmisBeyaz.Clear();
            yenmisSiyah.Clear();
        }

        BeyazSirasi = true;
        //Tüm karalerde taþ bulunabilir. Eðer bir karede taþ yoksa deðeri nulldur.
        //Awake();
        //Awake çaðýrmayý denedim ama yeniden grid oluþturduðu için bir sürü hata oluþtu. Sadece taþ spawn ve taþ konum lazým

        TumTaslarSpawn();
        TumTaslariKonumlandir();
    }
    public void CikisDugmesi() 
    {


        //Oyundan çýk

        Application.Quit();

    
    }


}

