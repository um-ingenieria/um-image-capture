using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCapturaDePantalla
{
    public partial class Pulsador : Form
    {

        int valorExcitacion = 99;
        int valorValencia = 99;

        public Pulsador()
        {  
              
            InitializeComponent();

            button19.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            valorExcitacion = -4;
            this.button1.BackColor = Color.Yellow;
            this.button2.BackColor = Color.LightGray;
            this.button3.BackColor = Color.LightGray;
            this.button4.BackColor = Color.LightGray;
            this.button5.BackColor = Color.LightGray;
            this.button6.BackColor = Color.LightGray;
            this.button7.BackColor = Color.LightGray;
            this.button8.BackColor = Color.LightGray;
            this.button9.BackColor = Color.LightGray;


            this.button1.ForeColor = Color.Black;
            this.button2.ForeColor = Color.Black;
            this.button3.ForeColor = Color.Black;
            this.button4.ForeColor = Color.Black;
            this.button5.ForeColor = Color.Black;
            this.button6.ForeColor = Color.Black;
            this.button7.ForeColor = Color.Black;
            this.button8.ForeColor = Color.Black;
            this.button9.ForeColor = Color.Black;


            if(valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            valorExcitacion = -3;
            this.button1.BackColor = Color.LightGray;
            this.button2.BackColor = Color.Yellow;
            this.button3.BackColor = Color.LightGray;
            this.button4.BackColor = Color.LightGray;
            this.button5.BackColor = Color.LightGray;
            this.button6.BackColor = Color.LightGray;
            this.button7.BackColor = Color.LightGray;
            this.button8.BackColor = Color.LightGray;
            this.button9.BackColor = Color.LightGray;

            this.button1.ForeColor = Color.Black;
            this.button2.ForeColor = Color.Black;
            this.button3.ForeColor = Color.Black;
            this.button4.ForeColor = Color.Black;
            this.button5.ForeColor = Color.Black;
            this.button6.ForeColor = Color.Black;
            this.button7.ForeColor = Color.Black;
            this.button8.ForeColor = Color.Black;
            this.button9.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            valorExcitacion = -2;
            this.button1.BackColor = Color.LightGray;
            this.button2.BackColor = Color.LightGray;
            this.button3.BackColor = Color.Yellow;
            this.button4.BackColor = Color.LightGray;
            this.button5.BackColor = Color.LightGray;
            this.button6.BackColor = Color.LightGray;
            this.button7.BackColor = Color.LightGray;
            this.button8.BackColor = Color.LightGray;
            this.button9.BackColor = Color.LightGray;

            this.button1.ForeColor = Color.Black;
            this.button2.ForeColor = Color.Black;
            this.button3.ForeColor = Color.Black;
            this.button4.ForeColor = Color.Black;
            this.button5.ForeColor = Color.Black;
            this.button6.ForeColor = Color.Black;
            this.button7.ForeColor = Color.Black;
            this.button8.ForeColor = Color.Black;
            this.button9.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            valorExcitacion = -1;
            this.button1.BackColor = Color.LightGray;
            this.button2.BackColor = Color.LightGray;
            this.button3.BackColor = Color.LightGray;
            this.button4.BackColor = Color.Yellow;
            this.button5.BackColor = Color.LightGray;
            this.button6.BackColor = Color.LightGray;
            this.button7.BackColor = Color.LightGray;
            this.button8.BackColor = Color.LightGray;
            this.button9.BackColor = Color.LightGray;

            this.button1.ForeColor = Color.Black;
            this.button2.ForeColor = Color.Black;
            this.button3.ForeColor = Color.Black;
            this.button4.ForeColor = Color.Black;
            this.button5.ForeColor = Color.Black;
            this.button6.ForeColor = Color.Black;
            this.button7.ForeColor = Color.Black;
            this.button8.ForeColor = Color.Black;
            this.button9.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            valorExcitacion = 0;
            this.button1.BackColor = Color.LightGray;
            this.button2.BackColor = Color.LightGray;
            this.button3.BackColor = Color.LightGray;
            this.button4.BackColor = Color.LightGray;
            this.button5.BackColor = Color.Yellow;
            this.button6.BackColor = Color.LightGray;
            this.button7.BackColor = Color.LightGray;
            this.button8.BackColor = Color.LightGray;
            this.button9.BackColor = Color.LightGray;

            this.button1.ForeColor = Color.Black;
            this.button2.ForeColor = Color.Black;
            this.button3.ForeColor = Color.Black;
            this.button4.ForeColor = Color.Black;
            this.button5.ForeColor = Color.Black;
            this.button6.ForeColor = Color.Black;
            this.button7.ForeColor = Color.Black;
            this.button8.ForeColor = Color.Black;
            this.button9.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;

        }

        
        private void button9_Click(object sender, EventArgs e)
        {
            valorExcitacion = 1;
            this.button1.BackColor = Color.LightGray;
            this.button2.BackColor = Color.LightGray;
            this.button3.BackColor = Color.LightGray;
            this.button4.BackColor = Color.LightGray;
            this.button5.BackColor = Color.LightGray;
            this.button6.BackColor = Color.LightGray;
            this.button7.BackColor = Color.LightGray;
            this.button8.BackColor = Color.LightGray;
            this.button9.BackColor = Color.Yellow;

            this.button1.ForeColor = Color.Black;
            this.button2.ForeColor = Color.Black;
            this.button3.ForeColor = Color.Black;
            this.button4.ForeColor = Color.Black;
            this.button5.ForeColor = Color.Black;
            this.button6.ForeColor = Color.Black;
            this.button7.ForeColor = Color.Black;
            this.button8.ForeColor = Color.Black;
            this.button9.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            valorExcitacion = 2;
            this.button1.BackColor = Color.LightGray;
            this.button2.BackColor = Color.LightGray;
            this.button3.BackColor = Color.LightGray;
            this.button4.BackColor = Color.LightGray;
            this.button5.BackColor = Color.LightGray;
            this.button6.BackColor = Color.LightGray;
            this.button7.BackColor = Color.LightGray;
            this.button8.BackColor = Color.Yellow;
            this.button9.BackColor = Color.LightGray;

            this.button1.ForeColor = Color.Black;
            this.button2.ForeColor = Color.Black;
            this.button3.ForeColor = Color.Black;
            this.button4.ForeColor = Color.Black;
            this.button5.ForeColor = Color.Black;
            this.button6.ForeColor = Color.Black;
            this.button7.ForeColor = Color.Black;
            this.button8.ForeColor = Color.Black;
            this.button9.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            valorExcitacion = 3;
            this.button1.BackColor = Color.LightGray;
            this.button2.BackColor = Color.LightGray;
            this.button3.BackColor = Color.LightGray;
            this.button4.BackColor = Color.LightGray;
            this.button5.BackColor = Color.LightGray;
            this.button6.BackColor = Color.LightGray;
            this.button7.BackColor = Color.Yellow;
            this.button8.BackColor = Color.LightGray;
            this.button9.BackColor = Color.LightGray;

            this.button1.ForeColor = Color.Black;
            this.button2.ForeColor = Color.Black;
            this.button3.ForeColor = Color.Black;
            this.button4.ForeColor = Color.Black;
            this.button5.ForeColor = Color.Black;
            this.button6.ForeColor = Color.Black;
            this.button7.ForeColor = Color.Black;
            this.button8.ForeColor = Color.Black;
            this.button9.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            valorExcitacion = 4;
            this.button1.BackColor = Color.LightGray;
            this.button2.BackColor = Color.LightGray;
            this.button3.BackColor = Color.LightGray;
            this.button4.BackColor = Color.LightGray;
            this.button5.BackColor = Color.LightGray;
            this.button6.BackColor = Color.Yellow;
            this.button7.BackColor = Color.LightGray;
            this.button8.BackColor = Color.LightGray;
            this.button9.BackColor = Color.LightGray;

            this.button1.ForeColor = Color.Black;
            this.button2.ForeColor = Color.Black;
            this.button3.ForeColor = Color.Black;
            this.button4.ForeColor = Color.Black;
            this.button5.ForeColor = Color.Black;
            this.button6.ForeColor = Color.Black;
            this.button7.ForeColor = Color.Black;
            this.button8.ForeColor = Color.Black;
            this.button9.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;

        }

        private void button18_Click(object sender, EventArgs e)
        {
            valorValencia = -4;
            this.button18.BackColor = Color.Yellow;
            this.button17.BackColor = Color.LightGray;
            this.button16.BackColor = Color.LightGray;
            this.button15.BackColor = Color.LightGray;
            this.button14.BackColor = Color.LightGray;
            this.button13.BackColor = Color.LightGray;
            this.button12.BackColor = Color.LightGray;
            this.button11.BackColor = Color.LightGray;
            this.button10.BackColor = Color.LightGray;

            this.button18.ForeColor = Color.Black;
            this.button17.ForeColor = Color.Black;
            this.button16.ForeColor = Color.Black;
            this.button15.ForeColor = Color.Black;
            this.button14.ForeColor = Color.Black;
            this.button13.ForeColor = Color.Black;
            this.button12.ForeColor = Color.Black;
            this.button11.ForeColor = Color.Black;
            this.button10.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;

        }

        private void button17_Click(object sender, EventArgs e)
        {
            valorValencia = -3;
            this.button18.BackColor = Color.LightGray;
            this.button17.BackColor = Color.Yellow;
            this.button16.BackColor = Color.LightGray;
            this.button15.BackColor = Color.LightGray;
            this.button14.BackColor = Color.LightGray;
            this.button13.BackColor = Color.LightGray;
            this.button12.BackColor = Color.LightGray;
            this.button11.BackColor = Color.LightGray;
            this.button10.BackColor = Color.LightGray;

            this.button18.ForeColor = Color.Black;
            this.button17.ForeColor = Color.Black;
            this.button16.ForeColor = Color.Black;
            this.button15.ForeColor = Color.Black;
            this.button14.ForeColor = Color.Black;
            this.button13.ForeColor = Color.Black;
            this.button12.ForeColor = Color.Black;
            this.button11.ForeColor = Color.Black;
            this.button10.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            valorValencia = -2;
            this.button18.BackColor = Color.LightGray;
            this.button17.BackColor = Color.LightGray;
            this.button16.BackColor = Color.Yellow;
            this.button15.BackColor = Color.LightGray;
            this.button14.BackColor = Color.LightGray;
            this.button13.BackColor = Color.LightGray;
            this.button12.BackColor = Color.LightGray;
            this.button11.BackColor = Color.LightGray;
            this.button10.BackColor = Color.LightGray;

            this.button18.ForeColor = Color.Black;
            this.button17.ForeColor = Color.Black;
            this.button16.ForeColor = Color.Black;
            this.button15.ForeColor = Color.Black;
            this.button14.ForeColor = Color.Black;
            this.button13.ForeColor = Color.Black;
            this.button12.ForeColor = Color.Black;
            this.button11.ForeColor = Color.Black;
            this.button10.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            valorValencia = -1;
            this.button18.BackColor = Color.LightGray;
            this.button17.BackColor = Color.LightGray;
            this.button16.BackColor = Color.LightGray;
            this.button15.BackColor = Color.Yellow;
            this.button14.BackColor = Color.LightGray;
            this.button13.BackColor = Color.LightGray;
            this.button12.BackColor = Color.LightGray;
            this.button11.BackColor = Color.LightGray;
            this.button10.BackColor = Color.LightGray;

            this.button18.ForeColor = Color.Black;
            this.button17.ForeColor = Color.Black;
            this.button16.ForeColor = Color.Black;
            this.button15.ForeColor = Color.Black;
            this.button14.ForeColor = Color.Black;
            this.button13.ForeColor = Color.Black;
            this.button12.ForeColor = Color.Black;
            this.button11.ForeColor = Color.Black;
            this.button10.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            valorValencia = 0;
            this.button18.BackColor = Color.LightGray;
            this.button17.BackColor = Color.LightGray;
            this.button16.BackColor = Color.LightGray;
            this.button15.BackColor = Color.LightGray;
            this.button14.BackColor = Color.Yellow;
            this.button13.BackColor = Color.LightGray;
            this.button12.BackColor = Color.LightGray;
            this.button11.BackColor = Color.LightGray;
            this.button10.BackColor = Color.LightGray;

            this.button18.ForeColor = Color.Black;
            this.button17.ForeColor = Color.Black;
            this.button16.ForeColor = Color.Black;
            this.button15.ForeColor = Color.Black;
            this.button14.ForeColor = Color.Black;
            this.button13.ForeColor = Color.Black;
            this.button12.ForeColor = Color.Black;
            this.button11.ForeColor = Color.Black;
            this.button10.ForeColor = Color.Black;


            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            valorValencia = 1;
            this.button18.BackColor = Color.LightGray;
            this.button17.BackColor = Color.LightGray;
            this.button16.BackColor = Color.LightGray;
            this.button15.BackColor = Color.LightGray;
            this.button14.BackColor = Color.LightGray;
            this.button13.BackColor = Color.Yellow;
            this.button12.BackColor = Color.LightGray;
            this.button11.BackColor = Color.LightGray;
            this.button10.BackColor = Color.LightGray;

            this.button18.ForeColor = Color.Black;
            this.button17.ForeColor = Color.Black;
            this.button16.ForeColor = Color.Black;
            this.button15.ForeColor = Color.Black;
            this.button14.ForeColor = Color.Black;
            this.button13.ForeColor = Color.Black;
            this.button12.ForeColor = Color.Black;
            this.button11.ForeColor = Color.Black;
            this.button10.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            valorValencia = 2;
            this.button18.BackColor = Color.LightGray;
            this.button17.BackColor = Color.LightGray;
            this.button16.BackColor = Color.LightGray;
            this.button15.BackColor = Color.LightGray;
            this.button14.BackColor = Color.LightGray;
            this.button13.BackColor = Color.LightGray;
            this.button12.BackColor = Color.Yellow;
            this.button11.BackColor = Color.LightGray;
            this.button10.BackColor = Color.LightGray;

            this.button18.ForeColor = Color.Black;
            this.button17.ForeColor = Color.Black;
            this.button16.ForeColor = Color.Black;
            this.button15.ForeColor = Color.Black;
            this.button14.ForeColor = Color.Black;
            this.button13.ForeColor = Color.Black;
            this.button12.ForeColor = Color.Black;
            this.button11.ForeColor = Color.Black;
            this.button10.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            valorValencia = 3;
            this.button18.BackColor = Color.LightGray;
            this.button17.BackColor = Color.LightGray;
            this.button16.BackColor = Color.LightGray;
            this.button15.BackColor = Color.LightGray;
            this.button14.BackColor = Color.LightGray;
            this.button13.BackColor = Color.LightGray;
            this.button12.BackColor = Color.LightGray;
            this.button11.BackColor = Color.Yellow;
            this.button10.BackColor = Color.LightGray;

            this.button18.ForeColor = Color.Black;
            this.button17.ForeColor = Color.Black;
            this.button16.ForeColor = Color.Black;
            this.button15.ForeColor = Color.Black;
            this.button14.ForeColor = Color.Black;
            this.button13.ForeColor = Color.Black;
            this.button12.ForeColor = Color.Black;
            this.button11.ForeColor = Color.Black;
            this.button10.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            valorValencia = 4;
            this.button18.BackColor = Color.LightGray;
            this.button17.BackColor = Color.LightGray;
            this.button16.BackColor = Color.LightGray;
            this.button15.BackColor = Color.LightGray;
            this.button14.BackColor = Color.LightGray;
            this.button13.BackColor = Color.LightGray;
            this.button12.BackColor = Color.LightGray;
            this.button11.BackColor = Color.LightGray;
            this.button10.BackColor = Color.Yellow;


            this.button18.ForeColor = Color.Black;
            this.button17.ForeColor = Color.Black;
            this.button16.ForeColor = Color.Black;
            this.button15.ForeColor = Color.Black;
            this.button14.ForeColor = Color.Black;
            this.button13.ForeColor = Color.Black;
            this.button12.ForeColor = Color.Black;
            this.button11.ForeColor = Color.Black;
            this.button10.ForeColor = Color.Black;

            if (valorExcitacion != 99 && valorValencia != 99)
                button19.Enabled = true;
        }


        public int DarValorExcitacion()
        {
            return valorExcitacion;
        }

        public int DarValorValencia()
        {
            return valorValencia;
        }

        

        private void button20_Click(object sender, EventArgs e)
        {
            valorValencia = 0;
            valorExcitacion = 0;
            this.Close();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
