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
            Console.WriteLine("Enter expression:");

           string  currentExpression = Console.ReadLine(); // ввод проиcxодит до нажатия клавиши ENTER

            MainMethod(currentExpression);
        }

        public static void MainMethod(string  currentExpression)
        {
            //разбиваем строку на токены с помощью MakeTokens
            LexAnalyzer MakeTokens;
            //экземпляр класса который решаем математическое выражение.
            OPZ SolutionOfMathExpression = new OPZ();

            //переменная xранит в себе выражение которое мы ввели
            

            Console.WriteLine("Добро пожаловать в Student MathCad v 0.1");

            Console.WriteLine("Введите выражение типа : x=y+1 , h = 5 , 2+4 , (только арабские цифры и латиница) ");

            // int countOfExpressions = 0;

            while (true)
            {
                

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
                    bool OnlyMathExpression = true;
                    //видим что первым стоит идентификатор (значит уже не простое мат. выражение)
                    Expession = "";
                    if (MakeTokens.Tokens.Count == 1)
                    {
                        if (MakeTokens.Tokens[0] == "IDENT")
                        {
                            foreach (Identify Ident in ListOfIdentify)
                            {
                                if (Ident.IdName == MakeTokens.Lexemes[0])
                                {
                                    if (Ident.Answer != null) Console.WriteLine(Ident.IdName + " answer is : " + Ident.Answer);
                                    else throw new Exception("Null exception");
                                }
                            }

                        }
                        else throw new Exception("There in NO ANY IDENTIFY LIKE:" + MakeTokens.Lexemes[0]);
                    }
                    else
                    {
                        if (MakeTokens.Tokens[0] == "IDENT" && MakeTokens.Tokens[1] == "=")
                        {
                            //сразу же создаём экземпляр класса с ем же именем что идет первая переменная(мы уже проверили перая переменая это ident)?
                            ListOfIdentify.Add(new Identify(MakeTokens.Lexemes[0]));
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
                            ListOfIdentify[ListOfIdentify.Count - 1].Answer = (float)(SolutionOfMathExpression.result(Expession));
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
                            //MakeTokens = new LexAnalyzer(Expession);
                            for (int i = 0; i < MakeTokens.Tokens.Count; i++)
                            {
                                if (OnlyMathExpression)
                                {
                                    if (i % 2 == 0)
                                    {
                                        if (MakeTokens.Tokens[i] != "NUMBER")
                                        {
                                            OnlyMathExpression = false;
                                            i = 0;
                                        }
                                    }
                                    else
                                    {
                                        if (MakeTokens.Tokens[i] == "+" || MakeTokens.Tokens[i] == "*" ||
                                            MakeTokens.Tokens[i] == "/" || MakeTokens.Tokens[i] == "-") continue;
                                        else
                                        {
                                            OnlyMathExpression = false;
                                            i = 0;
                                        }
                                    }
                                }
                                else throw new Exception("We expected math expression");
                            }
                        }

                        if (OnlyMathExpression) Console.WriteLine(SolutionOfMathExpression.result(Expession));
                        else throw new Exception("You entered incorrect symbol");
                    }
                    //ListOfIdentify.Add(new LexAnalyzer(currentExpression));

                    //Console.WriteLine(SolutionOfMathExpression.result(currentExpression));
                    //  //  currentExpression += " ";
                    //  //countOfExpressions++;
                    //  //ListOfIdentify.Add(new Identify(currentExpression));
                    //  string Exprs = currentExpression;
                    //  currentExpression = currentExpression + "$";
                    //
                    //  ListOfIdentify.Add(new LexAnalyzer(currentExpression));//LexAnalyzer prints its output upon construction

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
                //если есть вxодящая то заносим в стэк если же есть закрывающаяся 
                //првоерим в стэке есть что то если нету то ИСКЛЮЧЕНИЕ (не может быть такой ситуации)
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

