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
using System.Media;

namespace Jogo_da_memoria
{
    public partial class TelaInicial : Form
    {
        Thread jogo;
        int i = 0;
        List<Bitmap> ListaImagens = new List<Bitmap>();

        public TelaInicial()
        {
            InitializeComponent();
            Titulo.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Abre o jogo e fecha o menu
            jogo = new Thread(abrirDificuldade);
            jogo.SetApartmentState(ApartmentState.STA);
            jogo.Start();
            this.Close();
            
        }

        private void abrirDificuldade(object obj)
        {
            Application.Run(new Dificuldade());
        }


        private void button3_Click(object sender, EventArgs e)
        {
            //fecha o menu inteiro
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //abre o placar de líderes
        }

        private void TelaInicial_Load(object sender, EventArgs e)
        {
            //Responsável por fazer a mudança do título da cor do título
            ListaImagens.Add(Properties.Resources._1);
            ListaImagens.Add(Properties.Resources._2);
            ListaImagens.Add(Properties.Resources._3);
            ListaImagens.Add(Properties.Resources._4);
            ListaImagens.Add(Properties.Resources._5);
            ListaImagens.Add(Properties.Resources._6);
            ListaImagens.Add(Properties.Resources._7);
            ListaImagens.Add(Properties.Resources._8);
            ListaImagens.Add(Properties.Resources._9);

            SoundPlayer song = new SoundPlayer(Properties.Resources._23TRAP_REMIX);
            song.Load();
            song.PlayLooping();
        }

        private void Titulo_tick(object sender, EventArgs e)
        {
            i++;
            pictureBox1.Image = ListaImagens[i];

            if (i == 8)
            {
                i = -1;
            }
        }

        private void PlayEnter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.play_negrito;
        }

        private void PlayLeave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.play;
        }

        
        private void ExitEnter(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.Exit_negrito;
        }

        private void ExitLeave(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.Exit_normal;
        }
    }
}
