using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class rpn
    {
        public enum TTokTypes
        {
            NONE = 0,
            ALPHA,
            NUMBER,
            SYSMBOL
        };

        public struct TToken
        {
            public TTokTypes sType;
            public string sToken;
        }

        private static List<TToken> Tokens = new List<TToken>();
        private static string m_expr = string.Empty;
        private static Stack<double> al;
        private static Dictionary<string, double> vars = new Dictionary<string, double>();

        public rpn(string expr)
        {
            m_expr = expr;
        }

        private static bool isSymb(char c)
        {
            if ((c == '+') || (c == '-') ||
                (c == '/') || (c == '*'))
            {
                return true;
            }

            return false;
        }

        private static bool GetTokens(string expr)
        {
            int i = 0;
            string tok = string.Empty;
            TToken token;

            bool IsGood = true;

            while (i <= expr.Length)
            {
                tok = string.Empty;

                if (expr[i] == '\r')
                {
                    i += 2;
                }

                if (i > expr.Length - 1) { break; }

                if (char.IsWhiteSpace(expr[i]))
                {
                    while (char.IsWhiteSpace(expr[i]))
                    {
                        i++;
                        if (i > expr.Length - 1) { break; }
                    }
                }

                if (char.IsLetter(expr[i]))
                {
                    while (char.IsLetter(expr[i]) || (expr[i] == ':'))
                    {
                        tok += expr[i];
                        i++;
                        if (i > expr.Length - 1) { break; }
                    }
                    token.sType = TTokTypes.ALPHA;
                    token.sToken = tok;
                    Tokens.Add(token);
                }
                else if (char.IsDigit(expr[i]))
                {
                    while (char.IsDigit(expr[i]) || (expr[i] == '.'))
                    {
                        tok += expr[i];
                        i++;
                        if (i > expr.Length - 1) { break; }
                    }
                    token.sType = TTokTypes.NUMBER;
                    token.sToken = tok;
                    Tokens.Add(token);
                }
                else if (isSymb(expr[i]))
                {
                    tok = expr[i].ToString();
                    token.sType = TTokTypes.SYSMBOL;
                    token.sToken = tok;
                    Tokens.Add(token);
                    i++;
                    if (i > expr.Length - 1) { break; }
                }
                else
                {
                    IsGood = false;
                    Tokens.Clear();
                    break;
                }

            }
            return IsGood;
        }

        private static double calcsum(string expr)
        {
            GetTokens(expr);
            double ret = 0;
            int i = 0;
            double a = 0;
            double b = 0;
            string tok = string.Empty;
            //Go though the tokens
            al = new Stack<double>();
            int x = 0;
            while (i < Tokens.Count)
            {
                tok = Tokens[i].sToken;

                switch (Tokens[i].sType)
                {
                    case TTokTypes.NONE:
                        break;
                    case TTokTypes.ALPHA:
                        //Get value from variable and push onto the stack.
                        a = vars[tok.ToUpper()];
                        //Push value.
                        al.Push(a);
                        break;
                    case TTokTypes.NUMBER:
                        //Push value on the stack.
                        al.Push(double.Parse(tok));
                        break;
                    case TTokTypes.SYSMBOL:
                        switch (tok[0])
                        {
                            case '+':
                                //Pop two items and push back result.
                                a = al.Pop();
                                b = al.Pop();
                                ret = a + b;
                                break;
                            case '-':
                                a = al.Pop();
                                b = al.Pop();
                                ret = b - a;
                                break;
                            case '*':
                                a = al.Pop();
                                b = al.Pop();
                                ret = a * b;
                                break;
                            case '/':
                                a = al.Pop();
                                b = al.Pop();
                                ret = (b / a);
                                break;
                            case '^':
                                a = al.Pop();
                                b = al.Pop();
                                ret = ((int)b ^ (int)a);
                                break;
                        }
                        //Push result onto stack.
                        al.Push(ret);
                        break;
                }
                i++;
            }
            //Clear stack.
            while (x < al.Count) { al.Pop(); }
            //Return result.
            return ret;
        }

        public static void AddVar(string key, double num)
        {
            vars.Add(key, num);
        }

        public double Result
        {
            get
            {
                return calcsum(m_expr);
            }
        }
    }
}
