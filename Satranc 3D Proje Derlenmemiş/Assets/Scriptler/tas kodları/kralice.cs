using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kralice : taslar
{
    


    public override List<Vector2Int> GidilebilecekKonumlar�Bul(ref taslar[,] tahta, int KareX, int KareY)
    {
        //Vezir/Krali�e kale+fil. Kodlar� kopyala yap��t�r
        List<Vector2Int> r = new List<Vector2Int>();

        //A�a�� y�n� tara. For loop ile kar��na bir ta� ��kana kadar taray�p, ta� ��k�nca, ta� d��man ise
        //O ta��n oldu�u kareyi dahil et, de�ilse etme.

        for (int i = Anl�kY - 1; i >= 0; i--)
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

        for (int i = Anl�kX + 1; i < KareX; i++)
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
        for (int x = Anl�kX + 1, y = Anl�kY + 1; x < KareX && y < KareY; x++, y++)
        {

            //Bakt���m�z kare bo� mu? Bo� ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Bakt���m�z kare bo� de�ilse o karadeki hedef d��man m�? D��man ise dahil et ve loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //m�ttefik ise sadece loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }
        //sa� alt k��e.
        for (int x = Anl�kX + 1, y = Anl�kY - 1; x < KareX && y >= 0; x++, y--)
        {

            //Bakt���m�z kare bo� mu? Bo� ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Bakt���m�z kare bo� de�ilse o karadeki hedef d��man m�? D��man ise dahil et ve loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //m�ttefik ise sadece loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }

        //sol �st k��e
        for (int x = Anl�kX - 1, y = Anl�kY + 1; x >= 0 && y < KareY; x--, y++)
        {

            //Bakt���m�z kare bo� mu? Bo� ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Bakt���m�z kare bo� de�ilse o karadeki hedef d��man m�? D��man ise dahil et ve loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //m�ttefik ise sadece loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }

        //Sol alt k��e

        for (int x = Anl�kX - 1, y = Anl�kY - 1; x >= 0 && y >= 0; x--, y--)
        {

            //Bakt���m�z kare bo� mu? Bo� ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Bakt���m�z kare bo� de�ilse o karadeki hedef d��man m�? D��man ise dahil et ve loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //m�ttefik ise sadece loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }
        for (int x = Anl�kX + 1, y = Anl�kY + 1; x < KareX && y < KareY; x++, y++)
        {

            //Bakt���m�z kare bo� mu? Bo� ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Bakt���m�z kare bo� de�ilse o karadeki hedef d��man m�? D��man ise dahil et ve loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //m�ttefik ise sadece loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }
        //sa� alt k��e.
        for (int x = Anl�kX + 1, y = Anl�kY - 1; x < KareX && y >= 0; x++, y--)
        {

            //Bakt���m�z kare bo� mu? Bo� ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Bakt���m�z kare bo� de�ilse o karadeki hedef d��man m�? D��man ise dahil et ve loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //m�ttefik ise sadece loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }

        //sol �st k��e
        for (int x = Anl�kX - 1, y = Anl�kY + 1; x >= 0 && y < KareY; x--, y++)
        {

            //Bakt���m�z kare bo� mu? Bo� ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Bakt���m�z kare bo� de�ilse o karadeki hedef d��man m�? D��man ise dahil et ve loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //m�ttefik ise sadece loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }

        //Sol alt k��e

        for (int x = Anl�kX - 1, y = Anl�kY - 1; x >= 0 && y >= 0; x--, y--)
        {

            //Bakt���m�z kare bo� mu? Bo� ise ekle.
            if (tahta[x, y] == null)
            {

                r.Add(new Vector2Int(x, y));

            }
            //Bakt���m�z kare bo� de�ilse o karadeki hedef d��man m�? D��man ise dahil et ve loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));
                break;

            }
            //m�ttefik ise sadece loopdan ��k.
            if (tahta[x, y] != null && tahta[x, y].takim == takim)
            {

                break;

            }

        }

        return r;
    }
}
