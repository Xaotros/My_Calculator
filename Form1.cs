using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = Form1.EvalExpression(textBox1.Text.ToCharArray()).ToString();
        }

        public static double EvalExpression(char[] expr)
        {
            return ParseSummands(expr, 0);
        }

        //rasclanjujem po '+', '-'
        private static double ParseSummands(char[] expr, int index)
        {
            double x = ParseFactors(expr, ref index);
            while (true)
            {
                char op = expr[index];
                if (op != '+' && op != '-')
                    return x;
                index++;
                double y = ParseFactors(expr, ref index);
                if (op == '+')
                    x += y;
                else
                    x -= y;
            }
        }

        //rasclanjujem po '/', '*'
        private static double ParseFactors(char[] expr, ref int index)
        {
            double x = GetDouble(expr, ref index);
            while (true)
            {
                char op = expr[index];
                if (op != '/' && op != '*')
                    return x;
                index++;
                double y = GetDouble(expr, ref index);
                if (op == '/')
                    x /= y;
                else
                    x *= y;
            }
        }

        //metoda koja vraca double iz stringa
        private static double GetDouble(char[] expr, ref int index)
        {
            string dbl = "";
            while (((int)expr[index] >= 48 && (int)expr[index] <= 57) || expr[index] == '.' || expr[index] == ',')
            {
                if (expr[index] == '.')
                    expr[index] = ',';

                dbl = dbl + expr[index].ToString();
                index++;

                if (index == expr.Length)
                {
                    index--;
                    break;
                }
            }
            return double.Parse(dbl);
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            //omogucit unos samo brojki i operatora + ostali simboli
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46 && ch != '/' && ch != '*' && ch != 13 && ch != '+'
                && ch != '-' && ch != '(' && ch != ')' && ch != '.' && ch != ',' && ch != 27)
            {
                e.Handled = true;
                MessageBox.Show("Please enter valid value");
            }
            //enter key kao button klick
            else if (ch == 13)
            {
                textBox2.Text = Form1.EvalExpression(textBox1.Text.ToCharArray()).ToString();
            //da ne cujemo 'ding' nakon entera
                e.Handled = true;
              
            }
            //pa zasto ne i esc ubacit
            else if (ch == 27)
            {  
              this.Close();
            }
        }


        private void TextBox1_Enter(object sender, EventArgs e)
        {
            //PlaceHolder za enter
            if (textBox1.Text == " Enter Here")
            {
                textBox1.Text = "";
            }
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            //PlaceHolder za leave
            if (textBox1.Text == "")
            {
                textBox1.Text = " Enter Here";
            }
        }
    }
}