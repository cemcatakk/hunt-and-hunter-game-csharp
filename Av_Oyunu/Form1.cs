using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Av_Oyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int avsayisi = 7; //Av ve avcı sayılarını görebilmek için değişkende tutuyoruz.
        int avcisayisi = 2;
        Random rnd = new Random(); //Rastgele sayı üretmek için değişkenimizi tanımlıyoruz
        void hareket(int hiz1, int hiz2, Control kontrol) //Av ve Avcılarımızı hareket ettirecek methodumuz.
        {
            int kontrolrow = tableLayoutPanel1.GetRow(kontrol); //kontrol değişkeninin Layout Panel üzerindeki pozisyonunu alıyoruz.
            int kontrolcol = tableLayoutPanel1.GetColumn(kontrol);
            if (kontrolrow + hiz1 < 0 || kontrolcol + hiz2 < 0) //Olası durumda av ve avcıların panel sınırı dışında veri getirmemesi için sorgu yapıyoruz.
            {
                hiz1 += 2; //panel dışına çıkabilecek değer var ise çıkmaması için değeri arttırıyoruz.
                hiz2 += 2;
            }
            Control hedef = tableLayoutPanel1.GetControlFromPosition((kontrolcol + hiz2), (kontrolrow + hiz1)); //av veya avcımızın hareket edeceği pozisyonun içindeki Control'u bulduruyoruz.
            if (hedef != null) //Olası hatadan kaçınmak için hedefte kontrol olup olmadığına bakıyoruz.
            {
                if (kontrol.Tag == "avci" && hedef.Tag == "av") //Avcı ve av birbiri üzerine gelirse Avcı avını avlıyor ve "Av" Avcıya dönüşüyor
                {
                    hedef.Tag = "avci"; //Av'ın program tarafından Avcı olarak görünmesini sağlıyoruz
                    hedef.BackColor = Color.Red; //Av'ın rengini Avcı rengine dönüştürüyoruz.

                    avsayisi--; //Av sayısını azaltıp avcı sayısını arttırıyoruz.
                    avcisayisi++;
                }
                tableLayoutPanel1.SetCellPosition(kontrol, tableLayoutPanel1.GetCellPosition(hedef)); //Kontrolün(av veya avcı) pozisyonunu değiştiriyoruz.
                tableLayoutPanel1.SetRow(hedef, kontrolrow);//Kontrolün gideceği noktatadi diğer kontrolün(label) pozisyonunu değiştiriyoruz.
                tableLayoutPanel1.SetColumn(hedef, kontrolcol);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control item in tableLayoutPanel1.Controls) //Panel içindeki kontrollere foreach döngüsü ile bakıyoruz
            {
                if (item != null)
                {
                    if (item.Tag == "avci") //Tag'i avci olan Label'lar 2 birim rastgele ilerletiliyor
                    {
                        hareket(rnd.Next(-2, 3), rnd.Next(-2, 3), item);

                    }
                    else if (item.Tag == "av") //Tag'i av olan Label'lar 1 birim rastgele ilerletiliyor.
                    {
                        hareket(rnd.Next(-1, 2), rnd.Next(-1, 2), item);
                    }
                    if (avsayisi == 0) //Av sayısı 0 olduğunda oyun bitti mesajı geliyor.
                    {
                        MessageBox.Show("Oyun Bitti!");
                    }
                }

            }
            avsayisilabel.Text = avsayisi.ToString(); //Label'lara Avcı ve Av sayısını aktarıyoruz.
            avcisayisilabel.Text = avcisayisi.ToString();
        }

        private void label147_Click(object sender, EventArgs e)
        {

        }
    }
}

