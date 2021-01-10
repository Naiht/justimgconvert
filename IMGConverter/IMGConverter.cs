using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace IMGConverter
{
    public partial class IMGConverter : Form
    {

        List<string> list = new List<string>();

        string res = "";

        

        public IMGConverter()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);


        private void Form1_Load(object sender, EventArgs e)
        {
            listaBox.DropDownStyle = ComboBoxStyle.DropDownList;
            btnRemp.Enabled = false;
            btnConv.Enabled = false;
            btnSelec.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListaCarga.Items.Clear();
            list.Clear();
            openFileDialog2.Multiselect = true;
            openFileDialog2.Filter = "Jpg image(*.jpg)|*.jpg|" + "BmpPT image(*.bmp)|*.bmp|" + "Png image(*.png)|*.png|" +
                "Gif image(*.gif)|*.gif|" + "Emf image(*.emf)|*.emf|" + "Exif image(*.exif)|*.exif|" + "Icon image(*.ico)|*.ico|" +
                "Wmf image(*.wmf)|*.wmf|" + "Tiff image(*.tiff)|*.tiff|" + "All Files(*.*)|*.*";
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                string[] info = openFileDialog2.FileNames;
                foreach (string s in info)
                {
                    ListaCarga.Items.Add(Path.GetFileName(s));
                    list.Add(s);
                }

                btnRemp.Enabled = true;
                btnSelec.Enabled = true;
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            String info = "";
            info = "IMG Converter v 1.0" + "\nDesarollado por Naiht 2019";
            MessageBox.Show(info,"",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnSelec_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                res = folderBrowserDialog1.SelectedPath;
            }
            btnConv.Enabled = true;
        }

        private void btnConv_Click(object sender, EventArgs e)
        {
            String formato = "";
            int f = 0;

            if (listaBox.SelectedIndex >= 0)
            {
                formato = listaBox.Items[listaBox.SelectedIndex].ToString();

                String ruta = "";

                foreach (string s in list)
                {
                    Image img = Image.FromFile(@s);

                    ruta = res + "\\" + Path.GetFileNameWithoutExtension(s) + "." + formato.ToLower();

                    switch (formato)
                    {
                        case "Bmp":
                            img.Save(@ruta, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                        case "Jpg":
                            img.Save(@ruta, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        case "Gif":
                            img.Save(@ruta, System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                        case "Png":
                            img.Save(@ruta, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                        case "Emf":
                            img.Save(@ruta, System.Drawing.Imaging.ImageFormat.Emf);
                            break;
                        case "Exif":
                            img.Save(@ruta, System.Drawing.Imaging.ImageFormat.Exif);
                            break;
                        case "Ico":
                            img.Save(@ruta, System.Drawing.Imaging.ImageFormat.Icon);
                            break;
                        case "Wmf":
                            img.Save(@ruta, System.Drawing.Imaging.ImageFormat.Wmf);
                            break;
                        case "Tiff":
                            img.Save(@ruta, System.Drawing.Imaging.ImageFormat.Tiff);
                            break;
                    }
                    f++;
                }

                MessageBox.Show("Imagenes convertidas con exito.");
            }
            else {

                MessageBox.Show("Seleccione el formato al que desea convertir las imagenes.");
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle,0x112,0xf012,0);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
