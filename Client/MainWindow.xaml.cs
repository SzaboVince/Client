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

namespace Szevasz
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		peopleservice service = new peopleservice();
		public MainWindow()
		{
			InitializeComponent();
			peopleTable.ItemsSource = service.GetAll();
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			Window1 personForm = new Window1();
			personForm.Closed += (_, _) =>
			{
				peopleTable.ItemsSource = service.GetAll();
			};
			personForm.ShowDialog();
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			person selected = peopleTable.SelectedItem as person;
			if (selected == null)
			{
				MessageBox.Show("Törléshez előbb válasszon ki elemet!");
				return;
			}

			MessageBoxResult clickedButton =
				MessageBox.Show($"Biztos, hogy törölni szeretné az alábbi embert: {selected.Name}", "Törlés", MessageBoxButton.YesNo);
			if (clickedButton == MessageBoxResult.Yes)
			{
				if (service.Delete(selected))
				{
					MessageBox.Show("Sikeres törlés!");
				}
				else
				{
					MessageBox.Show("Hiba történt a törlés során!");
				}
				peopleTable.ItemsSource = service.GetAll();
			}
		}

		private void Update_Click(object sender, RoutedEventArgs e)
		{
			person selected = peopleTable.SelectedItem as person;
			if (selected == null)
			{
				MessageBox.Show("Módosításhoz előbb válasszon ki elemet!");
				return;
			}

			Window1 personForm = new Window1(selected);
			personForm.Closed += (_, _) =>
			{
				peopleTable.ItemsSource = service.GetAll();
			};
			personForm.ShowDialog();
		}
	}
}
