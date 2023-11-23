using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Szevasz
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
		peopleservice service = new peopleservice();
		person person;
		public Window1()
        {
            InitializeComponent();
        }
		public Window1(person person)
		{
			InitializeComponent();
			this.person=person;
			Loadperson();
			addButton.Visibility = Visibility.Collapsed;
			updateButton.Visibility = Visibility.Collapsed;
		}
		private void Loadperson()
		{
			nev.Text = this.person.Name;
			otthon.Text = this.person.Home;
			imej.Text = this.person.Imei.ToString();
		}
		private void Add_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				person person = CreatePersonFromInputFields();
				person newperson = service.Add(person);
				if (newperson.Id != 0)
				{
					MessageBox.Show("Sikeres felvétel");
					nev.Text = "";
					otthon.Text = "";
					imej.Text = "";
				}
				else
				{
					MessageBox.Show("Hiba történt a felvétel során!");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		private void Update_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				person person = CreatePersonFromInputFields();
				person updated = service.Update(this.person.Id, person);
				if (updated.Id != 0)
				{
					MessageBox.Show("Sikeres módosítás!");
					this.Close();
				}
				else
				{
					MessageBox.Show("Hiba történt a módosítás során!");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private person CreatePersonFromInputFields()
		{
			string name = nev.Text.Trim();
			string home = otthon.Text.Trim();
			string imeiText = imej.Text.Trim();

			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Név kitöltése kötelező!");
			}

			if (string.IsNullOrEmpty(home))
			{
				throw new Exception("Lakcím kitöltése kötelező!");
			}

			if (!int.TryParse(imeiText, out int imei))
			{
				throw new Exception("Az IMEI azonosító csak szám lehet!");
			}

			person person = new person();
			person.Name = name;
			person.Home = home;
			person.Imei = imei;
			return person;
		}
	}
}
