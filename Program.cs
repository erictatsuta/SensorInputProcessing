///Sao Paulo, May 20th 2021
//


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SensorInputProcessing
{
    class Program
    {
        static void FileImporter(string filename)
        {
            //se o arquivo é o test, faz a importação do arquivo test.csv
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filePath = dir + "\\..\\..\\input\\" + filename;
            using (var reader = new StreamReader(filePath))
            {
                List<String> listA = new List<String>();  //Creating the temperature list
                List<String> listB = new List<String>();  //Creating the time list
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    listA.Add(values[0]);  //Adding the values from the .csv file to the temperature list (1rs column in the file)
                    listB.Add(values[1]);  //Adding the values from the .csv file to the time list (2nd column)
                }
            }


        }
        

        static void Main(string[] args)
        {
         


            //read csv file
            FileImporter("test.csv");
            string filePath = @"C:\Users\Eric Tatsuta\source\repos\SensorInputProcessing\input\test.csv";
            using (var reader = new StreamReader(filePath))
            {
                List<String> listA = new List<String>();  //Creating the temperature list
                List<String> listB = new List<String>();  //Creating the time list
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    listA.Add(values[0]);  //Adding the values from the .csv file to the temperature list (1rs column in the file)
                    listB.Add(values[1]);  //Adding the values from the .csv file to the time list (2nd column)
                }
                double value, val = 0.0, temperatureSum = 0, temperature = 0, temperatureaverage = 0.0;
                double temperaturenow, temperaturebefore;
                int i = 0;
                Console.WriteLine("The collected values are: ");  //Showing the temperature values to the user
                foreach (var itemA in listA)
                {
                    value = double.Parse(itemA);
                    temperaturebefore = double.Parse(listA[listA.Count - 1]);
                    temperaturenow = value;
                    if (temperaturenow > 150.00 && temperaturenow - temperaturebefore <= 0)
                    {
                        Console.WriteLine("WARNING! The place is at high temperatures");  //Warning message if the temperature is too high
                        Console.WriteLine("\t" + itemA + "°C");
                    }
                    if (temperaturenow < -10.00 && temperaturebefore < 0 && temperaturenow - temperaturebefore <= -1)
                    {
                        Console.WriteLine("WARNING! The place is at low temperatures");  //Warning message if the temperature is too low
                        Console.WriteLine("\t" + itemA + "°C");
                    }
                    if (double.TryParse(itemA, out val))
                    {
                        temperature = val;
                        temperatureSum = temperatureSum + temperature;
                        i++;
                        temperatureaverage = temperatureSum / i;
                    }
                }
                Console.WriteLine("The sum of the temperatures is: " + temperatureSum + "°C");  //Showing the sum of the temperature values to the user
                Console.WriteLine("The value's average is: " + temperatureaverage + "°C");  //Showing the temperature average to the user
                Console.WriteLine("The minimum temperature value is: " + listA.Min() + "°C");  //Showing the minimum temperature value to the user
                Console.WriteLine("The maximum temperature value is: " + listA.Max() + "°C");  //Showing the maximum temperature values to the user

                //para realizar o plot, deve-se primeiro, usando o console do genrenciador de pacotes instalar o scottplot
                //PM> Install-Package ScottPlot -Version 4.0.48
                //mais informações: https://swharden.com/scottplot/cookbooks/4.0.47/#quickstart-quickstart-scatter-plot-quickstart
                
                var plt = new ScottPlot.Plot(400, 300);
                plt.PlotScatter(Array.ConvertAll(listB.ToArray(), Double.Parse), Array.ConvertAll(listA.ToArray(), Double.Parse));
                plt.Title("Scatter Plot Temp x time");
                plt.YLabel("Temperature");
                plt.XLabel("Time");
                plt.SaveFig("scatterplot.png");

            }
        }
    }
}
