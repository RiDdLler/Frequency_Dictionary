using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;

namespace Frequency_Dictionary
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ParseButton_Click(object sender, RoutedEventArgs e)
		{
			string url = urlTextBox.Text;
			if (!string.IsNullOrWhiteSpace(url))
			{
				Dictionary<string, int> wordFrequency = ParseWebResource(url);
				DisplayWordFrequency(wordFrequency);
			}
			else
			{
				MessageBox.Show("Please enter a valid URL.");
			}
		}

		private Dictionary<string, int> ParseWebResource(string url)
		{
			Dictionary<string, int> wordFrequency = new Dictionary<string, int>();

			using (WebClient client = new WebClient())
			{
				string content = client.DownloadString(url);
				string[] words = content.Split(new[] { ' ', '\t', '\n', '\r', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

				foreach (string word in words)
				{
					string cleanedWord = word.Trim().ToLower();
					if (wordFrequency.ContainsKey(cleanedWord))
					{
						wordFrequency[cleanedWord]++;
					}
					else
					{
						wordFrequency[cleanedWord] = 1;
					}
				}
			}

			return wordFrequency;
		}

		private void DisplayWordFrequency(Dictionary<string, int> wordFrequency)
		{
			var sortedFrequency = wordFrequency.OrderByDescending(pair => pair.Value);

			resultListView.Items.Clear();

			foreach (var pair in sortedFrequency)
			{
				resultListView.Items.Add(new { Word = pair.Key, Frequency = pair.Value });
			}
		}
	}
}

//ПАРСИТЬ ЭТО
//http://dict.ruslang.ru/freq.php
