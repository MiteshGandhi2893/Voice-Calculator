using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
namespace calculator
{
    public partial class Calculator : Form
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Choices clist = new Choices();
        Choices clist1 = new Choices();
        public Calculator()
        {
            InitializeComponent();
            clist.Add(new string[] { "one","two","three","four","five","six","seven","eight","nine","zero" });
            clist1.Add(new string[] { "plus", "minus", "multiply", "divide","enter","erase","reset" ,"exit","close"});
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ss.Speak("Welcome to MG's Calculator");
            Grammar gr = new Grammar(new GrammarBuilder(clist));
            Grammar gr1 = new Grammar(new GrammarBuilder(clist1));
            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.LoadGrammar(gr1);
                sre.SpeechRecognized += sre_recognized;
                sre.SpeechRecognized += operations;
                    sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void sre_recognized(object sender, SpeechRecognizedEventArgs e)
        {

            switch (e.Result.Text.ToString())
            {
                case "one":
                    textBox1.AppendText("1");
                    button1.Focus();
                    //ss.SpeakAsync("hello mitesh");
                    break;

                case "two":
                    //ss.SpeakAsync("i m fine mitesh");
                    textBox1.AppendText("2");
                    button2.Focus();
                    break;
                case "three":
                    textBox1.AppendText("3");
                    button3.Focus();
                    //ss.SpeakAsync("current time is " + DateTime.Now.ToLongTimeString());
                    break;
                case "four":
                    textBox1.AppendText("4");
                    button4.Focus();
                    break;
                case "five":
                    textBox1.AppendText("5");
                    button5.Focus();
                    break;
                case "six":
                    textBox1.AppendText("6");
                    button6.Focus();
                    break;
                case "seven":
                    textBox1.AppendText("7");
                    button7.Focus();
                    break;
                case "eight":
                    textBox1.AppendText("8");
                    button8.Focus();
                    break;
                case "nine":
                    textBox1.AppendText("9");
                    button9.Focus();
                    break;
                case "zero":
                    textBox1.AppendText("0");
                    button10.Focus();

                    break;
                //default:
                //    ss.Speak("please enter the aprropriate choice");
                //    break;

            }
            // MessageBox.Show(e.Result.Text.ToString());
            //textBox1.Text += e.Result.Text.ToString() + Environment.NewLine;
        }
        void operations(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                switch (e.Result.Text.ToString())
                {
                    case "plus":
                        textBox1.AppendText("+");
                        button11.Focus();
                        //ss.SpeakAsync("hello mitesh");
                        break;

                    case "minus":
                        //ss.SpeakAsync("i m fine mitesh");
                        textBox1.AppendText("-");
                        button12.Focus();
                        break;
                    case "multiply":
                        textBox1.AppendText("*");
                        button13.Focus();
                        //ss.SpeakAsync("current time is " + DateTime.Now.ToLongTimeString());
                        break;
                    case "divide":
                        textBox1.AppendText("/");
                        button14.Focus();
                        break;
                    //default :
                    //    ss.Speak("please enter the aprropriate choice");
                    //    break;




                }
                if (e.Result.Text == "enter")
                {

                    button15.Focus();
                    string output = textBox1.Text;
                    char[] operationarray = { '+', '-', '*', '/' };
                    char[] ab = output.ToCharArray();
                    List<char> a = new List<char>();
                    a.Clear();
                    for (int i = 0; i < ab.Length; i++)
                    {
                        if (ab[i] == '+' || ab[i] == '-' || ab[i] == '*' || ab[i] == '/')
                        {
                            a.Add(ab[i]);
                        }
                    }
                    double result = 0;
                   
                    string[] abc = output.Split(operationarray);


                    if (a[0] == '+')
                    {
                        result =  Convert.ToDouble(abc[0]) + Convert.ToDouble(abc[1]);
                        textBox1.Text = result.ToString();
                    }
                    if (a[0] == '-')
                    {
                        result =  Convert.ToDouble(abc[0]) - Convert.ToDouble(abc[1]);
                        textBox1.Text = result.ToString();
                    }
                    if (a[0] == '*')
                    {
                        result = Convert.ToDouble(abc[0]) * Convert.ToDouble(abc[1]);
                        textBox1.Text = result.ToString();
                    }
                    if (a[0] == '/')
                    {
                        if (Convert.ToDouble(abc[1]) == 0)
                        {
                            ss.Speak("divide by zero error");
                        }
                        else
                        {
                            result = result + Convert.ToDouble(abc[0]) / Convert.ToDouble(abc[1]);
                            textBox1.Text = result.ToString();
                        }
                    }

                    for (int i = 1; i < a.Count; i++)
                    {
                        if (a[i] == '+')
                        {
                            result =result + Convert.ToDouble(abc[i + 1]);
                            textBox1.Text = result.ToString();
                        }
                        if (a[i] == '-')
                        {
                            result = result - Convert.ToDouble(abc[i + 1]);
                            textBox1.Text = result.ToString();
                        }
                        if (a[i] == '*')
                        {
                            if (result == 0)
                            {
                                result = 1;
                                result = result * Convert.ToDouble(abc[i + 1]);
                            }
                            else
                            {
                                result = result * Convert.ToDouble(abc[i + 1]);
                            }
                            textBox1.Text = result.ToString();
                        }
                        if (a[i] == '/')
                        {
                            if (Convert.ToDouble(abc[i]) == 0)
                            {
                                ss.Speak("divide by zero error");
                            }
                            else
                            {
                                result = result / Convert.ToDouble(abc[i + 1]);
                                textBox1.Text = result.ToString();
                            }
                        }
                    }
                    ss.Speak("your result is "+textBox1.Text);
                }
                if (e.Result.Text == "erase")
                {
                    button16.Focus();

                    List<char> abcd = textBox1.Text.ToList();

                    abcd.Reverse();
                    abcd.Remove(abcd[0]);
                    string s = "";
                    abcd.Reverse();
                    foreach (char a in abcd)
                    {
                        s = s + a;
                    }

                    textBox1.Text = s;
                }
                if (e.Result.Text == "exit")
                {
                    ss.Speak("thank you very much");
                    Application.Exit();
                }
                if (e.Result.Text == "reset")
                {

                   
                    textBox1.Clear();
                    button17.Focus();

                }
            }
            catch (Exception ex)
            {
            }
    }

      

