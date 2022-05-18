using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fil : taslar
{

    public override List<Vector2Int> GidilebilecekKonumlar�Bul(ref taslar[,] tahta, int KareX, int KareY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //�arprazlar� tara, kar��na hedef ��karsa, d��man ise dahil et, m�ttefik ise dahil etme.

        //Sa� �st k��e
        //looplar hem x ve hem y i�in �al���r.

        //Bunu b�yle yapmasak da olurdu gibi ama yinede X ve Yyi ay�rd�m

        for (int x = Anl�kX+1, y= Anl�kY+1 ; x < KareX && y<KareY; x++,y++)
        {

            //Bakt���m�z kare bo� mu? Bo� ise ekle.
            if(tahta[x,y]==null)
            {

                r.Add(new Vector2Int(x,y));

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
        for (int x = Anl�kX + 1, y = Anl�kY - 1; x < KareX && y >=0 ; x++, y--)
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
        for (int x = Anl�kX - 1, y = Anl�kY + 1; x >=0 && y < KareY; x--, y++)
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