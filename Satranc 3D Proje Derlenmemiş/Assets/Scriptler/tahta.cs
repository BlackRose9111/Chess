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
    //G�rsel De�i�kenler
    [Header("G�rsel De�i�kenler")]

    [SerializeField] public Material kareMateryali;
    [SerializeField] public float KareBoyutu=1.0f;
    [SerializeField] public float Yoffset = 0.2f;
    [SerializeField] public Vector3 tahtaOrtas� = Vector3.zero;

    //UI ��in gerekli bile�enler

    [SerializeField] public GameObject zaferEkran�;

    [Header("Prefablar Ve Materyaller")]

    //A�a��daki kod enum olarak atanan ID numaralar�na g�re ta� spawnlamay� sa�l�yor. Tak�m renginin belirlenmesi i�in de �nemli.
    [SerializeField] public GameObject[] prefabs;
    [SerializeField] public Material[] takimMateryalleri;



    //MANTIKSAL KOD:
    //her s�rada kameran�n y�n�n� belirle.
    Vector3 kameraKonumuBeyaz = new Vector3(0, 52.2f, -50);
    Vector3 kameraYonuBeyaz = new Vector3(50,0,0);
    Vector3 kameraKonumuSiyah = new Vector3(0, 52.2f, 50);
    Vector3 kameraYonuSiyah = new Vector3(130, 0,180);

    private taslar[,] tahtadakiTaslar;
    //a�a��daki de�i�ken, ta��n�p b�rak�lan ta�� tutar.
    private taslar Anl�kTas�nanTas;
    //A�a��daki kod sanal tahta b�y�kl���n� belirler. Bu de�erleri de�i�tirip daha b�y�k tahta elde edebilirsiniz ancak model de�i�mez.
    private const int KARE_X= 8;
    private const int KARE_Y= 8;
    //Kareler dizisi t�m karelerin indexlerini tutar.
    private GameObject[,] kareler;
    private Camera anl�kKamera;
    private Vector2Int anl�kHover;
    private Vector3 Limit;
    public bool BeyazSirasi;
    //A�a��daki kozmetik i�in. Bunun yerine sadece ta�� silebilirdik -Baran
    private List<taslar> yenmisSiyah = new List<taslar>();
    private List<taslar> yenmisBeyaz = new List<taslar>();
    [SerializeField] public float yenmeBoyutu = 1f;
    [SerializeField] public float UcusOffet = 3f;

    //Hangi hamleleri yapabiliriz?
    private List<Vector2Int> yap�labilinenHamleler = new List<Vector2Int>();

    //�zel harektler En Passant, Rok ve Piyon y�kseltme
    //Hareket listesi �nceden yap�lan hareketleri tutar. Her ta� i�in ayr� tutulmas� laz�m o y�zden List i�inde array.
    private List<Vector2Int[]> HareketListesi = new List<Vector2Int[]>();
    private OzelHareket OzelHareket;


    //Awake, sahne ba�lat�ld���nda �al���r
    private void Awake()
    {
        BeyazSirasi = true;
        //T�m karalerde ta� bulunabilir. E�er bir karede ta� yoksa de�eri nulldur.
        TumKaraleriOlustur(KareBoyutu, KARE_X, KARE_Y);
        TumTaslarSpawn();
        TumTaslariKonumlandir();
        

        

    }
    //Update her frame de �al���r
    private void Update()
    {
        if (!anl�kKamera)
        {
            anl�kKamera = Camera.main;
            return;

        }
        //High light �zelli�i i�in, kameradan fare konumuna ray g�nderiyoruz
        RaycastHit info;
        Ray ray = anl�kKamera.ScreenPointToRay(Input.mousePosition);
       

        if (Physics.Raycast(ray, out info, 100, LayerMask.GetMask("Hover","Kare","Highlight"))) {

            Vector2Int temasNoktas� = KareIndexBul(info.transform.gameObject);

            //E�er daha �nce bir karenin �zerinden ge�mediysek.

            if (anl�kHover == -Vector2Int.one)
            {


                anl�kHover = temasNoktas�;
                kareler[temasNoktas�.x, temasNoktas�.y].layer = LayerMask.NameToLayer("Hover");
                //kareler[temasNoktas�.x, temasNoktas�.y].layer = LayerMask.NameToLayer("Hover");
                //bu farenin �st�nde oldu�u karenin katman�n� de�i�tiriyor, b�ylece highlight olu�turuyoruz.

                //update: Yukar�daki ge�ici olarak de�i�ti, di�er highlight efektini bozuyor.
            }


            //E�er daha �nce bir Karenin �st�nden ge�tyiysek 

            if (anl�kHover != -Vector2Int.one)
            {

                kareler[anl�kHover.x, anl�kHover.y].layer = (DogruHaraketVar(ref yap�labilinenHamleler, anl�kHover)) ? LayerMask.NameToLayer("Highlight") : LayerMask.NameToLayer("Kare");
                anl�kHover = temasNoktas�;
                kareler[temasNoktas�.x, temasNoktas�.y].layer = LayerMask.NameToLayer("Hover");
              
            }

            //E�er mouse t�kland�ysa
            if (Input.GetMouseButtonDown(0))
            {
                //e�er t�klad���m�z yerde ta� varsa
                if (tahtadakiTaslar[temasNoktas�.x, temasNoktas�.y] !=null)
                {
                    //S�ra bizim mi? Beyaz( 0 indexli tak�m) i�in true, Siyah(1 indexli tak�m) i�in false.
                    if ((tahtadakiTaslar[temasNoktas�.x, temasNoktas�.y].takim == 0 && BeyazSirasi == true)|| (tahtadakiTaslar[temasNoktas�.x, temasNoktas�.y].takim == 1 && BeyazSirasi == false))
                    {

                        Anl�kTas�nanTas = tahtadakiTaslar[temasNoktas�.x, temasNoktas�.y];
                        //gidilebilecek konumlar�n listesini al ve o kareleri gridde belirginle�tir.
                        yap�labilinenHamleler = Anl�kTas�nanTas.GidilebilecekKonumlar�Bul(ref tahtadakiTaslar,KARE_X,KARE_Y);
                        //Yap�labilinen �zel hareketlerin listesini al. Bunun i�in tahtada bulunan ta�lar�n arrayi, �nceden yap�lm�� hareketlerin listesini ve yap�labilinen hamleler listesinin referanslar�n� g�nder.
                        OzelHareket = Anl�kTas�nanTas.OzelHareketlerGet(ref tahtadakiTaslar,ref HareketListesi, ref yap�labilinenHamleler);

                        MatEngelle();

                        //Mat otomatik engelleme �imdilik ba�ar�s�z. Sim�lasyon �ok karma��k, bir s�r� bug vard�.

                        //Simulasyon kodu ��kar�lm�� kod.txt i�erisinde


                        KareBelirginlestir();

                    }

                }

            }
            //E�er t�klamay� b�rkat�ysak ve elimizde bir ta� varsa.
            if (Anl�kTas�nanTas!=null && Input.GetMouseButtonUp(0))
            {
                Vector2Int oncekiKonum = new Vector2Int(Anl�kTas�nanTas.Anl�kX,Anl�kTas�nanTas.Anl�kY);
                //Hareket yap�labilir mi?
                bool dogruHaraket = Tas�HareketEttir(Anl�kTas�nanTas,temasNoktas�.x,temasNoktas�.y);
                //Yap�lamazsa, ta�� eski konumuna geri getir.
                if (!dogruHaraket)
                {
                    Anl�kTas�nanTas.KonumSet(kareOrtas�n�Bul(oncekiKonum.x,oncekiKonum.y));
                    Anl�kTas�nanTas = null;

                    KareBelirginKald�r();

                }
                else
                {
                    KareBelirginKald�r();
                    Anl�kTas�nanTas = null;
                    //Referans� null yapmazsak, t�klad���m�z her yere gitmeye �al���yor.

                }

            }

            //E�er bir ta� elimizde varsa bunun g�r�nmesini sa�la. Bu �ok karma��k -Baran
            //Bunun i�in, yatay bir d�zlem olu�turup, ta�� tuttu�umuz s�rece d�zlemde tut. 

            if (Anl�kTas�nanTas)
            {
                //D�zlemin yukar� bakt���na emin ol. �zerine ray d���r ve mesafeyi �l�.
                Plane Yatay = new Plane(Vector3.up,Vector3.up*Yoffset);
                float distance = 0.0f;
                if (Yatay.Raycast(ray, out distance))
                {

                    Anl�kTas�nanTas.KonumSet(ray.GetPoint(distance)+Vector3.up*UcusOffet);


                }
            }


        }
        //E�er tahtan�n d���na ��karsak hover efekti kald�r
        else
        {   //Daha �nce  bir karenin �zerinde de�ilsek, gidilebilinecek hamle var m� kontrol et
           if (anl�kHover != -Vector2Int.one)
          {

              kareler[anl�kHover.x, anl�kHover.y].layer = (DogruHaraketVar(ref yap�labilinenHamleler,anl�kHover)) ? LayerMask.NameToLayer("Highlight") : LayerMask.NameToLayer("Kare");
                anl�kHover = -Vector2Int.one;
            }

           //Tahtan�n d���na, elimizde bir ta� varken ��karsak ve tu�tan parma��m�z� �ekersek referans� kald�r.
           //B�ylece ta� bir sonraki t�klad���m�z yere gitmesin. Ayr�ca ta��n do�ru konumda kald���ndan emin ol.

           if(Anl�kTas�nanTas && Input.GetMouseButtonUp(0))
            {
                Anl�kTas�nanTas.KonumSet(kareOrtas�n�Bul(Anl�kTas�nanTas.Anl�kX, Anl�kTas�nanTas.Anl�kY));
                Anl�kTas�nanTas = null;
                

            }



        } //bu kodda hata vard�, sonradan d�zelttim, -Baran


        //Kamera y�n�n� de�i�tir. Tarafa g�re kameran�n bak�� a��s� ve y�n� de�i�sin.
        if (BeyazSirasi==true)
        {
            anl�kKamera.transform.position = kameraKonumuBeyaz;
            anl�kKamera.transform.eulerAngles = kameraYonuBeyaz;

        }
        if (BeyazSirasi==false)
        {
            anl�kKamera.transform.position = kameraKonumuSiyah;
            anl�kKamera.transform.eulerAngles = kameraYonuSiyah;

        }



    }

    private void TumKaraleriOlustur(float kareBoyutu,int KareSayisiX, int KareSayisiY)
    {

        Yoffset += transform.position.y;
        Limit = new Vector3((KareSayisiX / 2) * kareBoyutu, 0, (KareSayisiY / 2) * kareBoyutu) + tahtaOrtas�;

        kareler = new GameObject[KareSayisiX,KareSayisiY];
        for (int x = 0; x < KareSayisiX; x++)
        {
            for (int y = 0; y < KareSayisiY; y++)
            {
                kareler[x, y] = TekKareOlustur(kareBoyutu,x,y);
            }
        }

    }
    //Bu fonksiyon tek kare olu�turmay� sa�l�yor. Ayr� ayr� kare olu�turulabildi�i gibi, TumKaraleriOlu�tur Fonksiyonu ile t�m tahta gridi olu�turulabilinir.
    private GameObject TekKareOlustur(float kareBoyutu, int x, int y)
    {
        GameObject kareObjesi = new GameObject(string.Format("X:{0}, Y:{1},",x,y));
        kareObjesi.transform.parent = transform;
        //Yukaridaki kod t�m tahtay� kapsayan objeyi her karenin atas� olarak atar.

        Mesh mesh = new Mesh();
        kareObjesi.AddComponent<MeshFilter>().mesh= mesh;
        kareObjesi.AddComponent<MeshRenderer>().material=kareMateryali;
        //Mesh renderi, karelerin g�rsel olarak olu�turulmas�n� sa�lar.

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
        //g�rsel katman atamas�
        

        //Tahta geometrisi

        return kareObjesi;
    }

    //Operasyonlar

    //�zel Hareketler

    private void OzelHareketIncele()
    {
        //�zel hareket En Passant m�?

        switch (OzelHareket)
        {

            case OzelHareket.EnPassant:
                //En passant yap�ld�ktan sonra yenilen piyonun ortadan kald�r�lmas� laz�m.

                //Kar��la�t�raca��m�z hareketleri ve �zerinde i�lem yapt���m�z piyonu kolayl�k olsun diye de�i�ken haline getirdik.
                var YeniHareket = HareketListesi[HareketListesi.Count - 1];
                taslar BizimPiyon = tahtadakiTaslar[YeniHareket[1].x, YeniHareket[1].y];
                var HedefPiyonKonum = HareketListesi[HareketListesi.Count - 2];
                taslar HedefPiyon = tahtadakiTaslar[HedefPiyonKonum[1].x, HedefPiyonKonum[1].y];

                //En passant hareketini yapt�ysak hedefin �st�ndeyizdir. E�er �st�ndeysek onu ortadan kald�r.

                if (BizimPiyon.Anl�kX == HedefPiyon.Anl�kX) //Piyonlar ayn� hizada m�?
                {
                    //A�a��daki kontrolu muhtemelen yapmasak da olur ama yine de emin olmak istedim. -Baran

                    if (BizimPiyon.Anl�kY == HedefPiyon.Anl�kY + 1 || BizimPiyon.Anl�kY == HedefPiyon.Anl�kY - 1)
                    {
                        //D��man�n tak�m� ne? Ona g�re yeme hareketini ger�ekle�tir.

                        switch (HedefPiyon.takim)
                        {
                            //Beyaz tak�m
                            case 0:

                                yenmisBeyaz.Add(HedefPiyon);
                                //�len ta�� k���lt ve tahtan�n kenar�na koy.
                                HedefPiyon.BoyutSet(Vector3.one * yenmeBoyutu, true);
                                HedefPiyon.KonumSet(new Vector3(8 * KareBoyutu, Yoffset, -1 * KareBoyutu) - Limit + new Vector3(KareBoyutu / 2, 0, KareBoyutu / 2) + (Vector3.forward * 2.5f) * yenmisBeyaz.Count);
                                //Bu kodu daha �nceden yazd�k, a�a��da ta�� hareket ettir fonksiyonu i�erisinde.
                                break;
                            //siyah tak�m:
                            case 1:

                                yenmisSiyah.Add(HedefPiyon);
                                HedefPiyon.BoyutSet(Vector3.one * yenmeBoyutu, true);
                                HedefPiyon.KonumSet(new Vector3(-1 * KareBoyutu, Yoffset, 8 * KareBoyutu) - Limit + new Vector3(KareBoyutu / 2, 0, KareBoyutu / 2) + (Vector3.back * 2.5f) * yenmisSiyah.Count);

                                break;
                        }

                        //a�a��dakini yapmazsak, ta��n hayaleti o noktadan ge�i�i engelliyor.
                        tahtadakiTaslar[HedefPiyon.Anl�kX, HedefPiyon.Anl�kY] = null;
                    }

                }

                break;
            //Rok hareketinin �art� kral �zerinde. �ah oynad�ktan sonra bu fonksiyonda kalenin yerini de�i�tir.
            case OzelHareket.Rok:

                //Rok hangi tarafa yap�ld�? Hangi kalenin hareket etmesi laz�m? Hangi tak�m?
                //Kolayl�k olsun diye baz� de�erleri de�i�ken i�inde tutuyoruz.

                Vector2Int[] SonHareket = HareketListesi[HareketListesi.Count - 1];

                //Muhtemelen Y de�erini kontrol etmeye gerek yok.
                //Sol Rok
                if (SonHareket[1].x == 2) //&& (SonHareket[1].y == 0 || SonHareket[1].y == 7))
                {
                    //Beyaz rok
                    if (SonHareket[1].y == 0)
                    {
                        //�zerinde i�lem yapt���m�z kaleleri tutmam�z laz�m. Asl�nda tutmasak da olur ama i�lemi daha anla��l�r yap�yor.
                        taslar kale = tahtadakiTaslar[0, 0];
                        //Bu kaleyi hareket ettir.
                        tahtadakiTaslar[3, 0] = kale;
                        TekTasKonumlandir(3, 0);

                        //�nceki konumu temizle
                        tahtadakiTaslar[0, 0] = null;

                    }

                    //Siyah Rok

                    if (SonHareket[1].y == 7)
                    {
                        //�zerinde i�lem yapt���m�z kaleleri tutmam�z laz�m. Asl�nda tutmasak da olur ama i�lemi daha anla��l�r yap�yor.
                        taslar kale = tahtadakiTaslar[0, 7];
                        //Bu kaleyi hareket ettir.
                        tahtadakiTaslar[3, 7] = kale;
                        TekTasKonumlandir(3, 7);

                        //�nceki konumu temizle
                        tahtadakiTaslar[0, 7] = null;

                    }




                }

                //Sa� Rok:

                else if (SonHareket[1].x == 6)
                {
                    //Beyaz rok
                    if (SonHareket[1].y == 0)
                    {
                        //�zerinde i�lem yapt���m�z kaleleri tutmam�z laz�m. Asl�nda tutmasak da olur ama i�lemi daha anla��l�r yap�yor.
                        taslar kale = tahtadakiTaslar[7, 0];
                        //Bu kaleyi hareket ettir.
                        tahtadakiTaslar[5, 0] = kale;
                        TekTasKonumlandir(5, 0);

                        //�nceki konumu temizle
                        tahtadakiTaslar[7, 0] = null;

                    }

                    //Siyah Rok

                    if (SonHareket[1].y == 7)
                    {
                        //�zerinde i�lem yapt���m�z kaleleri tutmam�z laz�m. Asl�nda tutmasak da olur ama i�lemi daha anla��l�r yap�yor.
                        taslar kale = tahtadakiTaslar[7, 7];
                        //Bu kaleyi hareket ettir.
                        tahtadakiTaslar[5, 7] = kale;
                        TekTasKonumlandir(5, 7);

                        //�nceki konumu temizle
                        tahtadakiTaslar[7, 7] = null;

                    }




                }

                break;

            case OzelHareket.PiyonYukseltme:

                //E�er piyon son kareye ula��rsa, piyonu krali�e yap. �imdilik sadece Krali�e -Baran

                var sonHareket = HareketListesi[HareketListesi.Count - 1];
                taslar piyon = tahtadakiTaslar[sonHareket[1].x, sonHareket[1].y];

                //muhtemelen bu kontrolu yapmaya gerek yok ama �zerinde i�lem yapt���m�z ta��n piyon oldu�udan emin olmak istiyorum.
                if (piyon.tip == Tastipi.Piyon)
                {
                    //Beyaz piyon
                    //piyon beyaz m� ve son noktaya ula�t� m�?
                    if (piyon.takim == 0 && sonHareket[1].y == 7)
                    {
                        //Yeni kralice spawn et
                        taslar Kralice = TekTasSpawn(Tastipi.Kralice, 0);
                        //�nceki piyonu yok et
                        Destroy(tahtadakiTaslar[sonHareket[1].x, sonHareket[1].y].gameObject);
                        //Bu konumda Krali�e oldu�unu kaydet
                        tahtadakiTaslar[sonHareket[1].x, sonHareket[1].y] = Kralice;
                        //T�m spawnlar fiziksel olarak tahtan�n ortas�nda. Unity koordinatlar� ile, oyun koordinatlar� aras�nda fark oldu�u i�in, hareket etme fonksiyonu ile hareket ettir
                        TekTasKonumlandir(sonHareket[1].x, sonHareket[1].y, true);

                    }

                    //Siyah Piyon
                    if (piyon.takim == 1 && sonHareket[1].y == 0)
                    {
                        //Yeni kralice spawn et
                        taslar Kralice = TekTasSpawn(Tastipi.Kralice, 1);
                        //�nceki piyonu yok et
                        Destroy(tahtadakiTaslar[sonHareket[1].x, sonHareket[1].y].gameObject);
                        //Bu konumda Krali�e oldu�unu kaydet
                        tahtadakiTaslar[sonHareket[1].x, sonHareket[1].y] = Kralice;
                        //T�m spawnlar fiziksel olarak tahtan�n ortas�nda. Unity koordinatlar� ile, oyun koordinatlar� aras�nda fark oldu�u i�in, hareket etme fonksiyonu ile hareket ettir
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
        //E�er �ah� tehlikeye atan bir hareket varsa bu harekete izin verme. Bunun i�in �nceden t�m ta�lar�n yapabilece�i hareketleri �ah�n konumu ile kar��la�t�rmam�z gerekiyor

        taslar HedefKral = null;
        //Kral nerede?
        for (int x = 0; x < KARE_X; x++)
        {

            for (int y = 0; y < KARE_Y; y++)
            {


                 if((tahtadakiTaslar[x, y]!=null))       // hata, bunu kullanma if(tahtadakiTaslar[x, y].tip==Tastipi.Kral) 
                {
                    //Buldu�umuz �ah bizim mi?

                    if (Anl�kTas�nanTas.takim == tahtadakiTaslar[x, y].takim)
                    {
                        //bizim kral bu koordinatlarda

                        HedefKral = tahtadakiTaslar[x, y];

                    }



                }
            }

        }
        //referans olarak g�ndermemizin sebebi, kral� tehlikeye atan ad�mlar� yap�labilinen hamlelerden ��karaca��z.
        //TekTasHareketSimuleEt(Anl�kTas�nanTas,ref yap�labilinenHamleler,HedefKral);


    }
   

    //Hareket ettir

    private bool DogruHaraketVar(ref List<Vector2Int> hareketler,Vector2 pos)
    {
        //Yap�labilen hareketleri belirlemek i�in kontrol ediyoruz, eski highlight sistemini de�i�tirmek i�in.

        //E�er kontrol etti�imiz yerde yap�labilen bir hamle varsa highlight efektini bozma.

        //Ayr�ca simulasyonda da kullan�l�yor. 
        for (int i = 0; i < hareketler.Count; i++)
        {
            if(hareketler[i].x==pos.x && hareketler[i].y == pos.y)
            {

                return true;
            }
        }
        
        //Bu fonksiyon, hamle tutan bir listedeki hamleleri istenilen konum ile kar��la�t�r�r. E�er istenen konum bu listede ise true d�nderir.

        return false;

    }
    private bool Tas�HareketEttir(taslar cp, int x, int y)
    {

        //Gitmek istedi�imiz yer hareket kurallar�na uyuyor mu?

        if (!DogruHaraketVar(ref yap�labilinenHamleler,new Vector2Int(x,y)))

        { return false; }

            //gitti�imiz yerde ba�ka bir ta� var m�?

            if (tahtadakiTaslar[x, y] != null)
        {
            taslar HedefTas = tahtadakiTaslar[x, y];
            //E�er gitmeye �al��t���m�z konumda bizim tak�mdan ta� varsa bu konuma gidilemez
            if (cp.takim == HedefTas.takim)
            {
                return false;

            }
            switch (HedefTas.takim)
            {
                //hedef ta��n tak�m� ne?
                case 0:
                    //beyaz indexi 0
                    //E�er �ah �l�rse, oyun biter.
                    if (HedefTas.tip == Tastipi.Kral)
                    {
                        Mat(1);
                    }
                    yenmisBeyaz.Add(HedefTas);
                    //�len ta�� k���lt ve tahtan�n kenar�na koy.
                    HedefTas.BoyutSet(Vector3.one * yenmeBoyutu, true);
                    HedefTas.KonumSet(new Vector3(8 * KareBoyutu, Yoffset, -1 * KareBoyutu) - Limit + new Vector3(KareBoyutu / 2, 0, KareBoyutu / 2) + (Vector3.forward * 2.5f) * yenmisBeyaz.Count);
                    //Yukar�dakinde yeniden orta bulmakla u�ra�mam�z�n sebebi grid'in d���nda bir konuma ta� g�nderiyoruz. Aral�nda bo�luk oldu�undan emin ol. 0.5f~
                    break;

                case 1:
                    //E�er �ah �l�rse, oyun biter.
                    if (HedefTas.tip == Tastipi.Kral)
                    {
                        Mat(0);
                    }
                    yenmisSiyah.Add(HedefTas);
                    HedefTas.BoyutSet(Vector3.one * yenmeBoyutu, true);
                    HedefTas.KonumSet(new Vector3(-1 * KareBoyutu, Yoffset, 8 * KareBoyutu) - Limit + new Vector3(KareBoyutu / 2, 0, KareBoyutu / 2) + (Vector3.back * 2.5f) * yenmisSiyah.Count);

                    break;

            }

            //E�er gitti�imiz yerde d��man ta� varsa onu ye.

           



        }

        Vector2Int oncekiKonum = new Vector2Int(cp.Anl�kX,cp.Anl�kY);
        tahtadakiTaslar[x, y] = cp;
        tahtadakiTaslar[oncekiKonum.x, oncekiKonum.y] = null;
        TekTasKonumlandir(x, y);
        //S�ray� ters �evir.
        BeyazSirasi = !BeyazSirasi;
        //Yap�lan hareketi haf�zada kaydet. 
        HareketListesi.Add(new Vector2Int[] { oncekiKonum, new Vector2Int(x, y) });

        //�zel hareket ko�ulunu incele:
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
    //Bu fonksiyon tek tas olu�turmay� sa�l�yor. Ayr� ayr� tas olu�turulabildi�i gibi, TumTasSpawn Fonksiyonu ile t�m tahta dolusu ta� olu�turulabilinir.
    private void TumTaslarSpawn()
    {
        tahtadakiTaslar = new taslar[KARE_X,KARE_Y];
        int beyaz = 0, siyah = 1;

        //beyaz tak�m ba�lang�� yerle�tirmeleri

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



        //instantiate obje yaratan unity codu. �nceden belirlenmi� prefablar� yarat�r.
        taslar tas = Instantiate(prefabs[(int) tip-1],transform).GetComponent<taslar>();

        tas.tip = tip;
        tas.takim = takim;
        tas.GetComponent<MeshRenderer>().material = takimMateryalleri[takim];
        //Siyahlar� ters d�nd�r yoksa yanl�� tarafa bak�yorlar.
        if (tas.takim == 1)
        {
            tas.transform.Rotate(0,0,180f);
        }

        return tas;
    }


    private void TumTaslariKonumlandir() 
    { //bu fonksiyon oyun ba�lad���nda t�m ta�lar�n yerinde olmas�n� tek ta� konumland�rma fonksiyonu ile sa�lar. Tek ta� konumland�rma
      //bu fonksiyonun d���nda ta�lar� hareket ettirmek i�in kullna�l�r. bool false veya true olmas� bu ta�lar�n nas�l hareket edece�ini belirler.

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
        tahtadakiTaslar[x, y].Anl�kX = x;
        tahtadakiTaslar[x, y].Anl�kY = y;
        tahtadakiTaslar[x, y].KonumSet(kareOrtas�n�Bul(x,y),force);
    
    }

    private Vector3 kareOrtas�n�Bul(int x,int y)
    {
        //bu kod ta�lar�n karenin ortas�nda olmas�n� sa�l�yor, yoksa sadece k��elere gidiyorlar
        //bunu fonksiyon yapmasak da olurdu, ileride laz�m olur belki
        return new Vector3(x * KareBoyutu, Yoffset, y * KareBoyutu) - Limit + new Vector3(KareBoyutu/2,0,KareBoyutu/2);


    }


    //Yap�labilen t�m hareketleri belirginle�tiren fonksiyonlar:

    private void KareBelirginlestir()
    {
        for (int i = 0; i < yap�labilinenHamleler.Count; i++)
        {
            kareler[yap�labilinenHamleler[i].x, yap�labilinenHamleler[i].y].layer = LayerMask.NameToLayer("Highlight");
            //hoverdaki gibi, katmanlar render �ncelli�ini ve nas�l render yap�laca��n� belirlemek i�in kullan�l�r.
            
        }

    }
    private void KareBelirginKald�r()
    {
        for (int i = 0; i < yap�labilinenHamleler.Count; i++)
        {
            kareler[yap�labilinenHamleler[i].x, yap�labilinenHamleler[i].y].layer = LayerMask.NameToLayer("Kare");
            //hoverdaki gibi, katmanlar render �ncelli�ini ve nas�l render yap�laca��n� belirlemek i�in kullan�l�r.
            //Yukar�dakinin ayn�s�, �nceki highlightlar� sil.
           

        }
        yap�labilinenHamleler.Clear();

    }


    //�ah Mat

    private void Mat(int KazananTakim)
    {

        ZaferGoster(KazananTakim);


    }
    private void ZaferGoster(int KazananTakim)
    {

        zaferEkran�.SetActive(true);
        //Do�ru tak�m�n zafer yaz�s�n�n g�r�nmesi i�in indexi beyaz i�in(0), siyah i�in(1) olmas� gerekir.
        //Bu d�zen edit�rden ayarlanm��t�r.
        zaferEkran�.transform.GetChild(KazananTakim).gameObject.SetActive(true);


    }

    //��k veya s�f�rla d��meleri

    public void SifirlaDugmesi()
    {
        //Bu d��meye bas�ld���nda oyun ba�a d�ns�n.

        //UI
        zaferEkran�.SetActive(false);
        zaferEkran�.transform.GetChild(0).gameObject.SetActive(false);
        zaferEkran�.transform.GetChild(1).gameObject.SetActive(false);

        //Bazen yanl��l�kla, h�zl� basarken elde ta� kalabiliyor bu y�zden ne olur ne olmaz diye null att�m -Baran
        Anl�kTas�nanTas = null;
        yap�labilinenHamleler.Clear();
        //�zel hareketlerin temizlenmesi bir sonraki oyun i�in �nemli!
        HareketListesi.Clear();



        //temizlik

        //T�m kareleri temizle
        for (int x = 0; x < KARE_X; x++)
        {

            for (int y = 0; y < KARE_Y; y++)
            {
                //E�er taranan yerde bir ta� varsa, ta�� sil.
                if (tahtadakiTaslar[x, y] != null)
                {
                    //ta�� yok et
                    Destroy(tahtadakiTaslar[x, y].gameObject);

                    //bu koordinatlarda art�k ta� yok.

                    tahtadakiTaslar[x, y] = null;
                }
                //bu sadece hayatta kalan ta�lar� temizler, bir de �len ta�lar var.
                        

            }

            for (int i = 0; i < yenmisBeyaz.Count; i++)
            {
                //yenmi� beyazlar� sil.
                Destroy(yenmisBeyaz[i].gameObject);

            }
            for (int i = 0; i < yenmisSiyah.Count; i++)
            {
                //yenmi� beyazlar� sil.
                Destroy(yenmisSiyah[i].gameObject);

            }

            //listeleri temizle
            yenmisBeyaz.Clear();
            yenmisSiyah.Clear();
        }

        BeyazSirasi = true;
        //T�m karalerde ta� bulunabilir. E�er bir karede ta� yoksa de�eri nulldur.
        //Awake();
        //Awake �a��rmay� denedim ama yeniden grid olu�turdu�u i�in bir s�r� hata olu�tu. Sadece ta� spawn ve ta� konum laz�m

        TumTaslarSpawn();
        TumTaslariKonumlandir();
    }
    public void CikisDugmesi() 
    {


        //Oyundan ��k

        Application.Quit();

    
    }


}

