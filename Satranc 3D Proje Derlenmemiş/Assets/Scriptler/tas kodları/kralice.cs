using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kralice : taslar
{
    


    public override List<Vector2Int> GidilebilecekKonumlarýBul(ref taslar[,] tahta, int KareX, int KareY)
    {
        //Vezir/Kraliçe kale+fil. Kodlarý kopyala yapýþtýr
        List<Vector2Int> r = new List<Vector2Int>();

        //Aþaðý yönü tara. For loop ile karþýna bir taþ çýkana kadar tarayýp, taþ çýkýnca, taþ düþman ise
        //O taþýn olduðu kareyi dahil et, deðilse etme.

        for (int i = AnlýkY - 1; i >= 0; i--)
        {
            //Taranan kare boþ mu? Boþ ise yapýlabilecek hamleler listesine ekle.
            if (tahta[AnlýkX, i] == null)

            {
                r.Add(new Vector2Int(AnlýkX, i));
            }
            //Taranan kare boþ deðil ise, karedeki hedef düþman mý? Düþman ise listeye ekle ve loopdan çýk

            if (tahta[AnlýkX, i] != null && tahta[AnlýkX, i].takim != takim)

            {
                r.Add(new Vector2Int(AnlýkX, i));

                break;
            }
            //Eðer taranan kare boþ deðil ise ve o karadeki hedef müttefik ise, loopdan çýk.
            if (tahta[AnlýkX, i] != null && tahta[AnlýkX, i].takim == takim)

            {

                break;
            }

        }
        //Yukarý yön

        for (int i = AnlýkY + 1; i < KareX; i++)
        {
            //Taranan kare boþ mu? Boþ ise yapýlabilecek hamleler listesine ekle.
            if (tahta[AnlýkX, i] == null)

            {
                r.Add(new Vector2Int(AnlýkX, i));
            }
            //Taranan kare boþ deðil ise, karedeki hedef düþman mý? Düþman ise listeye ekle ve loopdan çýk

            if (tahta[AnlýkX, i] != null && tahta[AnlýkX, i].takim != takim)

            {
                r.Add(new Vector2Int(AnlýkX, i));

                break;
            }
            //Eðer taranan kare boþ deðil ise ve o karadeki hedef müttefik ise, loopdan çýk.
            if (tahta[AnlýkX, i] != null && tahta[AnlýkX, i].takim == takim)

            {

                break;
            }

        }

        //Sol

        for (int i = AnlýkX - 1; i >= 0; i--)
        {
            //Taranan kare boþ mu? Boþ ise yapýlabilecek hamleler listesine ekle.
            if (tahta[i, AnlýkY] == null)

            {
                r.Add(new Vector2Int(i, AnlýkY));
            }
            //Taranan kare boþ deðil ise, karedeki hedef düþman mý? Düþman ise listeye ekle ve loopdan çýk

            if (tahta[i, AnlýkY] != null && tahta[i, AnlýkY].takim != takim)

            {
                r.Add(new Vector2Int(i, AnlýkY));

                break;
            }
            //Eðer taranan kare boþ deðil ise ve o karadeki hedef müttefik ise, loopdan çýk.
            if (tahta[i, AnlýkY] != null && tahta[i, AnlýkY].takim == takim)

            {

                break;
            }

        }

        //sað

        for (int i = AnlýkX + 1; i < KareX; i++)
        {
            //Taranan kare boþ mu? Boþ ise yapýlabilecek hamleler listesine ekle.
            if (tahta[i, AnlýkY] == null)

            {
                r.Add(new Vector2Int(i, AnlýkY));
            }
            //Taranan kare boþ deðil ise, karedeki hedef düþman mý? Düþman ise listeye ekle ve loopdan çýk

            if (tahta[i, AnlýkY] != null && tahta[i, AnlýkY].takim != takim)

            {
                r.Add(new Vector2Int(i, AnlýkY));

                break;
            }
            //Eðer taranan kare boþ deðil ise ve o karadeki hedef müttefik ise, loopdan çýk.
            if (tahta[i, AnlýkY] != null && tahta[i, AnlýkY].takim == takim)

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
