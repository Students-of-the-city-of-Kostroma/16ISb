using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Identify
    {

        public Identify()
        {
        }
        public Identify(string idName)
        {
            this.idName = idName;

        }
        /// <summary>
        /// Название переменной
        /// </summary>
        string idName = null;


        public string IdName
        {
            get { return idName; }
            set { idName = value; }
        }
        /// <summary>
        /// Ответ переменной
        /// </summary>
        float? answer = null;

        public float? Answer
        {
            get { return answer; }
            set { answer = value; }
        }






    }
}
