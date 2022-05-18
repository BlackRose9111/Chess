using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kral : taslar
{

    public override List<Vector2Int> GidilebilecekKonumlar�Bul(ref taslar[,] tahta, int KareX, int KareY)
    {

        List<Vector2Int> r = new List<Vector2Int>();

        //Sa�
        //Sa� taraf tahtan�n i�i mi? Sa�da hedef var m�? 

        if (Anl�kX + 1 < KareX)
        {
            if (tahta[Anl�kX + 1, Anl�kY] == null)
            {
                //Bo� ise bu kareye gidilebilinir. Listeye ekle.
                r.Add(new Vector2Int(Anl�kX + 1, Anl�kY));


            }
            //hedef varsa d��man m�? D��mansa ekle.
            else if (tahta[Anl�kX + 1, Anl�kY].takim != takim)
            {
                r.Add(new Vector2Int(Anl�kX + 1, Anl�kY));

            }

            //Sa� �st �apraz 

            //�st�m�z tahtan�n i�inde mi?
            if (Anl�kY + 1 < KareY)
            {
                if (tahta[Anl�kX + 1, Anl�kY+1] == null)
                {
                    //Bo� ise bu kareye gidilebilinir. Listeye ekle.
                    r.Add(new Vector2Int(Anl�kX + 1, Anl�kY+1));


                }
                // hedef varsa d��man m�? D��mansa ekle.
                else if (tahta[Anl�kX + 1, Anl�kY+1].takim != takim)
                {
                    r.Add(new Vector2Int(Anl�kX + 1, Anl�kY+1));

                }


            }

            //Sa� alt
            if (Anl�kY - 1 >=0)
            {
                if (tahta[Anl�kX + 1, Anl�kY - 1] == null)
                {
                    //Bo� ise bu kareye gidilebilinir. Listeye ekle.
                    r.Add(new Vector2Int(Anl�kX + 1, Anl�kY - 1));


                }
                // hedef varsa d��man m�? D��mansa ekle.
                else if (tahta[Anl�kX + 1, Anl�kY - 1].takim != takim)
                {
                    r.Add(new Vector2Int(Anl�kX + 1, Anl�kY - 1));

                }


            }


        }


        //Sol
        if (Anl�kX -1 >= 0)
        {
            if (tahta[Anl�kX - 1, Anl�kY] == null)
            {
                //Bo� ise bu kareye gidilebilinir. Listeye ekle.
                r.Add(new Vector2Int(Anl�kX - 1, Anl�kY));


            }
            //hedef varsa d��man m�? D��mansa ekle.
            else if (tahta[Anl�kX - 1, Anl�kY].takim != takim)
            {
                r.Add(new Vector2Int(Anl�kX - 1, Anl�kY));

            }

            //Sol �st �apraz 

            //�st�m�z tahtan�n i�inde mi?
            if (Anl�kY + 1 < KareY)
            {
                if (tahta[Anl�kX - 1, Anl�kY + 1] == null)
                {
                    //Bo� ise bu kareye gidilebilinir. Listeye ekle.
                    r.Add(new Vector2Int(Anl�kX - 1, Anl�kY + 1));


                }
                // hedef varsa d��man m�? D��mansa ekle.
                else if (tahta[Anl�kX - 1, Anl�kY + 1].takim != takim)
                {
                    r.Add(new Vector2Int(Anl�kX - 1, Anl�kY + 1));

                }


            }

            //sol alt
            if (Anl�kY - 1 >= 0)
            {
                if (tahta[Anl�kX - 1, Anl�kY - 1] == null)
                {
                    //Bo� ise bu kareye gidilebilinir. Listeye ekle.
                    r.Add(new Vector2Int(Anl�kX - 1, Anl�kY - 1));


                }
                // hedef varsa d��man m�? D��mansa ekle.
                else if (tahta[Anl�kX - 1, Anl�kY - 1].takim != takim)
                {
                    r.Add(new Vector2Int(Anl�kX - 1, Anl�kY - 1));

                }


            }


        }


        //�leri 
        //Hedef tahtan�n i�inde mi?
        if (Anl�kY + 1 < KareY)
        {
            //Hedef bo� mu?
            if (tahta[Anl�kX, Anl�kY + 1] == null)
            {
                //Bo� ise ekle

                r.Add(new Vector2Int(Anl�kX,Anl�kY+1));

            }
            //Bo� de�ilse hedef d��man m�?
            else if (tahta[Anl�kX, Anl�kY + 1].takim != takim)
            {
                r.Add(new Vector2Int(Anl�kX, Anl�kY + 1));

            }

        }

        //Geri
        if (Anl�kY -1 >= 0)
        {
            //Hedef bo� mu?
            if (tahta[Anl�kX, Anl�kY - 1] == null)
            {
                //Bo� ise ekle

                r.Add(new Vector2Int(Anl�kX, Anl�kY - 1));

            }
            //Bo� de�ilse hedef d��man m�?
            else if (tahta[Anl�kX, Anl�kY - 1].takim != takim)
            {
                r.Add(new Vector2Int(Anl�kX, Anl�kY - 1));

            }

        }


        return r;
    }


    //Rok hareketi: �ah, at ve piyon aradan �ekildikten sonra, kale hareket etmemi�se, �ah kalenin arkas�na ge�er ve kale �ah�n �n�ne ge�er.
    public override OzelHareket OzelHareketlerGet(ref taslar[,] tahta, ref List<Vector2Int[]> hareketListesi, ref List<Vector2Int> Yap�labilinenHamleler)
    {
        OzelHareket r = OzelHareket.Yok;
        //�ay ba�lang�� konumunda m�? Beyaz �ah 4,0 Siyah �ah 4,7. E�er yap�lan hareketler aras�na �ah hareket etmi� mi? Bunu anlamak i�in daha �nce yap�lan hareketlerin aras�nda ar�yoruz.
        var KralHareket = hareketListesi.Find(m=>m[0].x==4 && m[0].y== ((takim==0)? 0 : 7) );
        //Sol kale daha �nce hareket etmi� mi? Listede ara.
        var solKale = hareketListesi.Find(m => m[0].x == 0 && m[0].y == ((takim == 0) ? 0 : 7));
        //Sa� kale hareket etmi� mi? Listede ara
        var sagKale = hareketListesi.Find(m => m[0].x == 7 && m[0].y == ((takim == 0) ? 0 : 7));


        //Kral hareket etmemi�se olacak olaylar. Anl�kX kontrol etmeye gerek yok ama emin olmak istedim.
        if(KralHareket==null && Anl�kX == 4)
        {
            
            switch(takim)
            {
                //Beyaz Takim

                case 0:

                    //sol kale:

                    if (solKale == null)
                    {
                        //muhtemelen bu kontrol� de yapmaya gerek yok ama bu proje buglar y�z�nden beni paranoyak etti -baran
                        if (tahta[0, 0].tip == Tastipi.Kale) 
                        {
                        //Bakt���m�z kale ger�ekten bize mi ait? Bu kontrol de gereksiz. Yapmam�n sebebi, kale Rok yapmadan yenebilir. Do�ru �al��t���ndan emin olmak istiyorum.

                            if(tahta[0,0].takim==0)
                            {
                                //arada engel var m�? Bunun muhtemelen daha kolay bir yolu var.

                                if(tahta[3,0]==null && tahta[2,0]==null && tahta[1, 0] == null)
                                {
                                    //�ah�n rok yapmas� i�in gitmesi gerekti�i konumu listeye ekle.
                                    Yap�labilinenHamleler.Add(new Vector2Int(2,0));
                                    r = OzelHareket.Rok;
                                }

                            }
                        }

                    }
                    //Sa� Kale:

                    if (sagKale == null)
                    {
                        //muhtemelen bu kontrol� de yapmaya gerek yok ama bu proje buglar y�z�nden beni paranoyak etti -baran
                        if (tahta[7, 0].tip == Tastipi.Kale)
                        {
                            //Bakt���m�z kale ger�ekten bize mi ait? Bu kontrol de gereksiz. Yapmam�n sebebi, kale Rok yapmadan yenebilir. Do�ru �al��t���ndan emin olmak istiyorum.

                            if (tahta[7, 0].takim == 0)
                            {
                                //arada engel var m�? Bunun muhtemelen daha kolay bir yolu var.
                                //Sa� tarafta rok i�in daha az alan var
                                if (tahta[6, 0] == null && tahta[5, 0] == null)
                                {
                                    //�ah�n rok yapmas� i�in gitmesi gerekti�i konumu listeye ekle.
                                    Yap�labilinenHamleler.Add(new Vector2Int(6, 0));
                                    r = OzelHareket.Rok;
                                }

                            }
                        }

                    }





                    break;
                    //Siyah Tak�m
                case 1:
                    //sol kale:

                    if (solKale == null)
                    {
                        //muhtemelen bu kontrol� de yapmaya gerek yok ama bu proje buglar y�z�nden beni paranoyak etti -baran
                        if (tahta[0, 7].tip == Tastipi.Kale)
                        {
                            //Bakt���m�z kale ger�ekten bize mi ait? Bu kontrol de gereksiz. Yapmam�n sebebi, kale Rok yapmadan yenebilir. Do�ru �al��t���ndan emin olmak istiyorum.

                            if (tahta[0, 7].takim == 1)
                            {
                                //arada engel var m�? Bunun muhtemelen daha kolay bir yolu var.

                                if (tahta[3, 7] == null && tahta[2, 7] == null && tahta[1, 7] == null)
                                {
                                    //�ah�n rok yapmas� i�in gitmesi gerekti�i konumu listeye ekle.
                                    Yap�labilinenHamleler.Add(new Vector2Int(2, 7));
                                    r = OzelHareket.Rok;
                                }

                            }
                        }

                    }
                    //Sa� Kale:

                    if (sagKale == null)
                    {
                        //muhtemelen bu kontrol� de yapmaya gerek yok ama bu proje buglar y�z�nden beni paranoyak etti -baran
                        if (tahta[7, 7].tip == Tastipi.Kale)
                        {
                            //Bakt���m�z kale ger�ekten bize mi ait? Bu kontrol de gereksiz. Yapmam�n sebebi, kale Rok yapmadan yenebilir. Do�ru �al��t���ndan emin olmak istiyorum.

                            if (tahta[7, 7].takim == 1)
                            {
                                //arada engel var m�? Bunun muhtemelen daha kolay bir yolu var.
                                //Sa� tarafta rok i�in daha az alan var
                                if (tahta[6, 7] == null && tahta[5, 7] == null)
                                {
                                    //�ah�n rok yapmas� i�in gitmesi gerekti�i konumu listeye ekle.
                                    Yap�labilinenHamleler.Add(new Vector2Int(6, 7));
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