        private void buttons_Click(object sender, EventArgs e)
        {

            Button myButton = (Button)sender;
            switch (myButton.Text)
            {
                case "0": textBox1.AppendText("0"); break;
                case "1": textBox1.AppendText("1"); break;
                case "2": textBox1.AppendText("2"); break;
                case "3": textBox1.AppendText("3"); break;
                case "4": textBox1.AppendText("4"); break;
                case "5": textBox1.AppendText("5"); break;
                case "6": textBox1.AppendText("6"); break;
                case "7": textBox1.AppendText("7"); break;
                case "8": textBox1.AppendText("8"); break;
                case "9": textBox1.AppendText("9"); break;
                default: MessageBox.Show("Please click valid button");
                    break;
            }

        }

        private void Operation_click(object sender, EventArgs e)
        {


            Button myb = (Button)sender;
            switch (myb.Text.ToLower())
            {
                case "plus":
                    textBox1.AppendText("+");
                    button11.Focus();
                    //ss.SpeakAsync("hello mitesh");
                    break;

                case "minus":
                    //ss.SpeakAsync("i m fine mitesh");
                    textBox1.AppendText("-");
                    button12.Focus();
                    break;
                case "multiply":
                    textBox1.AppendText("*");
                    button13.Focus();
                    //ss.SpeakAsync("current time is " + DateTime.Now.ToLongTimeString());
                    break;
                case "divide":
                    textBox1.AppendText("/");
                    button14.Focus();
                    break;
                case "enter":
                    button15.Focus();
                    string output = textBox1.Text;
                    char[] operationarray = { '+', '-', '*', '/' };
                    char[] ab = output.ToCharArray();
                    List<char> a = new List<char>();
                    a.Clear();
                    for (int i = 0; i < ab.Length; i++)
                    {
                        if (ab[i] == '+' || ab[i] == '-' || ab[i] == '*' || ab[i] == '/')
                        {
                            a.Add(ab[i]);
                        }
                    }
                    double result = 0;
                   
                    string[] abc = output.Split(operationarray);


                    if (a[0] == '+')
                    {
                        result =  Convert.ToDouble(abc[0]) + Convert.ToDouble(abc[1]);
                        textBox1.Text = result.ToString();
                    }
                    if (a[0] == '-')
                    {
                        result =  Convert.ToDouble(abc[0]) - Convert.ToDouble(abc[1]);
                        textBox1.Text = result.ToString();
                    }
                    if (a[0] == '*')
                    {
                        result = Convert.ToDouble(abc[0]) * Convert.ToDouble(abc[1]);
                        textBox1.Text = result.ToString();
                    }
                    if (a[0] == '/')
                    {
                        if (Convert.ToDouble(abc[1]) == 0)
                        {
                            ss.Speak("divide by zero error");
                        }
                        else
                        {
                            result = result + Convert.ToDouble(abc[0]) / Convert.ToDouble(abc[1]);
                            textBox1.Text = result.ToString();
                        }
                    }

                    for (int i = 1; i < a.Count; i++)
                    {
                        if (a[i] == '+')
                        {
                            result =result + Convert.ToDouble(abc[i + 1]);
                            textBox1.Text = result.ToString();
                        }
                        if (a[i] == '-')
                        {
                            result = result - Convert.ToDouble(abc[i + 1]);
                            textBox1.Text = result.ToString();
                        }
                        if (a[i] == '*')
                        {
                            if (result == 0)
                            {
                                result = 1;
                                result = result * Convert.ToDouble(abc[i + 1]);
                            }
                            else
                            {
                                result = result * Convert.ToDouble(abc[i + 1]);
                            }
                            textBox1.Text = result.ToString();
                        }
                        if (a[i] == '/')
                        {
                            if (Convert.ToDouble(abc[i]) == 0)
                            {
                                ss.Speak("divide by zero error");
                            }
                            else
                            {
                                result = result / Convert.ToDouble(abc[i + 1]);
                                textBox1.Text = result.ToString();
                            }
                        }
                    }
                    ss.Speak("your result is "+textBox1.Text);
                    break;
                case "erase": button16.Focus();

                    List<char> abcd = textBox1.Text.ToList();

                    abcd.Reverse();
                    abcd.Remove(abcd[0]);
                    string s = "";
                    abcd.Reverse();
                    foreach (char a1 in abcd)
                    {
                        s = s + a1;
                    }

                    textBox1.Text = s;
                    break;
                case "reset": textBox1.Text = "";
                    break;
                default :
                    ss.Speak("please speak the aprropriate choice");
                    break;

            }

        }
    }
}
