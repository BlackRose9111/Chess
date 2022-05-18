using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kral : taslar
{

    public override List<Vector2Int> GidilebilecekKonumlarýBul(ref taslar[,] tahta, int KareX, int KareY)
    {

        List<Vector2Int> r = new List<Vector2Int>();

        //Sað
        //Sað taraf tahtanýn içi mi? Saðda hedef var mý? 

        if (AnlýkX + 1 < KareX)
        {
            if (tahta[AnlýkX + 1, AnlýkY] == null)
            {
                //Boþ ise bu kareye gidilebilinir. Listeye ekle.
                r.Add(new Vector2Int(AnlýkX + 1, AnlýkY));


            }
            //hedef varsa düþman mý? Düþmansa ekle.
            else if (tahta[AnlýkX + 1, AnlýkY].takim != takim)
            {
                r.Add(new Vector2Int(AnlýkX + 1, AnlýkY));

            }

            //Sað üst çapraz 

            //üstümüz tahtanýn içinde mi?
            if (AnlýkY + 1 < KareY)
            {
                if (tahta[AnlýkX + 1, AnlýkY+1] == null)
                {
                    //Boþ ise bu kareye gidilebilinir. Listeye ekle.
                    r.Add(new Vector2Int(AnlýkX + 1, AnlýkY+1));


                }
                // hedef varsa düþman mý? Düþmansa ekle.
                else if (tahta[AnlýkX + 1, AnlýkY+1].takim != takim)
                {
                    r.Add(new Vector2Int(AnlýkX + 1, AnlýkY+1));

                }


            }

            //Sað alt
            if (AnlýkY - 1 >=0)
            {
                if (tahta[AnlýkX + 1, AnlýkY - 1] == null)
                {
                    //Boþ ise bu kareye gidilebilinir. Listeye ekle.
                    r.Add(new Vector2Int(AnlýkX + 1, AnlýkY - 1));


                }
                // hedef varsa düþman mý? Düþmansa ekle.
                else if (tahta[AnlýkX + 1, AnlýkY - 1].takim != takim)
                {
                    r.Add(new Vector2Int(AnlýkX + 1, AnlýkY - 1));

                }


            }


        }


        //Sol
        if (AnlýkX -1 >= 0)
        {
            if (tahta[AnlýkX - 1, AnlýkY] == null)
            {
                //Boþ ise bu kareye gidilebilinir. Listeye ekle.
                r.Add(new Vector2Int(AnlýkX - 1, AnlýkY));


            }
            //hedef varsa düþman mý? Düþmansa ekle.
            else if (tahta[AnlýkX - 1, AnlýkY].takim != takim)
            {
                r.Add(new Vector2Int(AnlýkX - 1, AnlýkY));

            }

            //Sol üst çapraz 

            //üstümüz tahtanýn içinde mi?
            if (AnlýkY + 1 < KareY)
            {
                if (tahta[AnlýkX - 1, AnlýkY + 1] == null)
                {
                    //Boþ ise bu kareye gidilebilinir. Listeye ekle.
                    r.Add(new Vector2Int(AnlýkX - 1, AnlýkY + 1));


                }
                // hedef varsa düþman mý? Düþmansa ekle.
                else if (tahta[AnlýkX - 1, AnlýkY + 1].takim != takim)
                {
                    r.Add(new Vector2Int(AnlýkX - 1, AnlýkY + 1));

                }


            }

            //sol alt
            if (AnlýkY - 1 >= 0)
            {
                if (tahta[AnlýkX - 1, AnlýkY - 1] == null)
                {
                    //Boþ ise bu kareye gidilebilinir. Listeye ekle.
                    r.Add(new Vector2Int(AnlýkX - 1, AnlýkY - 1));


                }
                // hedef varsa düþman mý? Düþmansa ekle.
                else if (tahta[AnlýkX - 1, AnlýkY - 1].takim != takim)
                {
                    r.Add(new Vector2Int(AnlýkX - 1, AnlýkY - 1));

                }


            }


        }


        //Ýleri 
        //Hedef tahtanýn içinde mi?
        if (AnlýkY + 1 < KareY)
        {
            //Hedef boþ mu?
            if (tahta[AnlýkX, AnlýkY + 1] == null)
            {
                //Boþ ise ekle

                r.Add(new Vector2Int(AnlýkX,AnlýkY+1));

            }
            //Boþ deðilse hedef düþman mý?
            else if (tahta[AnlýkX, AnlýkY + 1].takim != takim)
            {
                r.Add(new Vector2Int(AnlýkX, AnlýkY + 1));

            }

        }

        //Geri
        if (AnlýkY -1 >= 0)
        {
            //Hedef boþ mu?
            if (tahta[AnlýkX, AnlýkY - 1] == null)
            {
                //Boþ ise ekle

                r.Add(new Vector2Int(AnlýkX, AnlýkY - 1));

            }
            //Boþ deðilse hedef düþman mý?
            else if (tahta[AnlýkX, AnlýkY - 1].takim != takim)
            {
                r.Add(new Vector2Int(AnlýkX, AnlýkY - 1));

            }

        }


        return r;
    }


    //Rok hareketi: Þah, at ve piyon aradan çekildikten sonra, kale hareket etmemiþse, Þah kalenin arkasýna geçer ve kale þahýn önüne geçer.
    public override OzelHareket OzelHareketlerGet(ref taslar[,] tahta, ref List<Vector2Int[]> hareketListesi, ref List<Vector2Int> YapýlabilinenHamleler)
    {
        OzelHareket r = OzelHareket.Yok;
        //Þay baþlangýç konumunda mý? Beyaz þah 4,0 Siyah Þah 4,7. Eðer yapýlan hareketler arasýna Þah hareket etmiþ mi? Bunu anlamak için daha önce yapýlan hareketlerin arasýnda arýyoruz.
        var KralHareket = hareketListesi.Find(m=>m[0].x==4 && m[0].y== ((takim==0)? 0 : 7) );
        //Sol kale daha önce hareket etmiþ mi? Listede ara.
        var solKale = hareketListesi.Find(m => m[0].x == 0 && m[0].y == ((takim == 0) ? 0 : 7));
        //Sað kale hareket etmiþ mi? Listede ara
        var sagKale = hareketListesi.Find(m => m[0].x == 7 && m[0].y == ((takim == 0) ? 0 : 7));


        //Kral hareket etmemiþse olacak olaylar. AnlýkX kontrol etmeye gerek yok ama emin olmak istedim.
        if(KralHareket==null && AnlýkX == 4)
        {
            
            switch(takim)
            {
                //Beyaz Takim

                case 0:

                    //sol kale:

                    if (solKale == null)
                    {
                        //muhtemelen bu kontrolü de yapmaya gerek yok ama bu proje buglar yüzünden beni paranoyak etti -baran
                        if (tahta[0, 0].tip == Tastipi.Kale) 
                        {
                        //Baktýðýmýz kale gerçekten bize mi ait? Bu kontrol de gereksiz. Yapmamýn sebebi, kale Rok yapmadan yenebilir. Doðru çalýþtýðýndan emin olmak istiyorum.

                            if(tahta[0,0].takim==0)
                            {
                                //arada engel var mý? Bunun muhtemelen daha kolay bir yolu var.

                                if(tahta[3,0]==null && tahta[2,0]==null && tahta[1, 0] == null)
                                {
                                    //Þahýn rok yapmasý için gitmesi gerektiði konumu listeye ekle.
                                    YapýlabilinenHamleler.Add(new Vector2Int(2,0));
                                    r = OzelHareket.Rok;
                                }

                            }
                        }

                    }
                    //Sað Kale:

                    if (sagKale == null)
                    {
                        //muhtemelen bu kontrolü de yapmaya gerek yok ama bu proje buglar yüzünden beni paranoyak etti -baran
                        if (tahta[7, 0].tip == Tastipi.Kale)
                        {
                            //Baktýðýmýz kale gerçekten bize mi ait? Bu kontrol de gereksiz. Yapmamýn sebebi, kale Rok yapmadan yenebilir. Doðru çalýþtýðýndan emin olmak istiyorum.

                            if (tahta[7, 0].takim == 0)
                            {
                                //arada engel var mý? Bunun muhtemelen daha kolay bir yolu var.
                                //Sað tarafta rok için daha az alan var
                                if (tahta[6, 0] == null && tahta[5, 0] == null)
                                {
                                    //Þahýn rok yapmasý için gitmesi gerektiði konumu listeye ekle.
                                    YapýlabilinenHamleler.Add(new Vector2Int(6, 0));
                                    r = OzelHareket.Rok;
                                }

                            }
                        }

                    }





                    break;
                    //Siyah Takým
                case 1:
                    //sol kale:

                    if (solKale == null)
                    {
                        //muhtemelen bu kontrolü de yapmaya gerek yok ama bu proje buglar yüzünden beni paranoyak etti -baran
                        if (tahta[0, 7].tip == Tastipi.Kale)
                        {
                            //Baktýðýmýz kale gerçekten bize mi ait? Bu kontrol de gereksiz. Yapmamýn sebebi, kale Rok yapmadan yenebilir. Doðru çalýþtýðýndan emin olmak istiyorum.

                            if (tahta[0, 7].takim == 1)
                            {
                                //arada engel var mý? Bunun muhtemelen daha kolay bir yolu var.

                                if (tahta[3, 7] == null && tahta[2, 7] == null && tahta[1, 7] == null)
                                {
                                    //Þahýn rok yapmasý için gitmesi gerektiði konumu listeye ekle.
                                    YapýlabilinenHamleler.Add(new Vector2Int(2, 7));
                                    r = OzelHareket.Rok;
                                }

                            }
                        }

                    }
                    //Sað Kale:

                    if (sagKale == null)
                    {
                        //muhtemelen bu kontrolü de yapmaya gerek yok ama bu proje buglar yüzünden beni paranoyak etti -baran
                        if (tahta[7, 7].tip == Tastipi.Kale)
                        {
                            //Baktýðýmýz kale gerçekten bize mi ait? Bu kontrol de gereksiz. Yapmamýn sebebi, kale Rok yapmadan yenebilir. Doðru çalýþtýðýndan emin olmak istiyorum.

                            if (tahta[7, 7].takim == 1)
                            {
                                //arada engel var mý? Bunun muhtemelen daha kolay bir yolu var.
                                //Sað tarafta rok için daha az alan var
                                if (tahta[6, 7] == null && tahta[5, 7] == null)
                                {
                                    //Þahýn rok yapmasý için gitmesi gerektiði konumu listeye ekle.
                                    YapýlabilinenHamleler.Add(new Vector2Int(6, 7));
                                    r = OzelHareket.Rok;
                                }

                            }
                        }

                    }





                    break;

            }
        }

        return r;
    }
}