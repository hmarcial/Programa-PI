using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Prograpi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random random = new Random();
        int numero = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int Humedad = random.Next(101);
            int Temperatura = random.Next(20, 30);
            int HSuelo = random.Next(101);
            SaveFileDialog guarda = new SaveFileDialog();
            guarda.FileName = "Sensados"+numero+".txt";
            guarda.Filter = "Text File | *.txt";
            if (guarda.ShowDialog() == DialogResult.OK)
            {
                StreamWriter escritura = new StreamWriter(guarda.OpenFile());
                escritura.WriteLine("Datos sensados ");
                escritura.WriteLine("//Compañia GAH//");
                escritura.WriteLine("//////////////////////////////////////////////");
                //for (int i = 0; i < lisdale.Items.Count; i++)
                //{
                escritura.WriteLine("La Humedad fu de "+Humedad+"%");
                //}
                escritura.WriteLine("//////////////////////////////////////////////");
                escritura.WriteLine("La temperatura fue de: "+Temperatura+"°c");
                escritura.WriteLine("La humedad del suelo fue de: "+HSuelo+"%");
                escritura.Dispose();

                escritura.Close();
            }
            numero++;
        }
        Timer timer = new Timer();
        private void btnsensar_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void btnparar_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void btnarchivo_Click(object sender, EventArgs e)
        {
            string rutaFTP = "ftp://" + "192.160.2.4";
            string rutaArchivo = "C:/Users/hector/Desktop/Prograpi.txt";
            string usuario = "hector";
            string contraseña = "hola";
            try
            {
                FtpWebRequest consulta = (FtpWebRequest)FtpWebRequest.Create(rutaFTP + "/" + Path.GetFileName(rutaArchivo));
                consulta.Method = WebRequestMethods.Ftp.UploadFile;
                consulta.Credentials = new NetworkCredential(usuario, contraseña);
                consulta.UsePassive = true;
                consulta.UseBinary = true;
                consulta.KeepAlive = false;
                FileStream sube = File.OpenRead(rutaArchivo);
                byte[] buffer = new byte[sube.Length];
                sube.Read(buffer, 0, buffer.Length);
                sube.Close();
                Stream streamQuery = consulta.GetRequestStream();
                streamQuery.Write(buffer, 0, buffer.Length);
                streamQuery.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
