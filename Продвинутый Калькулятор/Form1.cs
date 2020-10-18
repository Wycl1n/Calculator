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
        string input = "";
        private string[] ParserInput(string input)
        {
            int[] requered_minuses = new int[input.Length];
            int i_requered_minuses = 0;
            for (int i=0;i<input.Length;i++)
            {
                if (input[i] == '-')
                {
                    if (i == 0)
                    {
                        input = input.Insert(0, "(0");
                        requered_minuses[i_requered_minuses++] = i + 3;
                        i += 4;
                        continue;
                    }
                    if(input[i-1] == '1' || input[i - 1] == '2' || input[i - 1] == '3' || input[i - 1] == '4' || input[i - 1] == '5' || input[i - 1] == '6' || input[i - 1] == '7' || input[i - 1] == '8' || input[i - 1] == '9' || input[i - 1] == '0')
                    {
                        input = input.Insert(i, "+(0");
                        requered_minuses[i_requered_minuses++] = i + 4;
                        i += 5;
                        continue;
                    }
                    else
                    {
                        input = input.Insert(i, "(0");
                        requered_minuses[i_requered_minuses++] = i + 3;
                        i += 4;
                        continue;
                    }
                }
            }

            for(int i=0;i<i_requered_minuses;i++)
            {
                for (int j = requered_minuses[i] + 1; j < input.Length; j++)
                {
                    if (input[j] != '1' || input[j] != '2' || input[j] != '3' || input[j] != '4' || input[j] != '5' || input[j] != '6' || input[j] != '7' || input[j] != '8' || input[j] != '9' || input[j] != '0')
                    {
                        input = input.Insert(j, ")");
                        break;
                    }
                }
            }

            int n = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(') n++;
                if (input[i] == ')') n--;
            }
            for (int i = 0; i < n; i++)
                if (n > 0) input += ')';

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
                            result[i_result] = Convert.ToString(Math.PI);
                            break;
                        }
                    case 'e':
                        {
                            result[i_result] = Convert.ToString(Math.E);
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
            for (int i = 0; i < pInput.Length; i++)
            {
                if (pInput[i] == "(")
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
                        {
                            switch (pInput[i + 1])
                            {
                                case "o":
                                    {
                                        pInput[i] = Convert.ToString(Math.Log(Convert.ToDouble(pInput[i + 3])) / Math.Log(2));
                                        for (int z = 0; z < 3; z++)
                                        {
                                            for (int j = i + 1; j < pInput.Length - 1; j++)
                                            {
                                                pInput[j] = pInput[j + 1];
                                            }
                                            Array.Resize(ref pInput, pInput.Length - 1);
                                        }
                                        break;
                                    }
                                case "g":
                                    {
                                        pInput[i] = Convert.ToString(Math.Log(Convert.ToDouble(pInput[i + 2])) / Math.Log(10));
                                        for (int z = 0; z < 2; z++)
                                        {
                                            for (int j = i + 1; j < pInput.Length - 1; j++)
                                            {
                                                pInput[j] = pInput[j + 1];
                                            }
                                            Array.Resize(ref pInput, pInput.Length - 1);
                                        }
                                        break;
                                    }
                                case "n":
                                    {
                                        pInput[i] = Convert.ToString(Math.Log(Convert.ToDouble(pInput[i + 2])) / Math.Log(Math.E));
                                        for (int z = 0; z < 2; z++)
                                        {
                                            for (int j = i + 1; j < pInput.Length - 1; j++)
                                            {
                                                pInput[j] = pInput[j + 1];
                                            }
                                            Array.Resize(ref pInput, pInput.Length - 1);
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case "s":
                        pInput[i] = Convert.ToString(Math.Sin(Convert.ToDouble(pInput[i + 3])));
                        for (int z = 0; z < 3; z++)
                        {
                            for (int j = i + 1; j < pInput.Length - 1; j++)
                            {
                                pInput[j] = pInput[j + 1];
                            }
                            Array.Resize(ref pInput, pInput.Length - 1);
                        }
                        break;
                    case "c":
                        pInput[i] = Convert.ToString(Math.Cos(Convert.ToDouble(pInput[i + 3])));
                        for (int z = 0; z < 3; z++)
                        {
                            for (int j = i + 1; j < pInput.Length - 1; j++)
                            {
                                pInput[j] = pInput[j + 1];
                            }
                            Array.Resize(ref pInput, pInput.Length - 1);
                        }
                        break;
                    case "t":
                        pInput[i] = Convert.ToString(Math.Tan(Convert.ToDouble(pInput[i + 2])));
                        for (int z = 0; z < 2; z++)
                        {
                            for (int j = i + 1; j < pInput.Length - 1; j++)
                            {
                                pInput[j] = pInput[j + 1];
                            }
                            Array.Resize(ref pInput, pInput.Length - 1);
                        }
                        break;
                }
            }
            for (int i = 0; i < pInput.Length; i++)
            {
                string Char = pInput[i];
                switch (Char)
                {
                    case "^":
                        pInput[i - 1] = Convert.ToString(Math.Pow(Convert.ToDouble(pInput[i - 1]), Convert.ToDouble(pInput[i + 1])));
                        for (int z = 0; z < 2; z++)
                        {
                            for (int j = i; j < pInput.Length - 1; j++)
                            {
                                pInput[j] = pInput[j + 1];
                            }
                            Array.Resize(ref pInput, pInput.Length - 1);
                        }
                        i--;
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
                        for (int z = 0; z < 2; z++)
                        {
                            for (int j = i; j < pInput.Length - 1; j++)
                            {
                                pInput[j] = pInput[j + 1];
                            }
                            Array.Resize(ref pInput, pInput.Length - 1);
                        }
                        i--;
                        break;
                    case "/":
                        pInput[i - 1] = Convert.ToString(Convert.ToDouble(pInput[i - 1]) / Convert.ToDouble(pInput[i + 1]));
                        for (int z = 0; z < 2; z++)
                        {
                            for (int j = i; j < pInput.Length - 1; j++)
                            {
                                pInput[j] = pInput[j + 1];
                            }
                            Array.Resize(ref pInput, pInput.Length - 1);
                        }
                        i--;
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
                        for (int z = 0; z < 2; z++)
                        {
                            for (int j = i; j < pInput.Length - 1; j++)
                            {
                                pInput[j] = pInput[j + 1];
                            }
                            Array.Resize(ref pInput, pInput.Length - 1);
                        }
                        i--;
                        break;
                    case "-":
                        pInput[i - 1] = Convert.ToString(Convert.ToDouble(pInput[i - 1]) - Convert.ToDouble(pInput[i + 1]));
                        for (int z = 0; z < 2; z++)
                        {
                            for (int j = i; j < pInput.Length - 1; j++)
                            {
                                pInput[j] = pInput[j + 1];
                            }
                            Array.Resize(ref pInput, pInput.Length - 1);
                        }
                        i--;
                        break;
                }
            }
            return pInput[0];
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input += "1";
            label1.Text = Calculate(ParserInput(input));
            textBox1.Text = input;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            input += "2";
            label1.Text = Calculate(ParserInput(input));
            textBox1.Text = input;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            input += "3";
            label1.Text = Calculate(ParserInput(input));
            textBox1.Text = input;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            input += "4";
            label1.Text = Calculate(ParserInput(input));
            textBox1.Text = input;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            input += "5";
            label1.Text = Calculate(ParserInput(input));
            textBox1.Text = input;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            input += "6";
            label1.Text = Calculate(ParserInput(input));
            textBox1.Text = input;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            input += "7";
            label1.Text = Calculate(ParserInput(input));
            textBox1.Text = input;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            input += "8";
            label1.Text = Calculate(ParserInput(input));
            textBox1.Text = input;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            input += "9";
            label1.Text = Calculate(ParserInput(input));
            textBox1.Text = input;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            input += "0";
            label1.Text = Calculate(ParserInput(input));
            textBox1.Text = input;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            input += ",";
            label1.Text = Calculate(ParserInput(input));
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
            if (input.Length != 0) 
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
        private void button26_Click(object sender, EventArgs e)
        {
            input += "lg(";
            textBox1.Text = input;
        }
        private void button27_Click(object sender, EventArgs e)
        {
            input += "ln(";
            textBox1.Text = input;
                }
        private void button28_Click(object sender, EventArgs e)
                {
                    input += "sin(";
                    textBox1.Text = input;
                }
        private void button29_Click(object sender, EventArgs e)
                {
                    input += "cos(";
                    textBox1.Text = input;
                }
        private void button30_Click(object sender, EventArgs e)
                {
                    input += "tg(";
                    textBox1.Text = input;
                }
    }
}
