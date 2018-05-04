using System;
using System.Collections.Generic;

public class LexAnalyzer
{
    string idName = null;

    
    public string IdName
    {
        get { return idName; }
        set { idName = value; }
    }

    int? answer = null;

    public int? Answer
    {
        get { return answer; }
        set { answer = value; }
    }


    char[] charArr;//the entirety of the user input
    char nextChar;
    string lexeme;//temporary storage for single lexemes
    List<string> lexemes = new List<string>();//storage for all lexemes

    public List<string> Lexemes
    {
        get { return lexemes; }
        set { lexemes = value; }
    }
    public List<string> tokens = new List<string>();//public list to pass into the parser

    //a constructor to pass in the supplied equation and begin analysis
    public LexAnalyzer(string eq)
    {
        charArr = eq.ToCharArray();
       // Console.WriteLine("Lexical Analysis:");
       // Console.WriteLine();
       // Console.WriteLine("Lexeme | Token");
       // Console.WriteLine("---------------");
        StartAnalysis();
      //  for (int i = 0; i < lexemes.Count; i++)
     //   {
         //   Console.WriteLine("{0,-7}| {1,-7}", lexemes[i], tokens[i]);
     //   }
      //  Console.WriteLine();
        int a;
        if (!int.TryParse(lexemes[0], out a)) idName = lexemes[0];
        
       
    }

    //begin analysis of string
    void StartAnalysis()
    {
        int position = 0;
        nextChar = charArr[position];
        //scan through the entire input string
        while (nextChar != '$')
        {
            //ID subroutine
            if (Char.IsLetter(nextChar))
            {
                while (Char.IsLetterOrDigit(nextChar))
                {
                    lexeme = lexeme + nextChar.ToString();
                    position++;
                    nextChar = charArr[position];
                }
                LexLookup(lexeme);
            }
            //Number subroutine
            else if (Char.IsDigit(nextChar))
            {
                while (Char.IsDigit(nextChar))
                {
                    lexeme = lexeme + nextChar.ToString();
                    position++;
                    nextChar = charArr[position];
                }
                LexLookup(lexeme);
            }
            //Whitespace subroutine
            else if (Char.IsWhiteSpace(nextChar))
            {
                position++;
                nextChar = charArr[position];
            }
            //Unknown subroutine
            else
            {
                LexLookup(charArr[position].ToString());
                position++;
                nextChar = charArr[position];
            }
        }
    }

    void LexLookup(string lex)
    {
        lexemes.Add(lex);
        switch (lex)
        {
            case "/":
                tokens.Add("/");
                break;
            case "+":
                tokens.Add("+");
                break;
            case "-":
                tokens.Add("-");
                break;
            case "*":
                tokens.Add("/");
                break;
            case "=":
                tokens.Add("=");
                break;    
            default:
                tokens.Add("IDENT");
                break;
        }
        lexeme = "";
    }
}