using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McCluskey
{
    class Program
    {
        public static byte[] ikilik(int sayi,int basamak)
        {
            byte[] donusmus = new byte[basamak];
            for(int i = 0; i < basamak; i++)
            {
                donusmus[basamak-i-1] =Convert.ToByte( sayi % 2);
                sayi /= 2;
            }
            return donusmus;
        }
        public static byte KacBit(byte[] Dizi)
        {
            byte enb = 0,us=1;
            for(int i = 0; i < Dizi.Length; i++)
            {
                if (enb < Dizi[i]) enb = Dizi[i];
            }
            while (enb > Math.Pow(2, us)) { us++; }
            return us;
        }
        public static void TabloYazdir(byte[,] Tablo)
        {
            for(int i = 0; i < Tablo.GetLength(0); i++)
            {
                if (i == 0) { Console.WriteLine("        a b c d  SıfırSayısı"); }
                for (int j = 0; j < Tablo.GetLength(1); j++)
                {
                    if (j == 0) Console.Write(Tablo[i, j] + "\t");
                    else Console.Write(Tablo[i, j]+" ");
                    if (j == Tablo.GetLength(1) - 2) Console.Write("  ");
                }
                Console.WriteLine();
            }
        }
        public static byte BirlerinSayisi(byte[] Dizi)
        {
            byte sayi = 0;
            for(int i = 0; i < Dizi.Length; i++)
            {
                if (Dizi[i] == 1) sayi++;
            }
            return sayi;
        }
        public static void Genel_Yazdirim(byte[,] Dizi)
        {
            for(int i = 0; i < Dizi.GetLength(0); i++)
            {
                for(int j = 0; j < Dizi.GetLength(1); j++)
                {
                    Console.Write(Dizi[i, j]);
                }
                Console.WriteLine();
            }
        }
        public static bool Eslesiyormu(byte[] Dizi1,byte[] Dizi2)
        {
            byte sayac=0;
            for(int i = 0; i < Dizi1.Length; i++)
            {
                if (Dizi1[i] != Dizi2[i]) sayac++;
            }
            if (sayac < 2) return true;
            else return false;
        }
        public static byte[] Farkliyi_Sil(byte[] Dizi1,byte[] Dizi2)
        {
            for(int i = 0; i < Dizi1.Length; i++)
            {
                if (Dizi1[i] == Dizi2[i]) Dizi2[i] = Dizi2[i];
                else Dizi2[i] = 6;
            }
            return Dizi2;
        }
        public static string Indirgenmis_Hal(byte[,] Tablo,bool Mintermmi)
        {
            string degiskenler = "abcdefgh";
            string sonuc = "";
            for(int i = 0; i < Tablo.GetLength(0); i++)
            {
                for(int j = 0; j < Tablo.GetLength(1); j++)
                {
                    if (Tablo[i, j] == 1) sonuc += degiskenler[j]+"*";
                    else if(Tablo[i,j]==0) sonuc +="-"+degiskenler[j];
                }
                sonuc += "+";
            }
            int boyut = sonuc.Length;
            string yedeksonuc = "";
            for (int i = 0; i < boyut; i++) yedeksonuc += "+";//boyle yaparak eger hepsi sadelesmisse + ları yazmasını engelleriz
            if (yedeksonuc == sonuc && Mintermmi == true) return "1";
            else if (yedeksonuc == sonuc && Mintermmi == false) return "0";
            else return sonuc;
        }
        public static byte[,] Eslesenleri_İsaretle(byte[,] Tablo,byte Boyut)
        {

            byte[] eldeki = new byte[Tablo.GetLength(1)];
            byte[] gecici = new byte[Tablo.GetLength(1)];

            byte[,] YeniDizi = new byte[Boyut, Tablo.GetLength(1)];
            byte[] birlesmis = new byte[Tablo.GetLength(1)];

            byte sayac = 0;//yeni olusacak tablo içinde gezinmek için

            for (int Satir = 0; Satir < Tablo.GetLength(0); Satir++)//eldeki elemanlar sizideki butun satırlar olmalıdır...
            {
                for (int elde = 0; elde < Tablo.GetLength(1); elde++) eldeki[elde] = Tablo[Satir, elde];//elde bitlerini aldırdım

                for (int Ksatir = Satir + 1; Ksatir < Tablo.GetLength(0); Ksatir++)//KSatir:karsilastirma satirlari
                {
                    for (int gec = 0; gec < Tablo.GetLength(1); gec++) gecici[gec] = Tablo[Ksatir, gec];//karsilastirilacak gecici bitleri aldırdım

                    if (Eslesiyormu(eldeki, gecici))//eger eslesiyorsa 
                    {
                        birlesmis = Farkliyi_Sil(eldeki, gecici);//yeni dizi olustur 
                        for (int yukle = 0; yukle < birlesmis.Length; yukle++)//bu diziyi yeni tabloya koy
                        {
                            YeniDizi[sayac, yukle] = birlesmis[yukle];
                        }
                        sayac++;
                    }
                }
            }
            return YeniDizi;
        }
        public static byte Eslesme_Sayisi(byte[,] Tablo)
        {
            byte[] eldeki = new byte[Tablo.GetLength(1)];
            byte[] gecici = new byte[Tablo.GetLength(1)];
            byte sayac = 0;
            for (int d = 0; d < Tablo.GetLength(0); d++)//eldeki elemanlar sizideki butun satırlar olmalıdır...
            {
                for (int e = 0; e < Tablo.GetLength(1); e++) eldeki[e] = Tablo[d, e];//elde bitlerini aldırdım
                for (int i = d+1; i < Tablo.GetLength(0); i++)
                {
                    for (int g = 0; g < Tablo.GetLength(1); g++) gecici[g] = Tablo[i, g];//karsilastirilacak gecici bitleri aldırdım

                    if (Eslesiyormu(eldeki, gecici)) sayac++;
                }
            }
            if (sayac == 0) return (byte)Tablo.GetLength(0);//eger eslesme yoksa 
            else return sayac;
        }
        static void Main(string[] args)
        {
            byte[] Mintermler = {3,7,11,12,13,14,15};
            byte BitSayisi = KacBit(Mintermler);
            #region İlk Görterme //region yaparak gizledim
            byte[,] Tablo = new byte[Mintermler.Length, BitSayisi + 2];//ilk gösterge tablosu olusturma 
            int i, j;
            for(i = 0; i < Mintermler.Length; i++)//olusturulan gösterge tablosunu doldurma
            {
                byte[] ikiligi = ikilik(Mintermler[i], BitSayisi);
                Tablo[i, 0] = Mintermler[i];
                for(j = 0; j < BitSayisi ; j++) { Tablo[i, j + 1] = ikiligi[j]; }
                Tablo[i, j + 1] = BirlerinSayisi(ikiligi);
            }
            TabloYazdir(Tablo);
            #endregion
            byte[,] Bit_Diziler = new byte[Mintermler.Length,BitSayisi];
            for(int k = 0; k < Mintermler.Length; k++)
            {
                byte[] ikilikSayi = ikilik(Mintermler[k],BitSayisi);
                for(int l = 0; l < BitSayisi; l++)
                {
                    Bit_Diziler[k, l] = ikilikSayi[l];
                }
            }
            byte ESayisi = Eslesme_Sayisi(Bit_Diziler);//eslesme sayisi....

            byte[,] tDensonra = Eslesenleri_İsaretle(Bit_Diziler,ESayisi);
            while (Eslesme_Sayisi(Bit_Diziler) != 2 && Eslesme_Sayisi(Bit_Diziler) <= Bit_Diziler.GetLength(0) * 2 )
            {
                Bit_Diziler = Eslesenleri_İsaretle(Bit_Diziler, ESayisi);
                ESayisi = Eslesme_Sayisi(Bit_Diziler);
                Genel_Yazdirim(Bit_Diziler);
                Console.WriteLine();
            }
            Bit_Diziler = Eslesenleri_İsaretle(Bit_Diziler, ESayisi);
            ESayisi = Eslesme_Sayisi(Bit_Diziler);
            Genel_Yazdirim(Bit_Diziler);
            Console.WriteLine();
            Console.WriteLine("F="+Indirgenmis_Hal(Bit_Diziler,true));
        }
    }
}
