using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class at : taslar
{
    public override List<Vector2Int> GidilebilecekKonumlarýBul(ref taslar[,] tahta, int KareX, int KareY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //At en kafa karýþtýrýcý

        int x = AnlýkX + 1;
        int y = AnlýkY + 2;

        //iki ileri bir saða L
        //Tahtanýn içerisine mi bakýyoruz?
        if(x<KareX && y<KareY)
        {
            //Baktýðýmýz kare boþ mu? veya düþman mý?
            if(tahta[x,y]==null || tahta[x,y].takim != takim)
            {
                r.Add(new Vector2Int(x,y));

            }


        }
        //bir ileri, iki saða L
        x = AnlýkX + 2;
        y = AnlýkY + 1;

        //iki ileri bir saða L
        //Tahtanýn içerisine mi bakýyoruz?
        if (x < KareX && y < KareY)
        {
            //Baktýðýmýz kare boþ mu? veya düþman mý?
            if (tahta[x, y] == null || tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));

            }


        }

        //iki ileri bir sol
        x = AnlýkX - 1;
        y = AnlýkY + 2;
        //sola ve geriye bakarken, tahta limitini 0dan say çünkü tahta 0dan KareX , 0dan KareY aralýðýnda 
        //0 dahil.


        if (x >=0 && y < KareY)
        {
            //Baktýðýmýz kare boþ mu? veya düþman mý?
            if (tahta[x, y] == null || tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));

            }


        }
        //Ýki sol bir ileri L
        x = AnlýkX - 2;
        y = AnlýkY + 1;
        //sola ve geriye bakarken, tahta limitini 0dan say çünkü tahta 0dan KareX , 0dan KareY aralýðýnda 
        //0 dahil.


        if (x >= 0 && y < KareY)
        {
            //Baktýðýmýz kare boþ mu? veya düþman mý?
            if (tahta[x, y] == null || tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));

            }


        }
        //bir sað iki geri L
        x = AnlýkX + 1;
        y = AnlýkY - 2;
        //sola ve geriye bakarken, tahta limitini 0dan say çünkü tahta 0dan KareX , 0dan KareY aralýðýnda 
        //0 dahil.


        if (x <KareX && y >= 0)
        {
            //Baktýðýmýz kare boþ mu? veya düþman mý?
            if (tahta[x, y] == null || tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));

            }


        }
        //Ýki saða bir geri
        x = AnlýkX + 2;
        y = AnlýkY - 1;
        //sola ve geriye bakarken, tahta limitini 0dan say çünkü tahta 0dan KareX , 0dan KareY aralýðýnda 
        //0 dahil.


        if (x < KareX && y >= 0)
        {
            //Baktýðýmýz kare boþ mu? veya düþman mý?
            if (tahta[x, y] == null || tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));

            }


        }

        //bir sol iki geri
        x = AnlýkX - 1;
        y = AnlýkY - 2;
        //sola ve geriye bakarken, tahta limitini 0dan say çünkü tahta 0dan KareX , 0dan KareY aralýðýnda 
        //0 dahil.


        if (x >=0 && y >= 0)
        {
            //Baktýðýmýz kare boþ mu? veya düþman mý?
            if (tahta[x, y] == null || tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));

            }


        }
        //iki sol bir geri L
        x = AnlýkX - 2;
        y = AnlýkY - 1;
        //sola ve geriye bakarken, tahta limitini 0dan say çünkü tahta 0dan KareX , 0dan KareY aralýðýnda 
        //0 dahil.


        if (x >= 0 && y >= 0)
        {
            //Baktýðýmýz kare boþ mu? veya düþman mý?
            if (tahta[x, y] == null || tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));

            }


        }



        return r;
    }
}