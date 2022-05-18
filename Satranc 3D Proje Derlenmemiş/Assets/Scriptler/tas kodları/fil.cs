using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fil : taslar
{

    public override List<Vector2Int> GidilebilecekKonumlarýBul(ref taslar[,] tahta, int KareX, int KareY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //Çarprazlarý tara, karþýna hedef çýkarsa, düþman ise dahil et, müttefik ise dahil etme.

        //Sað üst köþe
        //looplar hem x ve hem y için çalýþýr.

        //Bunu böyle yapmasak da olurdu gibi ama yinede X ve Yyi ayýrdým

        for (int x = AnlýkX+1, y= AnlýkY+1 ; x < KareX && y<KareY; x++,y++)
        {

            //Baktýðýmýz kare boþ mu? Boþ ise ekle.
            if(tahta[x,y]==null)
            {

                r.Add(new Vector2Int(x,y));

            }
            //Baktýðýmýz kare boþ deðilse o karadeki hedef düþman mý? Düþman ise dahil et ve loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //müttefik ise sadece loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {
                
                break;

            }

        }
        //sað alt köþe.
        for (int x = AnlýkX + 1, y = AnlýkY - 1; x < KareX && y >=0 ; x++, y--)
        {

            //Baktýðýmýz kare boþ mu? Boþ ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Baktýðýmýz kare boþ deðilse o karadeki hedef düþman mý? Düþman ise dahil et ve loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //müttefik ise sadece loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }

        //sol üst köþe
        for (int x = AnlýkX - 1, y = AnlýkY + 1; x >=0 && y < KareY; x--, y++)
        {

            //Baktýðýmýz kare boþ mu? Boþ ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Baktýðýmýz kare boþ deðilse o karadeki hedef düþman mý? Düþman ise dahil et ve loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //müttefik ise sadece loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }

        //Sol alt köþe

        for (int x = AnlýkX - 1, y = AnlýkY - 1; x >= 0 && y >= 0; x--, y--)
        {

            //Baktýðýmýz kare boþ mu? Boþ ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Baktýðýmýz kare boþ deðilse o karadeki hedef düþman mý? Düþman ise dahil et ve loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //müttefik ise sadece loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }
        for (int x = AnlýkX + 1, y = AnlýkY + 1; x < KareX && y < KareY; x++, y++)
        {

            //Baktýðýmýz kare boþ mu? Boþ ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Baktýðýmýz kare boþ deðilse o karadeki hedef düþman mý? Düþman ise dahil et ve loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //müttefik ise sadece loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }
        //sað alt köþe.
        for (int x = AnlýkX + 1, y = AnlýkY - 1; x < KareX && y >= 0; x++, y--)
        {

            //Baktýðýmýz kare boþ mu? Boþ ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Baktýðýmýz kare boþ deðilse o karadeki hedef düþman mý? Düþman ise dahil et ve loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //müttefik ise sadece loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }

        //sol üst köþe
        for (int x = AnlýkX - 1, y = AnlýkY + 1; x >= 0 && y < KareY; x--, y++)
        {

            //Baktýðýmýz kare boþ mu? Boþ ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Baktýðýmýz kare boþ deðilse o karadeki hedef düþman mý? Düþman ise dahil et ve loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //müttefik ise sadece loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }

        //Sol alt köþe

        for (int x = AnlýkX - 1, y = AnlýkY - 1; x >= 0 && y >= 0; x--, y--)
        {

            //Baktýðýmýz kare boþ mu? Boþ ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Baktýðýmýz kare boþ deðilse o karadeki hedef düþman mý? Düþman ise dahil et ve loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //müttefik ise sadece loopdan çýk.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }

        return r;
    }
}