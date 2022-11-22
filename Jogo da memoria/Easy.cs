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
    public partial class Easy : Form
    {
        Thread menu,restart;
        // Instanciando o random do jogo da memoria
        Random rdm = new Random();
        int tempo = 0;//Tempo de jogo 
        int cliques=0;
        //Colocando em uma List todos os icones do jogo

        List<string> icones = new List<string>()
        {
            //Como a príncipio, as imagens estão sendo usadas a partir da fonte webdings, as letras abaixo representam as imagens escolhidas na fonte
            //A dupla de letra significa o par das imagens

            "o","o",//navio
            "L","L",//lupa
            "X", "X",//auto-falante
            "!","!",//aranha
            "N","N",//olho
            "b","b",//bicicleta
            "v","v",//onibus
            "c","c"//quadrado
        };

        //A variável label vai ser usada para comparar se a imagem do primeiro e do segundo click são igual, e é uma variavel label já que todas as imagens estão em label
        Label primeiro, segundo;


        public Easy()
        {
            InitializeComponent();
            PosicionandoIcones();
            timer2.Start();//Inicia o timer que calcula o tempo total de jogo
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

            if(clicada == null)
            {
                return;
            }
            if(clicada.ForeColor == Color.White)
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

            
            Vencedor();
            //caso as imagens do primeiro e do segundo click forem igual, a imagem continuará na tela
            if(primeiro.Text == segundo.Text)
            {
                primeiro = null;
                segundo = null;
            }
            //caso as imagens sejam diferentes irá iniciar um timer que após um tick irá apagar a seleção dos ticks
            else
            {
                timer1.Start();
            }

            //define o número de jogadas até a conclusão do jogo
            cliques++;
            int tentativas = cliques;
            label17.Text= tentativas.ToString();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //após o tick determinado, o timer para e transforma as imagens novamente na cor do fundo caso sejam diferentes 

            timer1.Stop();

            primeiro.ForeColor = primeiro.BackColor;
            segundo.ForeColor = segundo.BackColor;

            primeiro = null;
            segundo = null;
        }
        private void Vencedor()
        {
            //Esse método será o responsável por detectar se todas as imagens foram achadas e decretar o fim de jogo

            Label label;
            for(int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label;

                if(label != null && label.ForeColor == label.BackColor)
                {
                    return;
                }
            }

            //Esse comando para o timer que calcula o tempo total de jogo
            timer2.Stop();

            MessageBox.Show($"Parabéns!! Você jogou por {tempo} segundos e com {cliques+1} tentativas");
            Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            tempo++;
            label19.Text = tempo.ToString();
        }

        private void PosicionandoIcones()
        {
            //Esse método dara posicionamento para cada imagem de forma randomica

            Label label;
            int numero;

            //Esse for é para integragir com cada comando que existe dentro do painel, que no caso só existe as Labels
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                //O if ele é responsável por, se o comando dentro do painel for um Label, jogar esse comando para uma variável do tipo Label
                //Se fosse um outro tipo de interação, por exemplo, uma picturebox teria que ser feito a mesma coisa, porém uma variável do tipo PictureBox e assim por diante 
                //O if funciona como uma verificação para facilitar possíveis mudanças no código

                if (tableLayoutPanel1.Controls[i] is Label)
                {
                    label = (Label)tableLayoutPanel1.Controls[i];
                }
                else
                {
                    continue;
                }

                //posicionando as imagens nos quadrados
                numero = rdm.Next(0, icones.Count);
                label.Text = icones[numero];

                //evitando que haja sobreposição de imagens, ou seja, duas imagens terem o mesmo posicionamento
                icones.RemoveAt(numero);
            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Comando que retorna para tela inicial

            menu = new Thread(abrirMenu);
            menu.SetApartmentState(ApartmentState.STA);
            menu.Start();
            this.Close();
        }

        private void abrirMenu(object obj)
        {
            Application.Run(new Dificuldade());
        }

        private void RestartButtom_Click(object sender, EventArgs e)
        {
            //reinicia o jogo
            restart = new Thread(Restart);
            restart.SetApartmentState(ApartmentState.STA);
            restart.Start();
            this.Close();
        }

        private void Easy_Load(object sender, EventArgs e)
        {

        }

        private void Restart(object obj)
        {
            Application.Run(new Easy());
        }

        private void scoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //abre o placar de líderes 
        }
    }
}
