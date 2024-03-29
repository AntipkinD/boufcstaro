﻿using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            //Чтение файла
            StreamReader file = new("D:/VSWorks/boufcstaro/test.txt");
            string filecontent = file.ReadToEnd();

            //Переименование классов
            filecontent = Regex.Replace(filecontent, @"(?<=\bclass\s+)\w+(?=\s*\{)", "Class0");

            //Очистка от комментариев
            filecontent = Regex.Replace(filecontent, @"/\*(.*?)\*/" + "|" + @"//(.*?)\r?\n", "");

            //Объявление идентификаторов
            var idents = Regex.Matches(filecontent, @"\b\w+\b")
            .OfType<Match>()
            .Select(some => some.Value)
            .Distinct()
            .ToArray();

            //Замена идентификаторов
            for (int i = 0; i < idents.Length; i++)
            {
                string changeids = i < 26 ? ((char)('a' + i)).ToString() : "var" + (i - 26 + 1);
                filecontent = Regex.Replace(filecontent, $@"\b{idents[i]}\b", changeids);
            }

            //Удаление проблелов и отступов 
            filecontent = filecontent.Replace("\n", "").Replace("\r", "").Replace("  ", "");

            Console.WriteLine(filecontent);
        }
        catch (Exception ex)
        {
            ex = new("Что-то пошло не так");
            Console.WriteLine(ex.Message);
        }
    }
}
