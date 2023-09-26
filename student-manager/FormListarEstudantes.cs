﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student_manager
{
    public partial class FormListarEstudantes : Form
    {
        public FormListarEstudantes()
        {
            InitializeComponent();
        }

        // Instanciação de uma classe, ou seja, criação de um objeto.
        Estudante estudante = new Estudante();

        private void FormListarEstudantes_Load(object sender, EventArgs e)
        {
            // Preencher a tabela com os dados dos estudantes.
            MySqlCommand comando = new MySqlCommand("SELECT * FROM `estudantes`");
            dataGridViewLista.ReadOnly = true;
            DataGridViewImageColumn colunaDeImagens = new DataGridViewImageColumn();
            dataGridViewLista.RowTemplate.Height = 80;
            dataGridViewLista.DataSource = estudante.pegarEstudantes(comando);
            colunaDeImagens = (DataGridViewImageColumn)dataGridViewLista.Columns[7];
            colunaDeImagens.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridViewLista.AllowUserToAddRows = false;
        }

        private void buttonAtualizar_Click(object sender, EventArgs e)
        {
            // Atualiza a tabela de estudantes.
            MySqlCommand comando = new MySqlCommand("SELECT * FROM `estudantes`");
            dataGridViewLista.ReadOnly = true;
            DataGridViewImageColumn colunaDeImagens = new DataGridViewImageColumn();
            dataGridViewLista.RowTemplate.Height = 80;
            dataGridViewLista.DataSource = estudante.pegarEstudantes(comando);
            colunaDeImagens = (DataGridViewImageColumn)dataGridViewLista.Columns[7];
            colunaDeImagens.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridViewLista.AllowUserToAddRows = false;
        }

        private void dataGridViewLista_DoubleClick(object sender, EventArgs e)
        {
            // Abre o estudante selecionado em uma nova janela.
            AtualizarDeletarEstudante atualizarDeletarEstudante = new AtualizarDeletarEstudante();
            atualizarDeletarEstudante.textBoxID.Text = dataGridViewLista.CurrentRow.Cells[0].Value.ToString();
            atualizarDeletarEstudante.textBoxNome.Text = dataGridViewLista.CurrentRow.Cells[1].Value.ToString();
            atualizarDeletarEstudante.textBoxSobrenome.Text = dataGridViewLista.CurrentRow.Cells[2].Value.ToString();
            atualizarDeletarEstudante.dateTimePickerNascimento.Value = (DateTime) dataGridViewLista.CurrentRow.Cells[3].Value;

            // Gênero
            if (dataGridViewLista.CurrentRow.Cells[4].Value.ToString() == "Feminino")
            {
                atualizarDeletarEstudante.radioButtonFeminino.Checked = true;   
            }
            else
            {
                atualizarDeletarEstudante.radioButtonMasculino.Checked = true;
            }

            atualizarDeletarEstudante.textBoxTelefone.Text = dataGridViewLista.CurrentRow.Cells[5].Value.ToString();
            atualizarDeletarEstudante.textBoxEndereco.Text = dataGridViewLista.CurrentRow.Cells[6].Value.ToString();

            // A foto.
            byte[] fotoDaLista;
            fotoDaLista = (byte[]) dataGridViewLista.CurrentRow.Cells[7].Value;
            MemoryStream fotoDoEstudante = new MemoryStream(fotoDaLista);
            atualizarDeletarEstudante.pictureBoxFoto.Image = Image.FromStream(fotoDoEstudante);

            atualizarDeletarEstudante.Show();
        }
    }
}
