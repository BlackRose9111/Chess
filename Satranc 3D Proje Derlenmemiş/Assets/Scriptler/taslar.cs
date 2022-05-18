using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aþaðýdaki kod, sahne açýldýðýnda taþlarýn otomatik olarak doðru yüklenmesi için önemli. Her taþýn ve takýmýn bir ID numarasý olmalý. Bu veriler kullanýlarak 
//taþlar otomatik spawn olacaklar.
public enum Tastipi
{
    None = 0,
    Piyon=1,
    Kale=2,
    At=3,
    Fil=4,
    Kralice=5,
    Kral=6



}
public class taslar : MonoBehaviour
{
    public int takim;
    public int AnlýkX;
    public int AnlýkY;
    public Tastipi tip;

    private Vector3 istenilenKonum;
    private Vector3 istenilenBoyut;

    public virtual void KonumSet(Vector3 konum,bool force = false)
    {

        istenilenKonum = konum;
        //Taþýn hareketi için, ýþýnlamasý mý gerekli yoksa yavaþca sürüklenmesi mi?

        //ýþýnlamna:
        if (force) { transform.position = istenilenKonum;  }

        
    }
    public virtual void BoyutSet(Vector3 boyut, bool force = false)
    {

        istenilenBoyut = boyut;
        //Taþýn hareketi için, ýþýnlamasý mý gerekli yoksa yavaþca sürüklenmesi mi?

        //ýþýnlamna:
        if (force) { transform.localScale = istenilenBoyut; }

        //Doðrusal hareket update içinde


    }

    //Bir taþ bulunduðu konumdan nereye gidebilir? Bu fonksiyon çok önemli.
    public virtual List<Vector2Int> GidilebilecekKonumlarýBul(ref taslar[,] tahta,int tahtaBoyutX,int tahtaBoyutY)
    {
        List<Vector2Int> r = new List<Vector2Int>();
        //eðer aþaðýdaki karaler highlight edilmiþ ise, bir hata vardýr. Debug et!
        r.Add(new Vector2Int(3, 3));
        r.Add(new Vector2Int(3, 4));
        r.Add(new Vector2Int(4, 3));
        r.Add(new Vector2Int(4, 4));
        return r;
    }

    private void Update()
    {
        //Yavaþca doðrusal hareket. Taþýn bir noktadan diðerine belirlenen hýzda hareket etmesini saðlar.
        transform.position = Vector3.Lerp(transform.position, istenilenKonum, Time.deltaTime * 10);
        //Boyutunu deðiþtirmek için þimdilik kopyala yapýþtýr.

        //Taþlarýn boyutlarýný elle belirlemek zorunda kaldým, model kaynaklý sýkýntý, taþ boyutunu deðiþtirme
        //Yoksa güzel durmuyor.
        //transform.localScale = Vector3.Lerp(transform.localScale, istenilenBoyut, Time.deltaTime * 10);
    }
    //özel hareket dönderen, özel harektleri hesaplayan fonksiyon
   public virtual OzelHareket OzelHareketlerGet(ref taslar[,] tahta, ref List<Vector2Int[]> hareketListesi , ref List<Vector2Int> YapýlabilinenHamleler)
    {
        OzelHareket ozel = new OzelHareket();

        return ozel;

    }


}

