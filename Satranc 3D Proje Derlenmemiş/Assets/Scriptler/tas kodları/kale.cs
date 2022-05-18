using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kale : taslar
{
    public override List<Vector2Int> GidilebilecekKonumlarýBul(ref taslar[,] tahta, int KareX, int KareY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //Aþaðý yönü tara. For loop ile karþýna bir taþ çýkana kadar tarayýp, taþ çýkýnca, taþ düþman ise
        //O taþýn olduðu kareyi dahil et, deðilse etme.

        for (int i = AnlýkY-1; i>=0; i--)
        {
            //Taranan kare boþ mu? Boþ ise yapýlabilecek hamleler listesine ekle.
            if (tahta[AnlýkX, i] == null)

            { r.Add(new Vector2Int(AnlýkX, i));
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

        for (int i = AnlýkX+1; i < KareX; i++)
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


        return r;
    }
}