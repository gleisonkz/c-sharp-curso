﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestorBD
{


    public partial class Form1 : Form
    {
        //===========================================================================================================

        public static string dataBaseName = "loja.sdf";
        string pathDataBase = $"{AppDomain.CurrentDomain.BaseDirectory.ToString()}{dataBaseName}";

        //===========================================================================================================

        public Form1()
        {


            InitializeComponent();
        }

        //===========================================================================================================

        private void BtnCriarBaseDeDadosClick(object sender, EventArgs e)
        {
            GestorBD gestorBD = new GestorBD();

            List<string> parametros = new List<string>()
            {
                "CREATE TABLE Clientes(",
                "ClienteID     int not null primary key,",
                "Nome          nvarchar(50),",
                "Telefone      nvarchar(10),",
                "DtAtualizacao datetime)",
                "FIM",

                "CREATE TABLE Compras(",
                "CompraID     int not null primary key,",
                "ClienteID    int not null,",
                "QtdProduto      int,",
                "ValorUnitario      float,",
                "DtCriacao datetime," +
                "FOREIGN KEY(ClienteID) REFERENCES Clientes(ClienteID) ON DELETE CASCADE)",
                "FIM",
            };

            gestorBD.CriarBaseDados(pathDataBase, parametros, true);
        }

        //===========================================================================================================

        private void BtnSelectClick(object sender, EventArgs e)
        {


            List<GestorBD.SQLParametro> parametros = new List<GestorBD.SQLParametro>();
            parametros.Add(new GestorBD.SQLParametro("@Nome", textBox1.Text));

            string query = "SELECT * FROM Clientes WHERE nome = @Nome";

            GestorBD gestorBD = new GestorBD(pathDataBase);
            var dados = gestorBD.Select(query, parametros);






        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<GestorBD.SQLParametro> parametros = new List<GestorBD.SQLParametro>();
            parametros.Add(new GestorBD.SQLParametro("@ClienteID", 3));
            parametros.Add(new GestorBD.SQLParametro("@Nome", "Carlos"));
            parametros.Add(new GestorBD.SQLParametro("@Telefone", 35462846));
            parametros.Add(new GestorBD.SQLParametro("@DataCriacao", DateTime.Now.ToString("yyyy-MM-dd")));

            string query = "INSERT INTO Clientes VALUES" +
                " (@ClienteID," +
                "@Nome," +
                "@Telefone," +
                $"@DataCriacao)";

            GestorBD gestorBD = new GestorBD(pathDataBase);
            gestorBD.ExecuteNonQuery(query, parametros);
            MessageBox.Show("Query Executada!");
        }

        //===========================================================================================================
    }
}
