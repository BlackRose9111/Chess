using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piyon : taslar
{
   public override List<Vector2Int> GidilebilecekKonumlarýBul(ref taslar[,] tahta,int KareX, int KareY) 
    {
        //piyonun gidebildiði konumlarý tutan vector 2 listesi.

        List<Vector2Int> r = new List<Vector2Int>();
        //Yön takýma göre bir piyonun tahtanýn hangi kenarýna doðru gitmesi gerektiðini belirler.
        //Beyaz pozitif yön, siyah negatif yön.
        int direction = (takim == 0) ? 1 : -1;

        if ((takim == 0 && AnlýkY == 7) || (takim == 1 && AnlýkY == 0))
        { return r; 
        
        //Bu bir satýr için sabaha kadar uðraþtým. Eðer sona geldiysek piyonun hareketine izin verme. -Baran
        
        }    

            if (tahta[AnlýkX, AnlýkY + direction] == null)
        {
            r.Add(new Vector2Int(AnlýkX,AnlýkY+direction));

        }

        //piyon ilk hamlede iki adým ileri gidebilir.

        if (tahta[AnlýkX, AnlýkY + direction] == null)
        {
            //Beyaz mýyýz? Baþlangýç konumunda mýyýz? Ýki adým ötede bir engel var mý?
            if (takim == 0 && AnlýkY==1 &&tahta[AnlýkX, AnlýkY+(direction*2)]==null)
            {
                r.Add(new Vector2Int(AnlýkX, AnlýkY + (direction * 2)));


            }

            //Siyah mýyýz? Baþlangýç konumunda mýyýz? Ýki adým ötede bir engel var mý?
            if (takim == 1 && AnlýkY == 6 && tahta[AnlýkX, AnlýkY + (direction * 2)] == null)
            {
                r.Add(new Vector2Int(AnlýkX, AnlýkY + (direction * 2)));


            }


        }
        //Yeme haraketi

        //Çarpraz giderken tahtanýn dýþýnda yer arama, hata verebilir.
        //Sýradaki if en saðdaki piyon için. 
        //Bunun için muhtemelen daha kolay bir yöntem var ama aklýma bir þey gelmiyor, deðiþtirip bozmaktan korktum -Baran
      
        if (AnlýkX != KareX-1)
        {
            //Çarpraz sað boþ mu? Boþ deðilse çarprazdaki düþman mý?
            if (tahta[AnlýkX + 1, AnlýkY + direction] != null && tahta[AnlýkX + 1, AnlýkY + direction].takim != takim)
            {

                r.Add(new Vector2Int(AnlýkX + 1, AnlýkY + direction));

            }



        }

        if (AnlýkX != 0)
        {
            //sol taraf için.
            if (tahta[AnlýkX - 1, AnlýkY + direction] != null && tahta[AnlýkX - 1, AnlýkY + direction].takim != takim)
            {

                r.Add(new Vector2Int(AnlýkX - 1, AnlýkY + direction));

            }
        }

        
            
        return r;

    
    }

    public override OzelHareket OzelHareketlerGet(ref taslar[,] tahta, ref List<Vector2Int[]> hareketListesi, ref List<Vector2Int> YapýlabilinenHamleler)
    {
        //Beyaz takým iler, +1, siyah takým geri -1
        int yon = (takim == 0) ? 1 : -1;

        //En passant
        //Daha önce hareket yapýlmýþ mý? En Passant sadece ilk hareketten sonra yapýlýr ve bir daha yapýlamaz.
        if (hareketListesi.Count > 0)
        {
            Vector2Int[] SonHareket = hareketListesi[hareketListesi.Count - 1];

            //Son yapýlan hareket piyon mu?
            if (tahta[SonHareket[1].x, SonHareket[1].y].tip==Tastipi.Piyon)
            {
                //En passant için piyonun iki kare çýkmýþ olmasý gerekir. Yön yüzünden iþaret deðiþebilir ama sorun deðil
                //Math.Abs mutlak deðerini alýr.
                //Ýlk konum ile ikinci konum arasýnda 2 kare fark var mý? Hem siyah hem beyaz için mutlak deðer!
                if (Mathf.Abs(SonHareket[0].y - SonHareket[1].y) == 2)
                {
                    //zaten turn sistemi var, bunu yazmasak da olurdu ama yinede çalýþtýðýndan emin olmak istediðim için, son hareketin rakip takýmdan olduðunu kontrol edelim

                    if(tahta[SonHareket[1].x,SonHareket[1].y].takim!=takim)
                    {
                        //son hareket her zaman rakip takým ama yine de sorun çýksýn istemiyorum.

                        //En passant haraketi sadece piyonlar arasýnda olurmuþ. Bunun için iki piyonun dip dibe olmasý gerekir.

                        if (SonHareket[1].y == AnlýkY) //ayný y Hizasýnda mýyýz?
                        {
                            if (SonHareket[1].x==AnlýkX-1) //Ýki piyon dip dibe mi?
                            {
                                //Solda ise, yapýlabilinen hamleye ekle
                                YapýlabilinenHamleler.Add(new Vector2Int(AnlýkX-1,AnlýkY+yon));

                                return OzelHareket.EnPassant;

                            }
                            if (SonHareket[1].x == AnlýkX + 1)
                            {
                                //Saðda ise yapýlabilinen hamlelere ekle.
                                YapýlabilinenHamleler.Add(new Vector2Int(AnlýkX + 1, AnlýkY + yon));
                                return OzelHareket.EnPassant;

                            }


                        }


                    }

                }
                

            }

        }

        //Piyon Yükseltme
        //Piyon yükseltme yapmak üzere mi? Bir önceki kareyi kontrol etmemizin sebebi, son kareye ulaþma hamlesi ile birlikte yapmamýz gerekli.
        if ((takim == 0 && AnlýkY==6) || takim==1 && AnlýkY==1)

        {

           return OzelHareket.PiyonYukseltme;

        }




        return OzelHareket.Yok;
    }


}
