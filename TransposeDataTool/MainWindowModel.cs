using System.IO;
using System.Linq;
using System.Windows;

namespace TransposeDataTool
{
    internal class MainWindowModel
    {
        internal string[,] TransposeData(string[,] data)
        {
            var rows = data.GetLength(0);
            var columns = data.GetLength(1);
            var transposed = new string[columns, rows];

            for (var column = 0; column < columns; column++)
            {
                for (var row = 0; row < rows; row++)
                {
                    //Console.WriteLine($"Data at {column} | {row} : {data[row, column]}");
                    transposed[column, row] = data[row, column];
                }
            }

            return transposed;
        }

        internal void SaveData(string fileName, string[,] data)
        {
            var transposedFileName = Path.GetDirectoryName(fileName) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(fileName) + "_transposed.csv";

            using (StreamWriter file = new StreamWriter(transposedFileName))
            {
                for (var row = 0; row < data.GetLength(0); row++)
                {
                    for (var column = 0; column < data.GetLength(1); column++)
                    {
                        file.Write(data[row, column] + ';');
                    }
                    file.WriteLine();
                }
            }


            MessageBox.Show($"Data successfully transposed into {transposedFileName}");
        }

        internal string[,] ReadFile(string fileName)
        {
            var rows = 0;
            var columns = 0;

            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    rows++;
                    var values = reader.ReadLine().Split(';');
                    values = values.Take(values.Count() - 1).ToArray();
                    columns = values.Length;
                }
            }

            var data = new string[rows, columns];

            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    for (var row = 0; row < rows; row++)
                    {
                        var values = reader.ReadLine().Split(';');
                        values = values.Take(values.Count() - 1).ToArray();

                        for (var column = 0; column < columns; column++)
                        {
                            data[row, column] = values[column];
                        }

                    }
                }
            }
            return data;
        }
    }
}
