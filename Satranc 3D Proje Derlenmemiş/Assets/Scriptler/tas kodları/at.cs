using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class at : taslar
{
    public override List<Vector2Int> GidilebilecekKonumlar�Bul(ref taslar[,] tahta, int KareX, int KareY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //At en kafa kar��t�r�c�

        int x = Anl�kX + 1;
        int y = Anl�kY + 2;

        //iki ileri bir sa�a L
        //Tahtan�n i�erisine mi bak�yoruz?
        if(x<KareX && y<KareY)
        {
            //Bakt���m�z kare bo� mu? veya d��man m�?
            if(tahta[x,y]==null || tahta[x,y].takim != takim)
            {
                r.Add(new Vector2Int(x,y));

            }


        }
        //bir ileri, iki sa�a L
        x = Anl�kX + 2;
        y = Anl�kY + 1;

        //iki ileri bir sa�a L
        //Tahtan�n i�erisine mi bak�yoruz?
        if (x < KareX && y < KareY)
        {
            //Bakt���m�z kare bo� mu? veya d��man m�?
            if (tahta[x, y] == null || tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));

            }


        }

        //iki ileri bir sol
        x = Anl�kX - 1;
        y = Anl�kY + 2;
        //sola ve geriye bakarken, tahta limitini 0dan say ��nk� tahta 0dan KareX , 0dan KareY aral���nda 
        //0 dahil.


        if (x >=0 && y < KareY)
        {
            //Bakt���m�z kare bo� mu? veya d��man m�?
            if (tahta[x, y] == null || tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));

            }


        }
        //�ki sol bir ileri L
        x = Anl�kX - 2;
        y = Anl�kY + 1;
        //sola ve geriye bakarken, tahta limitini 0dan say ��nk� tahta 0dan KareX , 0dan KareY aral���nda 
        //0 dahil.


        if (x >= 0 && y < KareY)
        {
            //Bakt���m�z kare bo� mu? veya d��man m�?
            if (tahta[x, y] == null || tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));

            }


        }
        //bir sa� iki geri L
        x = Anl�kX + 1;
        y = Anl�kY - 2;
        //sola ve geriye bakarken, tahta limitini 0dan say ��nk� tahta 0dan KareX , 0dan KareY aral���nda 
        //0 dahil.


        if (x <KareX && y >= 0)
        {
            //Bakt���m�z kare bo� mu? veya d��man m�?
            if (tahta[x, y] == null || tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));

            }


        }
        //�ki sa�a bir geri
        x = Anl�kX + 2;
        y = Anl�kY - 1;
        //sola ve geriye bakarken, tahta limitini 0dan say ��nk� tahta 0dan KareX , 0dan KareY aral���nda 
        //0 dahil.


        if (x < KareX && y >= 0)
        {
            //Bakt���m�z kare bo� mu? veya d��man m�?
            if (tahta[x, y] == null || tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));

            }


        }

        //bir sol iki geri
        x = Anl�kX - 1;
        y = Anl�kY - 2;
        //sola ve geriye bakarken, tahta limitini 0dan say ��nk� tahta 0dan KareX , 0dan KareY aral���nda 
        //0 dahil.


        if (x >=0 && y >= 0)
        {
            //Bakt���m�z kare bo� mu? veya d��man m�?
            if (tahta[x, y] == null || tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));

            }


        }
        //iki sol bir geri L
        x = Anl�kX - 2;
        y = Anl�kY - 1;
        //sola ve geriye bakarken, tahta limitini 0dan say ��nk� tahta 0dan KareX , 0dan KareY aral���nda 
        //0 dahil.


        if (x >= 0 && y >= 0)
        {
            //Bakt���m�z kare bo� mu? veya d��man m�?
            if (tahta[x, y] == null || tahta[x, y].takim != takim)
            {
                r.Add(new Vector2Int(x, y));

            }


        }



        return r;
    }
}