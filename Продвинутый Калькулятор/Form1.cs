using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Продвинутый_Калькулятор
{
    public partial class Form1 : Form
    {
        
        private string[] ParserInput(string input)
        {
            string[] result = new string[input.Length];
            for (int i = 0; i < input.Length; i++) result[i] = "";
            for (int i = 0, i_result = 0; i < input.Length; i++)  
            {
                switch (input[i])
                {
                    case ',':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '0':
                        {
                            result[i_result] += Convert.ToString(input[i]);
                            break;
                        }
                    case 'p':
                        {
                            result[i_result] = "3,14159265359";
                            break;
                        }
                    case 'e':
                        {
                            result[i_result] = "2,71828182846";
                            break;
                        }
                    default:
                        {
                            if (i == 0) 
                            {
                                result[i_result++] = Convert.ToString(input[i]);
                                break;
                            }
                            switch (input[i - 1])
                            {
                                case '1':
                                case '2':
                                case '3':
                                case '4':
                                case '5':
                                case '6':
                                case '7':
                                case '8':
                                case '9':
                                case '0':
                                case 'p':
                                case 'e':
                                    {
                                        result[++i_result] = Convert.ToString(input[i]);
                                        i_result++;
                                        break;
                                    }
                                default:
                                    {
                                        result[i_result++] = Convert.ToString(input[i]);
                                        break;
                                    }
                            }
                            break;
                        }
                }
            }
            return result;
        }
        private string Calculate(string[] pInput)
        {
            for(int i=0;i<pInput.Length;i++)
            {
                if(pInput[i]=="(")
                {
                    int j_of_end = 0;
                    for (int j = i + 1, n = 1; j < pInput.Length; j++)
                    {
                        if (pInput[j] == ")") n--;
                        if (pInput[j] == "(") n++;
                        if (n == 0) { j_of_end = j; break; }
                    }
                    string[] StrToCalculate = new string[j_of_end - i];
                    for (int j = i + 1, t = 0; j < j_of_end; j++, t++)
                        StrToCalculate[t] = pInput[j];
                    pInput[i] = Calculate(StrToCalculate);
                    for (int j = 0; j < j_of_end - i; j++)
                    {
                        for (int a = i + 1; a < pInput.Length - 1; a++)
                        {
                            pInput[a] = pInput[a + 1];
                        }
                    }
                    Array.Resize(ref pInput, pInput.Length - 2);
                }
            }
            for (int i = 0; i < pInput.Length; i++)
            {
                string Char = pInput[i];
                switch (Char)
                {
                    case "l":
                        pInput[i] = Convert.ToString(Math.Log(Convert.ToDouble(pInput[i + 1])) / Math.Log(2));
                        for (int j = i + 1; j < pInput.Length - 1; j++) 
                        {
                            pInput[j] = pInput[j + 1];
                        }
                        for (int j = i + 1; j < pInput.Length - 1; j++) 
                        {
                            pInput[j] = pInput[j + 1];
                        }
                        for (int j = i + 1; j < pInput.Length - 1; j++) 
                        {
                            pInput[j] = pInput[j + 1];
                        }
                        Array.Resize(ref pInput, pInput.Length - 1);
                        break;
                    case "^":
                        pInput[i - 1] = Convert.ToString(Math.Pow(Convert.ToDouble(pInput[i - 1]), Convert.ToDouble(pInput[i + 1])));
                        for (int j = i; j < pInput.Length - 1; j++)
                        {
                            pInput[j] = pInput[j + 1];
                        }
                        for (int j = i; j < pInput.Length - 1; j++)
                        {
                            pInput[j] = pInput[j + 1];
                        }
                        i--;
                        Array.Resize(ref pInput, pInput.Length - 2);
                        break;
                    case "!":
                        double value = 1;
                        for (int j = 1; j <= Convert.ToDouble(pInput[i - 1]); j++)
                        {
                            value *= j;
                        }
                        pInput[i - 1] = Convert.ToString(value);
                        for (int j = i; j < pInput.Length - 1; j++)
                        {
                            pInput[j] = pInput[j + 1];
                        }
                        Array.Resize(ref pInput, pInput.Length - 1);
                        break;
                    case "*":
                        pInput[i - 1] = Convert.ToString(Convert.ToDouble(pInput[i - 1]) * Convert.ToDouble(pInput[i + 1]));
                        for (int j = i; j < pInput.Length - 1; j++)
                        {
                            pInput[j] = pInput[j + 1];
                        }
                        for (int j = i; j < pInput.Length - 1; j++)
                        {
                            pInput[j] = pInput[j + 1];
                        }
                        i--;
                        Array.Resize(ref pInput, pInput.Length - 1);
                        break;
                    case "/":
                        pInput[i - 1] = Convert.ToString(Convert.ToDouble(pInput[i - 1]) / Convert.ToDouble(pInput[i + 1]));
                        for (int j = i; j < pInput.Length - 1; j++)
                        {
                            pInput[j] = pInput[j + 1];
                        }
                        for (int j = i; j < pInput.Length - 1; j++)
                        {
                            pInput[j] = pInput[j + 1];
                        }
                        i--;
                        Array.Resize(ref pInput, pInput.Length - 1);
                        break;

                }
            }
            for (int i = 0; i < pInput.Length; i++)
            {
                string Char = pInput[i];
                switch (Char)
                {
                    case "+":
                        pInput[i - 1] = Convert.ToString(Convert.ToDouble(pInput[i - 1]) + Convert.ToDouble(pInput[i + 1]));
                        for (int j = i; j < pInput.Length - 1; j++)
                        {
                            pInput[j] = pInput[j + 1];
                        }
                        for (int j = i; j < pInput.Length - 1; j++)
                        {
                            pInput[j] = pInput[j + 1];
                        }
                        i--;
                        Array.Resize(ref pInput, pInput.Length - 1);
                        break;
                    case "-":
                        pInput[i - 1] = Convert.ToString(Convert.ToDouble(pInput[i - 1]) - Convert.ToDouble(pInput[i + 1]));
                        for (int j = i; j < pInput.Length - 1; j++)
                        {
                            pInput[j] = pInput[j + 1];
                        }
                        for (int j = i; j < pInput.Length - 1; j++)
                        {
                            pInput[j] = pInput[j + 1];
                        }
                        i--;
                        Array.Resize(ref pInput, pInput.Length - 1);
                        break;
                }
            }
            return pInput[0];
        }

        string input = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input += "1";
            textBox1.Text = input;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            input += "2";
            textBox1.Text = input;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            input += "3";
            textBox1.Text = input;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            input += "4";
            textBox1.Text = input;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            input += "5";
            textBox1.Text = input;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            input += "6";
            textBox1.Text = input;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            input += "7";
            textBox1.Text = input;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            input += "8";
            textBox1.Text = input;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            input += "9";
            textBox1.Text = input;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            input += "0";
            textBox1.Text = input;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            input += ",";
            textBox1.Text = input;
        }
        private void button13_Click(object sender, EventArgs e)
        {
            input += "/";
            textBox1.Text = input;
        }
        private void button14_Click(object sender, EventArgs e)
        {
            input += "*";
            textBox1.Text = input;
        }
        private void button15_Click(object sender, EventArgs e)
        {
            input += "-";
            textBox1.Text = input;
        }
        private void button16_Click(object sender, EventArgs e)
        {
            input += "+";
            textBox1.Text = input;
        }
        private void button17_Click(object sender, EventArgs e)
        {
            input += "(";
            textBox1.Text = input;
        }
        private void button18_Click(object sender, EventArgs e)
        {
            input += ")";
            textBox1.Text = input;
        }
        private void button19_Click(object sender, EventArgs e)
        {
            input = "";
            textBox1.Text = input;
        }
        private void button20_Click(object sender, EventArgs e)
        {
            input = input.Remove(input.Length - 1);
            textBox1.Text = input;
        }
        private void button21_Click(object sender, EventArgs e)
        {
            input += "!";
            textBox1.Text = input;
        }
        private void button22_Click(object sender, EventArgs e)
        {
            input += "^";
            textBox1.Text = input;
        }
        private void button23_Click(object sender, EventArgs e)
        {
            input += "l(";
            textBox1.Text = input;
        }
        private void button24_Click(object sender, EventArgs e)
        {
            input += "p";
            textBox1.Text = input;
        }
        private void button25_Click(object sender, EventArgs e)
        {
            input += "e";
            textBox1.Text = input;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            input = textBox1.Text;
            string result = Calculate(ParserInput(input));
            textBox1.Text = Convert.ToString(result);
            input = Convert.ToString(result);
        }
    }
}
