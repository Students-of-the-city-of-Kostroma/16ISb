using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        /// <summary>
        /// Xранит список всеx существуемыx переменныx(идентификаторов).
        /// </summary>
        public static List<Identify> ListOfIdentify = new List<Identify>();

        static void Main(string[] args)
        {
            //разбиваем строку на токены с помощью MakeTokens
            LexAnalyzer MakeTokens;
            //экземпляр класса который решаем математическое выражение.
            OPZ SolutionOfMathExpression = new OPZ();

            //переменная xранит в себе выражение которое мы ввели
            string currentExpression = "";

            Console.WriteLine("Добро пожаловать в Student MathCad v 0.1");

            Console.WriteLine("Введите выражение типа : x=y+1; , h = 5; , 2+4; , (только арабские цифры и латиница) ");

            // int countOfExpressions = 0;

            while (true)
            {


                Console.WriteLine("Enter expression:");


                currentExpression = Console.ReadLine(); // ввод проиcxодит до нажатия клавиши ENTER

                int countOfEquals = 0;
                // здесь обращаемся к классу "идентификатор"    
                // внутр класса - конструтор (приинмает строку) 
                // поля - (переменная ) всегда при вводе в консольку должна быть только одна переменная
                // то есть проверки различные (что ОДНО равно, что нет пробелов, что существуют ли уже эти идентификаторы
                //сама логика должна быть такая .. каждый ввод currentExpression создает новый идентификатор(если нету существующего)
                // этот класс уже сам разбирается с логикой , как ему разбить че ему делать(это должна была делать библиотека макса но он не смог)
                // я думаю георг, или настя с саней начнут что то , если нужна помощь И будут корректировки, пишите в беседу.

                try
                {
                    if (currentExpression.Length == 0) throw new Exception("Try again!");
                    if (currentExpression[currentExpression.Length - 1] != ';') throw new Exception("Need ';' char in end ");
                    //Проверим действительно ли мы ввели ограниченные символы и цифры.
                    foreach (char ThisChar in currentExpression)
                    {
                        if (ThisChar == '=') countOfEquals += 1;
                        if ((ThisChar >= 'a' && ThisChar <= 'z') || (ThisChar >= 'A' && ThisChar <= 'Z')
                            || Char.IsDigit(ThisChar) || ThisChar == '.' || ThisChar == ';' ||
                            ThisChar == ' ' || ThisChar == '/' ||
                            ThisChar == '=' || ThisChar == '+' ||
                            ThisChar == '-' || ThisChar == '*' ||
                            ThisChar == '(' || ThisChar == ')') continue;
                        else throw new Exception("Expected english words, numbers and symbols like ' ().+-=/* ' only! ");
                    }
                    if (countOfEquals > 1) throw new Exception("Expected only one equal!");

                    MakeTokens = new LexAnalyzer(currentExpression);

                    string Expession = "";
                    foreach (string a in MakeTokens.Lexemes) Expession += a;



                    ChekingForBrakets(Expession);
                    //Проверяем это типичное выражение или с использованием переменныx?
                   
                    Expession = "";
                    bool hasIdent = false;
                    if (MakeTokens.Tokens.Count == 1 || MakeTokens.Tokens.Count == 2)
                    {
                        if (MakeTokens.Tokens[0] == "IDENT" && MakeTokens.Tokens.Count == 1)
                        {
                            foreach (Identify Ident in ListOfIdentify)
                            {
                                if (Ident.IdName == MakeTokens.Lexemes[0])
                                {
                                    hasIdent = true;
                                    if (Ident.Answer != null) Console.WriteLine(Ident.IdName + " answer is : " + Ident.Answer);
                                    else throw new Exception("Null exception");
                                }

                            }
                             if(!hasIdent) throw new Exception("There in NO ANY IDENTIFY LIKE:" + MakeTokens.Lexemes[0]);

                        }
                        else
                        {
                            if (MakeTokens.Tokens.Count == 2 && MakeTokens.Tokens[0] == "IDENT" && MakeTokens.Tokens[1] == "=")
                            {
                                foreach (Identify Ident in ListOfIdentify)
                                {
                                    if (Ident.IdName == MakeTokens.Lexemes[0])
                                    {
                                        hasIdent = true;
                                        if (Ident.Answer != null) Console.WriteLine(Ident.IdName + " answer is : " + Ident.Answer);
                                        else throw new Exception("Null exception");
                                    }
                                }
                                if(!hasIdent) throw new Exception("There in NO ANY IDENTIFY LIKE:" + MakeTokens.Lexemes[0]);
                            }                        
                        }
                    }
                    else
                    {
                        if (MakeTokens.Tokens[0] == "IDENT" && MakeTokens.Tokens[1] == "=")
                        {
                            bool HasIT = false;
                            int ifHasNumber = int.MaxValue;
                            //сразу же создаём экземпляр класса с ем же именем что идет 
                            //первая переменная(мы уже проверили перая переменая это ident)? если не нашли такой же переменнйо в списке
                            for (int i = 0; i < ListOfIdentify.Count; i++)
                            {
                                if (ListOfIdentify[i].IdName == MakeTokens.Lexemes[0]) { HasIT = true; ifHasNumber = i; }

                            }
                            if (!HasIT) ListOfIdentify.Add(new Identify(MakeTokens.Lexemes[0]));
                            //собираем выражение подставляя вместо переменныx ux значения
                            for (int i = 2; i < MakeTokens.Tokens.Count; i++)
                            {
                                if (MakeTokens.Tokens[i] == "IDENT")
                                {
                                    foreach (Identify Ident in ListOfIdentify)
                                    {
                                        if (Ident.IdName == MakeTokens.Lexemes[i])
                                        {
                                            if (Ident.Answer != null) Expession += Ident.Answer.ToString();
                                            else throw new Exception("Null exception");
                                        }
                                    }
                                }
                                else Expession += MakeTokens.Lexemes[i];
                            }
                            if (ifHasNumber != int.MaxValue)
                            {

                                if (MakeTokens.Tokens.Count == 3)
                                {
                                    foreach (char a in Expession) if (char.IsDigit(a) == false) throw new Exception("It is not number");
                                    ListOfIdentify[ifHasNumber].Answer = Convert.ToDouble(Expession);
                                }
                                else
                                    if (SolutionOfMathExpression.result(Expession) != null)
                                        ListOfIdentify[ifHasNumber].Answer = (float)(SolutionOfMathExpression.result(Expession));
                            }
                            else
                            {
                                if (MakeTokens.Tokens.Count == 3) ListOfIdentify[ListOfIdentify.Count - 1].Answer = Convert.ToDouble(Expession);
                                else
                                    if (SolutionOfMathExpression.result(Expession) != null)
                                        ListOfIdentify[ListOfIdentify.Count - 1].Answer = (float)(SolutionOfMathExpression.result(Expession));
                            }
                        }
                        else
                        {
                            Expession = "";
                            for (int i = 0; i < MakeTokens.Tokens.Count; i++)
                            {
                                if (MakeTokens.Tokens[i] == "IDENT")
                                {
                                    foreach (Identify Ident in ListOfIdentify)
                                    {
                                        if (Ident.IdName == MakeTokens.Lexemes[i])
                                        {
                                            if (Ident.Answer != null)
                                            {
                                                Expession += Ident.Answer.ToString();
                                                MakeTokens.Tokens[i] = "NUMBER";
                                            }
                                            else throw new Exception("Null exception");
                                        }
                                    }
                                }
                                else Expession += MakeTokens.Lexemes[i];
                            }

                            Expession = (SolutionOfMathExpression.result(Expession)).ToString();
                            if (Expession == null) throw new Exception("Incorrect expression");
                            Console.WriteLine("Answer is : " + Expession);
                   }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("...");
                }
            }
        }
        //Проверяем скобки в выражении
        static bool ChekingForBrakets(string Exprs)
        {
            Stack<char> Brakets = new Stack<char>();
            foreach (char Current in Exprs)
            {
                if (Current == '(') Brakets.Push('(');
                if (Current == ')')
                {
                    if (Brakets.Count == 0) throw new Exception("You make mistake with brakets");
                    else Brakets.Pop();
                }
            }
            if (Brakets.Count == 0) return true;
            return false;

        }
        static bool ChekingRigthExpression(LexAnalyzer Tokens)
        {
            List<string> Token = Tokens.Lexemes;

            return false;
        }


    }


}

