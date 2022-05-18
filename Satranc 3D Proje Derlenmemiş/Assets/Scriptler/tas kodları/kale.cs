using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kale : taslar
{
    public override List<Vector2Int> GidilebilecekKonumlar�Bul(ref taslar[,] tahta, int KareX, int KareY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //A�a�� y�n� tara. For loop ile kar��na bir ta� ��kana kadar taray�p, ta� ��k�nca, ta� d��man ise
        //O ta��n oldu�u kareyi dahil et, de�ilse etme.

        for (int i = Anl�kY-1; i>=0; i--)
        {
            //Taranan kare bo� mu? Bo� ise yap�labilecek hamleler listesine ekle.
            if (tahta[Anl�kX, i] == null)

            { r.Add(new Vector2Int(Anl�kX, i));
            }
            //Taranan kare bo� de�il ise, karedeki hedef d��man m�? D��man ise listeye ekle ve loopdan ��k

            if (tahta[Anl�kX, i] != null && tahta[Anl�kX, i].takim != takim)

            {
                r.Add(new Vector2Int(Anl�kX, i));

                break;
            }
            //E�er taranan kare bo� de�il ise ve o karadeki hedef m�ttefik ise, loopdan ��k.
            if (tahta[Anl�kX, i] != null && tahta[Anl�kX, i].takim == takim)

            {

                break;
            }

        }
        //Yukar� y�n

        for (int i = Anl�kY + 1; i < KareX; i++)
        {
            //Taranan kare bo� mu? Bo� ise yap�labilecek hamleler listesine ekle.
            if (tahta[Anl�kX, i] == null)

            {
                r.Add(new Vector2Int(Anl�kX, i));
            }
            //Taranan kare bo� de�il ise, karedeki hedef d��man m�? D��man ise listeye ekle ve loopdan ��k

            if (tahta[Anl�kX, i] != null && tahta[Anl�kX, i].takim != takim)

            {
                r.Add(new Vector2Int(Anl�kX, i));

                break;
            }
            //E�er taranan kare bo� de�il ise ve o karadeki hedef m�ttefik ise, loopdan ��k.
            if (tahta[Anl�kX, i] != null && tahta[Anl�kX, i].takim == takim)

            {

                break;
            }

        }

        //Sol

        for (int i = Anl�kX - 1; i >= 0; i--)
        {
            //Taranan kare bo� mu? Bo� ise yap�labilecek hamleler listesine ekle.
            if (tahta[i, Anl�kY] == null)

            {
                r.Add(new Vector2Int(i, Anl�kY));
            }
            //Taranan kare bo� de�il ise, karedeki hedef d��man m�? D��man ise listeye ekle ve loopdan ��k

            if (tahta[i, Anl�kY] != null && tahta[i, Anl�kY].takim != takim)

            {
                r.Add(new Vector2Int(i, Anl�kY));

                break;
            }
            //E�er taranan kare bo� de�il ise ve o karadeki hedef m�ttefik ise, loopdan ��k.
            if (tahta[i, Anl�kY] != null && tahta[i, Anl�kY].takim == takim)

            {

                break;
            }

        }

        //sa�

        for (int i = Anl�kX+1; i < KareX; i++)
        {
            //Taranan kare bo� mu? Bo� ise yap�labilecek hamleler listesine ekle.
            if (tahta[i, Anl�kY] == null)

            {
                r.Add(new Vector2Int(i, Anl�kY));
            }
            //Taranan kare bo� de�il ise, karedeki hedef d��man m�? D��man ise listeye ekle ve loopdan ��k

            if (tahta[i, Anl�kY] != null && tahta[i, Anl�kY].takim != takim)

            {
                r.Add(new Vector2Int(i, Anl�kY));

                break;
            }
            //E�er taranan kare bo� de�il ise ve o karadeki hedef m�ttefik ise, loopdan ��k.
            if (tahta[i, Anl�kY] != null && tahta[i, Anl�kY].takim == takim)

            {

                break;
            }

        }


        return r;
    }
}