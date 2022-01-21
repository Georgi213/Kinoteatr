using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kinoteatr_bilet
{
    public partial class Afisha : Form
    {

        PictureBox picture1;
        PictureBox picture2;
        PictureBox picture3;
        PictureBox picture4;
        public Afisha()
        {

            this.ClientSize = new System.Drawing.Size(1120, 500);

            picture1 = new PictureBox();
            picture1.Size = new Size(220, 400);
            picture1.Location = new Point(800, 20);
            picture1.SizeMode = PictureBoxSizeMode.StretchImage;
            picture1.ImageLocation = (@"..\..\image\Harry.jpg");
            picture1.Click += picture1_Click;

            picture4 = new PictureBox();
            picture4.Size = new Size(220, 400);
            picture4.Location = new Point(1, 20);
            picture4.SizeMode = PictureBoxSizeMode.StretchImage;
            picture4.ImageLocation = (@"..\..\image\spiderman3.jpg");
            picture4.Click += picture4_Click;

            picture2 = new PictureBox();
            picture2.Size = new Size(220, 400);
            picture2.Location = new Point(250, 20);
            picture2.SizeMode = PictureBoxSizeMode.StretchImage;
            picture2.ImageLocation = (@"..\..\image\Cruella.jpg");
            picture2.Click += picture2_Click;

            picture3 = new PictureBox();
            picture3.Size = new Size(220, 400);
            picture3.Location = new Point(500, 20);
            picture3.SizeMode = PictureBoxSizeMode.StretchImage;
            picture3.ImageLocation = (@"..\..\image\batman2012.jpg");
            picture3.Click += picture3_Click;



            this.BackColor = Color.LightBlue;
            this.Controls.Add(picture1);
            this.Controls.Add(picture2);
            this.Controls.Add(picture3);
            this.Controls.Add(picture4);


        }

        private void picture1_Click(object sender, EventArgs e)
        {
            Zal_form uus_aken = new Zal_form();//запускает пустую форму
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.Show();
            string avatar = "Batman";
            using (StreamWriter srb = new StreamWriter(@"..\..\filmrecording\Movie.txt", true))
            {
                srb.WriteLine(avatar);
            }
            this.Hide();

        }

        private void picture2_Click(object sender, EventArgs e)
        {
            Zal_form uus_aken = new Zal_form();//запускает пустую форму
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.Show();
            string mc = "Spiderman";
            using (StreamWriter srb = new StreamWriter(@"..\..\filmrecording\Movie.txt", true))
            {
                srb.WriteLine(mc);
            }
            this.Hide();
        }

        private void picture3_Click(object sender, EventArgs e)
        {
            Zal_form uus_aken = new Zal_form();//запускает пустую форму
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.Show();
            string dz = "Cruella";
            using (StreamWriter srb = new StreamWriter(@"..\..\filmrecording\Movie.txt", true))
            {
                srb.WriteLine(dz);
            }
            this.Hide();
        }
        private void picture4_Click(object sender, EventArgs e)
        {
            Zal_form uus_aken = new Zal_form();//запускает пустую форму
            uus_aken.StartPosition = FormStartPosition.CenterScreen;
            uus_aken.Show();
            string dz = "Harry";
            using (StreamWriter srb = new StreamWriter(@"..\..\filmrecording\Movie.txt", true))
            {
                srb.WriteLine(dz);
            }
            this.Hide();
        }
    }
}
