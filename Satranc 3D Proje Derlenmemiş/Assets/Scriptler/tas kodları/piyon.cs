using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piyon : taslar
{
   public override List<Vector2Int> GidilebilecekKonumlar�Bul(ref taslar[,] tahta,int KareX, int KareY) 
    {
        //piyonun gidebildi�i konumlar� tutan vector 2 listesi.

        List<Vector2Int> r = new List<Vector2Int>();
        //Y�n tak�ma g�re bir piyonun tahtan�n hangi kenar�na do�ru gitmesi gerekti�ini belirler.
        //Beyaz pozitif y�n, siyah negatif y�n.
        int direction = (takim == 0) ? 1 : -1;

        if ((takim == 0 && Anl�kY == 7) || (takim == 1 && Anl�kY == 0))
        { return r; 
        
        //Bu bir sat�r i�in sabaha kadar u�ra�t�m. E�er sona geldiysek piyonun hareketine izin verme. -Baran
        
        }    

            if (tahta[Anl�kX, Anl�kY + direction] == null)
        {
            r.Add(new Vector2Int(Anl�kX,Anl�kY+direction));

        }

        //piyon ilk hamlede iki ad�m ileri gidebilir.

        if (tahta[Anl�kX, Anl�kY + direction] == null)
        {
            //Beyaz m�y�z? Ba�lang�� konumunda m�y�z? �ki ad�m �tede bir engel var m�?
            if (takim == 0 && Anl�kY==1 &&tahta[Anl�kX, Anl�kY+(direction*2)]==null)
            {
                r.Add(new Vector2Int(Anl�kX, Anl�kY + (direction * 2)));


            }

            //Siyah m�y�z? Ba�lang�� konumunda m�y�z? �ki ad�m �tede bir engel var m�?
            if (takim == 1 && Anl�kY == 6 && tahta[Anl�kX, Anl�kY + (direction * 2)] == null)
            {
                r.Add(new Vector2Int(Anl�kX, Anl�kY + (direction * 2)));


            }


        }
        //Yeme haraketi

        //�arpraz giderken tahtan�n d���nda yer arama, hata verebilir.
        //S�radaki if en sa�daki piyon i�in. 
        //Bunun i�in muhtemelen daha kolay bir y�ntem var ama akl�ma bir �ey gelmiyor, de�i�tirip bozmaktan korktum -Baran
      
        if (Anl�kX != KareX-1)
        {
            //�arpraz sa� bo� mu? Bo� de�ilse �arprazdaki d��man m�?
            if (tahta[Anl�kX + 1, Anl�kY + direction] != null && tahta[Anl�kX + 1, Anl�kY + direction].takim != takim)
            {

                r.Add(new Vector2Int(Anl�kX + 1, Anl�kY + direction));

            }



        }

        if (Anl�kX != 0)
        {
            //sol taraf i�in.
            if (tahta[Anl�kX - 1, Anl�kY + direction] != null && tahta[Anl�kX - 1, Anl�kY + direction].takim != takim)
            {

                r.Add(new Vector2Int(Anl�kX - 1, Anl�kY + direction));

            }
        }

        
            
        return r;

    
    }

    public override OzelHareket OzelHareketlerGet(ref taslar[,] tahta, ref List<Vector2Int[]> hareketListesi, ref List<Vector2Int> Yap�labilinenHamleler)
    {
        //Beyaz tak�m iler, +1, siyah tak�m geri -1
        int yon = (takim == 0) ? 1 : -1;

        //En passant
        //Daha �nce hareket yap�lm�� m�? En Passant sadece ilk hareketten sonra yap�l�r ve bir daha yap�lamaz.
        if (hareketListesi.Count > 0)
        {
            Vector2Int[] SonHareket = hareketListesi[hareketListesi.Count - 1];

            //Son yap�lan hareket piyon mu?
            if (tahta[SonHareket[1].x, SonHareket[1].y].tip==Tastipi.Piyon)
            {
                //En passant i�in piyonun iki kare ��km�� olmas� gerekir. Y�n y�z�nden i�aret de�i�ebilir ama sorun de�il
                //Math.Abs mutlak de�erini al�r.
                //�lk konum ile ikinci konum aras�nda 2 kare fark var m�? Hem siyah hem beyaz i�in mutlak de�er!
                if (Mathf.Abs(SonHareket[0].y - SonHareket[1].y) == 2)
                {
                    //zaten turn sistemi var, bunu yazmasak da olurdu ama yinede �al��t���ndan emin olmak istedi�im i�in, son hareketin rakip tak�mdan oldu�unu kontrol edelim

                    if(tahta[SonHareket[1].x,SonHareket[1].y].takim!=takim)
                    {
                        //son hareket her zaman rakip tak�m ama yine de sorun ��ks�n istemiyorum.

                        //En passant haraketi sadece piyonlar aras�nda olurmu�. Bunun i�in iki piyonun dip dibe olmas� gerekir.

                        if (SonHareket[1].y == Anl�kY) //ayn� y Hizas�nda m�y�z?
                        {
                            if (SonHareket[1].x==Anl�kX-1) //�ki piyon dip dibe mi?
                            {
                                //Solda ise, yap�labilinen hamleye ekle
                                Yap�labilinenHamleler.Add(new Vector2Int(Anl�kX-1,Anl�kY+yon));

                                return OzelHareket.EnPassant;

                            }
                            if (SonHareket[1].x == Anl�kX + 1)
                            {
                                //Sa�da ise yap�labilinen hamlelere ekle.
                                Yap�labilinenHamleler.Add(new Vector2Int(Anl�kX + 1, Anl�kY + yon));
                                return OzelHareket.EnPassant;

                            }


                        }


                    }

                }
                

            }

        }

        //Piyon Y�kseltme
        //Piyon y�kseltme yapmak �zere mi? Bir �nceki kareyi kontrol etmemizin sebebi, son kareye ula�ma hamlesi ile birlikte yapmam�z gerekli.
        if ((takim == 0 && Anl�kY==6) || takim==1 && Anl�kY==1)

        {

           return OzelHareket.PiyonYukseltme;

        }




        return OzelHareket.Yok;
    }


}
