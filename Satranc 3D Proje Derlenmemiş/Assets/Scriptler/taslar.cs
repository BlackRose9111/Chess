using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A�a��daki kod, sahne a��ld���nda ta�lar�n otomatik olarak do�ru y�klenmesi i�in �nemli. Her ta��n ve tak�m�n bir ID numaras� olmal�. Bu veriler kullan�larak 
//ta�lar otomatik spawn olacaklar.
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
    public int Anl�kX;
    public int Anl�kY;
    public Tastipi tip;

    private Vector3 istenilenKonum;
    private Vector3 istenilenBoyut;

    public virtual void KonumSet(Vector3 konum,bool force = false)
    {

        istenilenKonum = konum;
        //Ta��n hareketi i�in, ���nlamas� m� gerekli yoksa yava�ca s�r�klenmesi mi?

        //���nlamna:
        if (force) { transform.position = istenilenKonum;  }

        
    }
    public virtual void BoyutSet(Vector3 boyut, bool force = false)
    {

        istenilenBoyut = boyut;
        //Ta��n hareketi i�in, ���nlamas� m� gerekli yoksa yava�ca s�r�klenmesi mi?

        //���nlamna:
        if (force) { transform.localScale = istenilenBoyut; }

        //Do�rusal hareket update i�inde


    }

    //Bir ta� bulundu�u konumdan nereye gidebilir? Bu fonksiyon �ok �nemli.
    public virtual List<Vector2Int> GidilebilecekKonumlar�Bul(ref taslar[,] tahta,int tahtaBoyutX,int tahtaBoyutY)
    {
        List<Vector2Int> r = new List<Vector2Int>();
        //e�er a�a��daki karaler highlight edilmi� ise, bir hata vard�r. Debug et!
        r.Add(new Vector2Int(3, 3));
        r.Add(new Vector2Int(3, 4));
        r.Add(new Vector2Int(4, 3));
        r.Add(new Vector2Int(4, 4));
        return r;
    }

    private void Update()
    {
        //Yava�ca do�rusal hareket. Ta��n bir noktadan di�erine belirlenen h�zda hareket etmesini sa�lar.
        transform.position = Vector3.Lerp(transform.position, istenilenKonum, Time.deltaTime * 10);
        //Boyutunu de�i�tirmek i�in �imdilik kopyala yap��t�r.

        //Ta�lar�n boyutlar�n� elle belirlemek zorunda kald�m, model kaynakl� s�k�nt�, ta� boyutunu de�i�tirme
        //Yoksa g�zel durmuyor.
        //transform.localScale = Vector3.Lerp(transform.localScale, istenilenBoyut, Time.deltaTime * 10);
    }
    //�zel hareket d�nderen, �zel harektleri hesaplayan fonksiyon
   public virtual OzelHareket OzelHareketlerGet(ref taslar[,] tahta, ref List<Vector2Int[]> hareketListesi , ref List<Vector2Int> Yap�labilinenHamleler)
    {
        OzelHareket ozel = new OzelHareket();

        return ozel;

    }


}

