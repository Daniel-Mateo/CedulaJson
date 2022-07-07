using Newtonsoft.Json;
using System.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cedula_winforms2
{
    public partial class Form1 : Form
    {
        List<Concept>? Ciudadanos = new List<Concept>();
        public bool Adding { get; set; } = true;
        public object JsonConvert { get; private set; }

        public Form1()
        {
            InitializeComponent();

            GetRecords();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void SexoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void GenerateNewID()
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtNumeroCedula.Text = String.Empty;
            txtNombreCompleto.Text = String.Empty;
            txtNumeroCedula.Text = String.Empty;
            txtLugarNacimiento.Text = String.Empty;
            txtNacionalidad.Text = String.Empty;
            rbMasculino.Text = String.Empty;
            cbSangre.Text = String.Empty;
            cbEstadoCivil.Text = String.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            var json = string.Empty;
            var conceptList = new List<Concept>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\concept.js";

            if(File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile);
                conceptList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Concept>>(json);
            }

            var concept = new Concept();
            if(Adding)
            {
                concept = new Concept
                {
                    ID = int.Parse(txtNumeroCedula.Text),
                    NombreCompleto = txtNombreCompleto.Text,
                    NumeroCedula = int.Parse(txtNumeroCedula.Text),
                    LugarNacimiento = txtLugarNacimiento.Text,
                    Nacimiento = dtpNacimiento.Value,
                    Nacionalidad = txtNacionalidad.Text,
                    Sexo = rbMasculino.Text,
                    Sangre = cbSangre.Text,
                    EstadoCivil = cbEstadoCivil.Text
                };
            }
            else
            {
                var ID = int.Parse(txtNumeroCedula.Text);
                concept = conceptList.FirstOrDefault(x=> x.ID == ID);
                if(concept != null)
                {
                    conceptList.Remove(concept);

                    concept.ID = int.Parse(txtNumeroCedula.Text);
                    concept.NombreCompleto = txtNombreCompleto.Text;
                    concept.NumeroCedula = int.Parse(txtNumeroCedula.Text);
                    concept.LugarNacimiento = txtLugarNacimiento.Text;
                    concept.Nacimiento = dtpNacimiento.Value;
                    concept.Nacionalidad = txtNacionalidad.Text;
                    concept.Sexo = rbMasculino.Text;
                    concept.Sangre = cbSangre.Text;
                    concept.EstadoCivil = cbEstadoCivil.Text;
                }
            }
            conceptList.Add(concept);

            json = Newtonsoft.Json.JsonConvert.SerializeObject(conceptList);

            var sw = new StreamWriter(pathFile, false, Encoding.UTF8);
            sw.Write(json);
            sw.Close();

            MessageBox.Show("Registro almacenado");

            ClearFields();

            GetRecords();

        }

        private void GetRecords()
        {
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\concepts.json";
            var conceptList = new List<Concept>();

            if (File.Exists(pathFile))
            {
                var json = File.ReadAllText(pathFile, Encoding.UTF8);
                conceptList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Concept>>(json);
            }

            dgvCiudadanos.DataSource = conceptList;

        }

        private void GetCiudadanos()
        {
            dgvCiudadanos.DataSource = null;
            dgvCiudadanos.DataSource = Ciudadanos;
        }
    }
    public class Concept
    {
        public int ID { get; set; }
        public string? NombreCompleto { get; set; }
        public int NumeroCedula { get; set; }
        public string? LugarNacimiento { get; set; }
        public DateTime Nacimiento { get; set; }
        public string? Nacionalidad { get; set; }
        public string? Sexo { get; set; }
        public string? Sangre { get; set; }
        public string? EstadoCivil { get; set; }
        public string? Ocupacion { get; set; }


    }
}