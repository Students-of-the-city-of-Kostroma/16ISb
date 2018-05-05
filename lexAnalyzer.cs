using System;
using System.Collections.Generic;

public class LexAnalyzer
{
    /// <summary>
    /// �������� ����������
    /// </summary>
    string idName = null;


    public string IdName
    {
        get { return idName; }
        set { idName = value; }
    }
    /// <summary>
    /// ����� ����������
    /// </summary>
    float? answer = null;

    public float? Answer
    {
        get { return answer; }
        set { answer = value; }
    }


    char[] charArr;//the entirety of the user input
    char nextChar;

    string lexeme;//temporary storage for single lexemes
    /// <summary>
    /// ��� ����� ������� ��������� �� ��������������� - ������ ����� ���� ������������� (1-1)
    /// </summary>
    List<string> lexemes = new List<string>();//storage for all lexemes

    public List<string> Lexemes
    {
        get { return lexemes; }
        set { lexemes = value; }
    }
    /// <summary>
    /// �������������� - ������ �������������� ���� �����(1-1)
    /// </summary>
    public List<string> tokens = new List<string>();//public list to pass into the parser

    //a constructor to pass in the supplied equation and begin analysis
    public LexAnalyzer(string eq)
    {
        charArr = eq.ToCharArray();

        StartAnalysis();

        int a;
        if (!int.TryParse(lexemes[0], out a)) idName = lexemes[0];
        else throw new Exception("ID : It can not be integer");


    }

    //begin analysis of string
    void StartAnalysis()
    {
        //����������� ���� ��� �������� � �������� ����� �����,( 123.123.123, ����� �� ��� ����� ��������� ��� ������� ���������)
        bool flagDot = false;
        int position = 0;
        nextChar = charArr[position];
        //scan through the entire input string
        while (nextChar != ';')
        {
            //ID subroutine
            if (Char.IsLetter(nextChar) || nextChar == '.')
            {
                while (Char.IsLetterOrDigit(nextChar) || nextChar == '.')
                {
                    lexeme = lexeme + nextChar.ToString();
                    position++;
                    nextChar = charArr[position];
                }
                LexLookup(lexeme);
            }
            //Number subroutine
            else if (Char.IsDigit(nextChar) || nextChar == '.')
            {
                while (Char.IsDigit(nextChar) || nextChar == '.')
                {

                    if (nextChar == '.')
                    {
                        if (flagDot) throw new Exception("�� ��������� �� ���������� ���� �����!");
                        else flagDot = true; //��� ��������� ����� � ����� �� ������ ����  
                    }
                    lexeme = lexeme + nextChar.ToString();
                    position++;
                    nextChar = charArr[position];
                }
                flagDot = false;
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
            case "+":
                tokens.Add("+");
                break;
            case "-":
                tokens.Add("-");
                break;
            case "*":
                tokens.Add("MUL_OP");
                break;
            case "=":
                tokens.Add("=");
                break;
            default:
                if (goAwayChekNumber(lex)) tokens.Add("NUMBER");
                else tokens.Add("IDENT");
                break;
        }
        lexeme = "";
    }
    bool goAwayChekNumber(string lex)
    {
        foreach (char currentChar in lex) if (!Char.IsNumber(currentChar)) if(currentChar != '.') return false;
        return true;
    }
}