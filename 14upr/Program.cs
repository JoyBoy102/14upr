using _14upr;
using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.ExcelAc;

class Program
{
    static void Main()
    {
        /*
        List<string> InputStrsLen3 = GenerateInputStrs(3);
        List<string> InputStrsLen4 = GenerateInputStrs(4);
        List<string> InputStrsLen5 = GenerateInputStrs(5);
        */
        for (int i = 3; i < 100; i++)
        {
            List<string> states = new List<string> { "q1_s", "q2_s", "q3_s", "q4_s", "q5_s"};
            List<string> InputStrs = GenerateInputStrs(i);
            Dictionary<string, Dictionary<string, string>> Table = GenerateTransitionTable(states, InputStrs);

            
            foreach (var item in Table)
            {

                if (AllIsUnique(item.Value, states))
                {
                    GenerateExcelFile(Table, states);
                    Console.WriteLine(item.Key);
                    foreach (var item_item in item.Value)
                    {
                        Console.WriteLine($"{item_item.Key} {item_item.Value}");
                    }
                    break;
                } 
            }
           
            

        }
        
        

    }
    public static List<string> GenerateInputStrs(int inputStrLength)
    {
        List<string> InputSrs = new List<string>();
        int startNum = 0;

        // Максимальное значение для троичной системы с заданной длиной строки
        int maxNum = (int)Math.Pow(3, inputStrLength) - 1;

        while (startNum <= maxNum)
        {
            // Преобразуем число в строку в троичной системе
            string ternary = ConvertToBase3(startNum, inputStrLength);

            // Добавляем строку в список
            InputSrs.Add(ternary);

            startNum++;  // Переходим к следующему числу
        }

        return InputSrs;
    }

    // Функция для преобразования числа в троичную систему счисления с добавлением ведущих нулей
    private static string ConvertToBase3(int number, int length)
    {
        List<int> digits = new List<int>();

        // Преобразуем число в троичную систему
        while (number > 0)
        {
            digits.Insert(0, number % 3);  // Вставляем цифры в начало списка
            number /= 3;
        }

        // Если число меньше нужной длины, добавляем ведущие нули
        while (digits.Count < length)
        {
            digits.Insert(0, 0);  // Добавляем ведущие нули
        }

        // Преобразуем список цифр в строку
        return string.Join("", digits.Select(digit => digit.ToString()));
    }


    public static Dictionary<string, Dictionary<string, string>> GenerateTransitionTable(List<string> states, List<string> input_strs)
    {
        SelfTransition selfTransition = new SelfTransition();
        Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();
        foreach (string input_str in input_strs)
        {
            var innerDict = new Dictionary<string, string>();
            foreach (string state in states)
            {
                string result_str = selfTransition.RunSelfTransition(input_str, state);
                innerDict[state] = result_str;
            }
            result[input_str] = innerDict;
        }
        return result;
    }

    public static void GenerateExcelFile(Dictionary<string, Dictionary<string, string>> Table, List<string> states)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.AddWorksheet("TransitionTable");
            int col = 2;
            int row = 2;
            foreach (var item in Table)
            {
                worksheet.Cell(1, col).Value = item.Key;
                col++;
            }

            foreach (var state in states)
            {
                worksheet.Cell(row, 1).Value = state;
                col = 2;
                foreach (var item in Table)
                {
                    worksheet.Cell(row, col).Value = item.Value.GetValueOrDefault(state, "");
                    col++;
                }
                row++;
            }
            workbook.SaveAs(@"C:\Users\Ainur\TransitionTable.xlsx");
        }
        Console.WriteLine("Excel файл был создан успешно.");

    }

    public static bool AllIsUnique(Dictionary<string, string> statesDict, List<string> states)
    {
        List<string> output_strs = new List<string>();
        foreach(var state in statesDict)
        {
            output_strs.Add(state.Value);
        }
        HashSet<string> hashSetOutputStrs = new HashSet<string>(output_strs);
        if (hashSetOutputStrs.Count == states.Count)
            return true;
        return false;
    }
}

