using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Jogo_da_memoria
{
    public partial class Dificuldade : Form
    {
        Thread facil, medio, dificil, menu;

        public Dificuldade()
        {
            InitializeComponent();
        }

        private void ModoFacil(object sender, EventArgs e)
        {
            facil = new Thread(abrirFacil);
            facil.SetApartmentState(ApartmentState.STA);
            facil.Start();
            this.Close();
        }

        private void abrirFacil(object obj)
        {
            Application.Run(new Easy());
        }
        private void button2_Click(object sender, EventArgs e)
        {
            medio = new Thread(abrirMedio);
            medio.SetApartmentState(ApartmentState.STA);
            medio.Start();
            this.Close();
        }

        private void abrirMedio(object obj)
        {
            Application.Run(new Medium());
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dificil = new Thread(abrirDificil);
            dificil.SetApartmentState(ApartmentState.STA);
            dificil.Start();
            this.Close();
        }

        private void abrirDificil(object obj)
        {
            Application.Run(new Hard());
        }

        private void VoltarMenu(object sender, EventArgs e)
        {
            menu = new Thread(Menu);
            menu.SetApartmentState(ApartmentState.STA);
            menu.Start();
            this.Close();
        }


        private void Menu(object obj)
        {
            Application.Run(new TelaInicial());
        }


        private void EasyEnter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.easy_negrito;
        }

        private void EasyLeave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.easy_normal;
        }

        private void MediumEnter(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.medium_negrito;
        }

        private void MediumLeave(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.medium_normal;
        }

        private void HardEnter(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.hard_negrito;
        }
        private void HardLeaver(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.hard_normal;
        }
        private void BackEnter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.back_negrito;
        }
        private void BackLeave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.back_normal;
        }
    }

}
