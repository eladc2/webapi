using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class infix2postfix
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

        public infix2postfix(string expr)
        {
            m_expr = expr;
        }

        private static bool isSymb(char c)
        {
            if (((c == '+') || (c == '-') ||
                (c == '/') || (c == '*')) ||
                (c == '^') || (c == '(') || (c == ')'))
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
            Tokens.Clear();

            while (i <= expr.Length)
            {
                tok = string.Empty;

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

        static int precedence(char c)
        {
            int order = 0;
            //Order of operators
            switch (c)
            {
                case '(':
                case ')':
                    order = 1;
                    break;
                case '+':
                case '-':
                    order = 2;
                    break;
                case '*':
                case '/':
                    order = 3;
                    break;
                case '^':
                    order = 4;
                    break;
                default:
                    order = 0;
                    break;
            }
            return order;
        }

        public static string RetVal()
        {

            int x = 0;
            Stack<string> stack = new Stack<string>();
            Stack<string> postfix = new Stack<string>();
            string RetVal = string.Empty;
            string[] StrRPN = { };

            string sItem = string.Empty;
            TToken tok;
            TTokTypes types;

            //Fetch the tokens from the expression.
            GetTokens(m_expr);

            while (x < Tokens.Count)
            {
                tok = Tokens[x];
                types = tok.sType;

                //Check for numbers
                if (types == TTokTypes.NUMBER)
                {
                    //Push number onto postfix stack.
                    postfix.Push(tok.sToken);
                }
                else
                {
                    //Check for LPARM
                    if (tok.sToken[0] == '(')
                    {
                        //Push ( onto stack.
                        stack.Push("(");
                    }
                    //Check for closing param
                    else if (tok.sToken[0] == ')')
                    {
                        sItem = stack.Pop();
                        while (sItem[0] != '(')
                        {
                            //Post item of stack onto postfix stack.
                            postfix.Push(sItem);
                            sItem = stack.Pop();
                        }
                    }
                    else
                    {
                        //While the stack is not empty pop of rest of items.
                        while (stack.Count > 0)
                        {
                            //Get top item
                            sItem = stack.Pop();
                            //Check order of operators.
                            if (precedence(sItem[0]) >= precedence(tok.sToken[0]))
                            {
                                //Push operator on stack
                                postfix.Push(sItem);

                            }
                            else
                            {
                                //Push back onto stack.
                                stack.Push(sItem);
                                break;
                            }
                        }
                        //Push current token.
                        stack.Push(tok.sToken);
                    }
                }
                //INC Counter
                x++;
            }

            //Pop off remaining on the stack.
            while (stack.Count > 0)
            {
                //Add to postfix.
                postfix.Push(stack.Pop());
            }

            //Convert stack to string array.
            StrRPN = postfix.Reverse().ToArray();

            for (x = 0; x < StrRPN.Length; x++)
            {
                //Append string to RetVal
                RetVal += StrRPN[x] + " ";
            }
            //Return RPN string
            return RetVal.Trim();
        }
    }
}
