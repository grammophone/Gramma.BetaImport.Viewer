using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Grammophone.BetaImport;

namespace Grammophone.BetaImport.Viewer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void CanExecuteOpen(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void ExecuteOpen(object sender, ExecutedRoutedEventArgs e)
		{
			var dialog = new Microsoft.Win32.OpenFileDialog();

			dialog.DefaultExt = ".txt";
			dialog.Filter = "Text documents|*.txt";

			if (dialog.ShowDialog(this) == true)
			{
				using (Stream inputStream = dialog.OpenFile())
				{
					using (BetaReader betaReader = new BetaReader(inputStream, new PrecombinedDiacriticsBetaConverter()))
					//using (BetaReader betaReader = new BetaReader(inputStream, new ComposingDiacriticsBetaConverter()))
					{
						fileTextBox.Text = dialog.FileName;
						contentTextBox.ScrollToHome();
						contentTextBox.Text = betaReader.ReadToEnd();
					}
				}
			}
		}
	}
}
