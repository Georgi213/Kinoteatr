using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;


namespace Kinoteatr_bilet
{

    public partial class Zal_vaata : Form
    {

        Label message = new Label();
        Button[] btn = new Button[4];//создали массив(список) btn - название массива
        string[] texts = new string[4];//создали массив(список) texts - название массива
        TableLayoutPanel tlp = new TableLayoutPanel();
        Button btn_tabel;
        static List<Pilet> piletid;
        int k, r;
        List<string> aboutToBuy, bought;
        static string[] read_kohad;
        readonly Image seatTaken = Image.FromFile("../../image/seatTaken.png");
        readonly Image seatAvailable = Image.FromFile("../../image/seatavailable.png");
        readonly Image seatOrange = Image.FromFile("../../image/seatOrang.png");
        

        public Zal_vaata()//пустая форма
        { }


        public string[] Ostu_piletid()
        {
            try
            {
                StreamReader f = new StreamReader(@"..\..\Piletid\piletid.txt");
                read_kohad = f.ReadToEnd().Split(';');
                //int kogus = read_kohad.Length;
                f.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return read_kohad;
           

        }





        public Zal_vaata(int read, int kohad)
        {
            seatOrange.Tag = "orang";
            seatTaken.Tag = "taken";
            seatAvailable.Tag = "green";
            this.tlp.ColumnCount = kohad;
            this.tlp.RowCount = read;
            this.tlp.ColumnStyles.Clear();
            this.tlp.RowStyles.Clear();
            int i, j;
            read_kohad = Ostu_piletid();
            piletid = new List<Pilet> { };


            
            for (i = 0; i < read; i++)
            {
                this.tlp.RowStyles.Add(new RowStyle(SizeType.Percent/*, 100 / read*/));
                this.tlp.RowStyles[i].Height = 100 / read;
            }

            for (j = 0; j < kohad; j++)
            {
                this.tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent/*, 100 / kohad*/));
                this.tlp.ColumnStyles[j].Width = 100 / kohad;
            }

            this.Size = new System.Drawing.Size(kohad * 100, read * 100);
            for (int r = 0; r < read; r++)
            {
                for (int k = 0; k < kohad; k++)
                {
                   
                    btn_tabel = new Button
                    {
                        Text = string.Format("rida {0},koht {1}", r + 1, k + 1),
                        Name = string.Format("{1}{0}", k + 1, r + 1),
                        Dock = DockStyle.Fill,
                        BackColor = Color.Green
                    };
                    
                     foreach (var item in read_kohad)
                     {

                         if (item.ToString() == btn_tabel.Name)
                         {
                           
                             btn_tabel.BackColor = Color.Red;
                             btn_tabel.Enabled = false;

                            
                        }
                    }
                    btn_tabel.Click += new EventHandler(Pileti_zapis);
                    this.tlp.Controls.Add(btn_tabel, k, r);

                  }

            }
            
            this.tlp.Dock = DockStyle.Fill;
            this.Controls.Add(tlp);
        }


        private void Saada_piletid(List<Pilet> piletid)
        {

            string pocta = Interaction.InputBox("Email", "Email");


            if (pocta !="")
            {
                var filmivaata = File.ReadLines(@"..\..\filmrecording\Movie.txt").Last();

                string text = "Apollo Solaris\nFilmi on: " + filmivaata;
                foreach (var item in piletid)
                {
                    text += "\n" + "Rida: " + item.Rida + " Koht: " + item.Koht;
                }
                text += "\nAitäh, et valisite meie kinno!\nNaudi filmi vaatamist!\nBlinov Georgi";
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(pocta));
                message.From = new MailAddress(pocta);
                message.Subject = "Ostetud piletid";
                message.Body = text;
                string email = "programmeeriminetthk@gmail.com";
                string password = "2.kuursus tarpv20";
                SmtpClient client = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(email, password),
                    EnableSsl = true,
                };
                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Email ei ole", "Email");
            }
            
        }

        private void Pileti_zapis(object sender, EventArgs e)
        {
            Button btn_click = (Button)sender;
            btn_click.BackColor = Color.Yellow;
            //MessageBox.Show(btn_click.Name.ToString());
            var rida = int.Parse(btn_click.Name[0].ToString());
            var koht = int.Parse(btn_click.Name[1].ToString());

            var vas = MessageBox.Show("Sinu pilet on: Rida: " + rida + " Koht: " + koht, "Kas ostad?", MessageBoxButtons.YesNo);
            if (vas == DialogResult.Yes)
            {
                btn_click.BackColor = Color.Red;
                btn_click.Enabled = false;
                try
                {
                    Pilet pilet = new Pilet(rida, koht);
                    piletid.Add(pilet);
                    StreamWriter ost = new StreamWriter(@"..\..\Piletid\piletid.txt", true);
                    ost.Write(btn_click.Name.ToString() + ';');
                    ost.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (vas == DialogResult.No)
            {
                btn_click.BackColor = Color.Green;
            };

            if (MessageBox.Show("Sul on ostetud: " + piletid.Count() + " piletid", "Kas tahad saada neid e-mailile?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                Saada_piletid(piletid);
            }
        }





    }
}
