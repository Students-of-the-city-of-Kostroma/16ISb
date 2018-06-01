using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN
{
    class Program
    {
        static void Main(string[] args)
        {
            string arg;
            Stack<double> st = new Stack<double>();

            while ((arg = Console.ReadLine()) != "exit")
            {
                double num;
                bool isNum = double.TryParse(arg, out num);
                if (isNum)
                    st.Push(num);
                else
                {
                    double op2;
                    switch(arg)
                    {
                        case "+":
                            st.Push(st.Pop() + st.Pop());
                            break;
                        case "*":
                            st.Push(st.Pop() * st.Pop());
                            break;
                        case "-":
                            op2 = st.Pop();
                            st.Push(st.Pop() - op2);
                            break;
                        case "/":
                            op2 = st.Pop();
                            if (op2 != 0.0)
                                st.Push(st.Pop() / op2);
                            else
                                Console.WriteLine("Ошибка. Деление на ноль");
                            break;
                        case "calc":
                            Console.WriteLine("Результат: " + st.Pop());
                            break;
                        default:
                            Console.WriteLine("Ошибка. Неизвестная команда");
                            break;
                    }
                }
            }
        }
    }
}
