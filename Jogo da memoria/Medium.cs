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
    public partial class Medium : Form
    {
        Thread menu, restart;
        Random rnd = new Random();
        int tentativas = 25;
        Label primeiro, segundo;
        List<string> icones = new List<string>
        {
            "o","o",//navio
            "L","L",//lupa
            "X", "X",//auto-falante
            "!","!",//aranha
            "N","N",//olho
            "b","b",//bicicleta
            "v","v",//onibus
            "c","c"//quadrado
        };

        public Medium()
        {
            InitializeComponent();
            PosicionandoIcones();
        }
        private void PosicionandoIcones()
        {
            Label label;
            int numero;

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                {
                    label = (Label)tableLayoutPanel1.Controls[i];
                }
                else
                {
                    continue;
                }

                numero = rnd.Next(0, icones.Count);
                label.Text = icones[numero];

                icones.RemoveAt(numero);
            }
        }
        private void label_Click(object sender, EventArgs e)
        {
            //Esse if é responsavél por limitar em apenas 2 cliks por cada tick do timer
            if (primeiro != null && segundo != null)
            {
                return;
            }

            //Nesse comando ele esta convertendo a label clicada como uma label, e as que não foram, mantendo a mesma forma como foi gerada, no caso, aparecendo a imagem quando clicada e
            //invisivel quando não
            Label clicada = sender as Label;

            if (clicada == null)
            {
                return;
            }
            if (clicada.ForeColor == Color.White)
            {
                return;
            }

            //Aqui será contabilizado o click, onde irá aparecer somente as imagens do primeiro e segundo click 

            if (primeiro == null)
            {
                primeiro = clicada;
                primeiro.ForeColor = Color.White;
                return;
            }

            segundo = clicada;
            segundo.ForeColor = Color.White;


            //Define se o jogador irá perder ou ganhar dependendo da quantidade de tentativas
            if (tentativas == 0)
            {
                GameOver();
            }
            else
            {
                Vencedor();
            }


            //caso as imagens do primeiro e do segundo click forem igual, a imagem continuará na tela
            if (primeiro.Text == segundo.Text)
            {
                primeiro = null;
                segundo = null;
            }
            //caso as imagens sejam diferentes irá iniciar um timer que após um tick irá apagar a seleção dos ticks
            else
            {
                TempoCarta.Start();
            }

            tentativas--;

            label17.Text = tentativas.ToString();
            Console.WriteLine(tentativas);
        }

        private void Vencedor()
        {
            //Esse método será o responsável por detectar se todas as imagens foram achadas e decretar o fim de jogo

            Label label;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label;

                if (label != null && label.ForeColor == label.BackColor)
                {
                    return;
                }
            }

            //Esse comando para o timer que calcula o tempo total de jogo
            TempoJogo.Stop();

            if (MessageBox.Show($"Congratulations! You won the game with {25 - tentativas} attemps") == DialogResult.OK)
            {
                Dificuldade inicio = new Dificuldade();
                inicio.ShowDialog();

            }
            this.Close();
        }
        private void GameOver()
        {
            Label label;

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label;

                if (label == null && label.ForeColor == label.BackColor)
                {
                    return;
                }
            }

            if (MessageBox.Show("Game Over") == DialogResult.OK)
            {
                Dificuldade inicio = new Dificuldade();
                inicio.ShowDialog();

            }
            this.Close();
        }

        private void TempoCarta_Tick(object sender, EventArgs e)
        {
            TempoCarta.Stop();

            primeiro.ForeColor = primeiro.BackColor;
            segundo.ForeColor = segundo.BackColor;

            primeiro = null;
            segundo = null;
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menu = new Thread(AbrirMenu);
            menu.SetApartmentState(ApartmentState.STA);
            menu.Start();
            this.Close();
        }
        private void AbrirMenu(object obj)
        {
            Application.Run(new Dificuldade());
        }

        private void Medium_Load(object sender, EventArgs e)
        {

        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            restart = new Thread(JogoReiniciar);
            restart.SetApartmentState(ApartmentState.STA);
            restart.Start();
            this.Close();
        }
        private void JogoReiniciar(object obj)
        {
            Application.Run(new Medium());
        }

    }
}
