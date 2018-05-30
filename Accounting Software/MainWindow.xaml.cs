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

namespace Accounting_Software
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_RefreshEntryList_Click(object sender, RoutedEventArgs e)
        {
            var entrySource = Entry_Form.RefreshEntries();
            listbox_Entries.ItemsSource = entrySource;
            MessageBox.Show(listbox_Entries.SelectedIndex.ToString());
        }

        private void listbox_Entries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (listbox_Entries.SelectedIndex != -1)
                {
                    string currentEntryString = listbox_Entries.Items[listbox_Entries.SelectedIndex].ToString();
                    string[] currentEntryArray = currentEntryString.Split(',');
                    datepicker_Date.Text = currentEntryArray[0];
                    txtbox_Account.Text = currentEntryArray[1];
                    txtbox_Amount.Text = currentEntryArray[2];
                    txtbox_Description.Text = currentEntryArray[3];
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: ItemCount" + listbox_Entries.Items.Count + " Index Array Value: " + listbox_Entries.SelectedIndex + " |"+ error.ToString());
            }
            
        }

        private void btn_SubmitEntry_Click(object sender, RoutedEventArgs e)
        {
            Entry_Form.SubmitEntry(datepicker_Date.Text + "," + txtbox_Account.Text.ToString() + "," + "$" + txtbox_Amount.Text.ToString() + "," + txtbox_Description.Text.ToString());
        }

        private void btn_EditEntry_Click(object sender, RoutedEventArgs e)
        {
            String entryString = (datepicker_Date.Text + "," + txtbox_Account.Text.ToString() + "," + "$" + txtbox_Amount.Text.ToString() + "," + txtbox_Description.Text.ToString());
            int entrySelected = listbox_Entries.SelectedIndex;
            String allEntriesWithEdit = "";

            listbox_Entries.SelectedItem = -1; // clear entry slection so that it doesnt cause errorfergfff

            for (int i = 0; i < listbox_Entries.Items.Count; i++)
            {
                if (i == entrySelected)
                {
                    allEntriesWithEdit += entryString;
                }
                else
                {
                    allEntriesWithEdit += listbox_Entries.Items[i].ToString();
                }
                allEntriesWithEdit += "\n";
            }

            Entry_Form.EditEntry(allEntriesWithEdit);
        }

        private void btn_DeleteEntry_Click(object sender, RoutedEventArgs e)
        {
            String entryString = (datepicker_Date.Text + "," + txtbox_Account.Text.ToString() + "," + "$" + txtbox_Amount.Text.ToString() + "," + txtbox_Description.Text.ToString());
            int entrySelected = listbox_Entries.SelectedIndex;
            String allEntriesWithEdit = "";

            listbox_Entries.SelectedItem = -1; // clear entry slection so that it doesnt cause errorfergfff

            for (int i = 0; i < listbox_Entries.Items.Count; i++)
            {
                if (i == entrySelected)
                {
                    MessageBoxResult result = MessageBox.Show("Are You Sure?","",MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        allEntriesWithEdit += "";
                    }
                    else
                    {
                        allEntriesWithEdit += listbox_Entries.Items[i].ToString();
                    }
                }
                else
                {
                    allEntriesWithEdit += listbox_Entries.Items[i].ToString();
                }
                allEntriesWithEdit += "\n";
            }

            Entry_Form.EditEntry(allEntriesWithEdit);
        }

    }
}
