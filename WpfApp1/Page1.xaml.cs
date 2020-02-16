using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private rpn calc;
        private infix2postfix infix_conv;

        public Page1()
        {
            InitializeComponent();



            ////


            List<Number> hh = new List<Number>();
            hh.Add( new Number() { origNumber = 25 });
            hh.Add(new Number() { origNumber = 19 });
            hh.Add(new Number() { origNumber = 44444444 });

            //hh.Sort();

            List<Number> hh2 = new List<Number>();
            ////hh2 = SumSort(hh);

            //orderWeight("25 19 44444444");
            ////////orderWeight("2000 11 11");

            //orderWeight("56 65 74 100 99 68 86 180 90");
            //orderWeight("56 65 74 100 99 68 86 180 90");
            //RPNCalc("5 1 2 + 4 * + 3 -");

            //RPNCalc("1 2 3.5");
            evaluate("1 2 3.5");

            evaluate("10000 123 +");


            RPNCalc("1 3 +");
            ///



            //Convert infix expression to postfix.
            //infix_conv = new infix2postfix("5 1 2 + 4 * + 3 -");
            infix_conv = new infix2postfix("1 2 3.5");
            //Set textbox with RPN expression.
            string txtExpr = infix2postfix.RetVal();
            //Calculate the RPN expression
            calc = new rpn(txtExpr);
            //Display result
            string txtResult = calc.Result.ToString();

            ///RPNCalc("10000 123 +");

           

            ////


            int y = limit(5);

            int u = limit(3);


        }

        public double evaluate(String expr)
        {
            double result = 0.0;

            if (!string.IsNullOrWhiteSpace(expr))
            {
                result = RPNCalc(expr);
            }

            return result;
        }

        private double RPNCalc(string rpnValue)
        {
            string textBox1 = "";
            string textBox2 = "";
            double result = 0.0;

            Stack<double> stackCreated = new Stack<double>();

            try
            {
                var tokens = rpnValue.Replace("(", " ").Replace(")", " ")
                                     .Split().Where(s => !String.IsNullOrWhiteSpace(s));
                foreach (var t in tokens)
                {
                    try
                    {
                        //stackCreated.Push(Convert.ToInt32(t));
                        stackCreated.Push(Convert.ToDouble(t));
                    }
                    catch
                    {
                        double store1 = stackCreated.Pop();
                        double store2 = stackCreated.Pop();
                        switch (t)
                        {
                            case "+": store2 += store1; break;
                            case "-": store2 -= store1; break;
                            case "*": store2 *= store1; break;
                            case "/": store2 /= store1; break;
                            case "%": store2 %= store1; break;
                            case "^": store2 = (int)Math.Pow(store1, store2); break;
                            default: throw new Exception();
                        }
                        stackCreated.Push(store2);
                    }
                }

                if (stackCreated.Count != 1)
                {
                    // MessageBox.Show("Please check the input");
                    textBox1 = stackCreated.Pop().ToString();
                }
                else
                    textBox1 = stackCreated.Pop().ToString();

                result = double.Parse(textBox1);
            }
            catch (Exception e)
            {
                MessageBox.Show("Please check the input");
            }

            //textBox2 += rpnValue;

            

            return result;
            //textBox1.Clear();
        }



        public static string orderWeight(string str)
        {
            // your code
            string result = "";

            if (!string.IsNullOrWhiteSpace(str))
            {
                string[] arr = str.Split(new char[] { ' ' });
                List<Number> lNumbers1 = new List<Number>();

                foreach (string item in arr)
                {
                    lNumbers1.Add(new Number() { origNumber = Convert.ToInt64(item) });
                }

                List<Number> lNumbers2 = new List<Number>();
                lNumbers2 = SumSort(lNumbers1);


                foreach (Number item in lNumbers2)
                {
                    result += item.origNumber + " ";
                }
            }

            return result.TrimEnd();
        }

        public class Number
        {
            public long dsum;
            public long origNumber;
        }

        public class DinoComparer : IComparer<Number>
        {
            public int Compare(Number x, Number y)
            {

                if (x.dsum != y.dsum)
                {
                    return x.dsum.CompareTo(y.dsum);
                }
                else
                {
                    return x.origNumber.ToString().CompareTo(y.origNumber.ToString());
                }
                
            }
        }

        private static List<Number> SumSort(List<Number> orig)
        {
            List<Number> result = new List<Number>();


            foreach (Number item in orig)
            {
                //char[] arr = item.origNumber.ToString().ToArray();


                long[] arr2 = GetDigits(item.origNumber);

                long itemSum = 0;

                foreach (char item2 in arr2)
                {
                    itemSum += item2;// Convert.ToInt32(item2);
                }

                item.dsum = itemSum;

                result.Add(item);
            }

            result.Sort(new DinoComparer());
            //result.Reverse();


            return result;

        }

        public static long[] GetDigits(long number)
        {
            string temp = number.ToString();
            long[] rtn = new long[temp.Length];

            for (int i = 0; i < rtn.Length; i++)
            {
                rtn[i] = long.Parse(temp[i].ToString());
            }
            return rtn;
        }



        private void ScaleTransform_Changed(object sender, EventArgs e)
        {

        }

        private void xformSecond_Changed(object sender, EventArgs e)
        {
            tt.Text = (sender as System.Windows.Media.RotateTransform).Angle.ToString();
            

        }

        private int limit(int y)
        {
            if (y == 6)
                return 3;

            else
                return 3 * limit(y+1);


        }
    }
}
