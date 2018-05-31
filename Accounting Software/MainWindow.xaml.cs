using System;
using System.Collections;
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
            InitializeDataGrid();

            try
            {
                AutoRefresh();
            }
            catch(Exception error)
            {
                MessageBox.Show("No Entry Data Found \n\n" + error.ToString());
            }
        }

        private void btn_RefreshEntryList_Click(object sender, RoutedEventArgs e)
        {
            var entrySource = Entry_Form.RefreshEntries();
            datagrid_Entries.Items.Clear();
            AddDataToGrid(entrySource);
        }

        public struct EntryData
        {
            public string Status { set; get; }
            public string Date { set; get; }
            public string Account { set; get; }
            public string Amount { set; get; }
            public string Description { set; get; }
        }

        public void InitializeDataGrid()
        {
            DataGridTextColumn statusColumn = new DataGridTextColumn();
            DataGridTextColumn dateColumn = new DataGridTextColumn();
            DataGridTextColumn accountColumn = new DataGridTextColumn();
            DataGridTextColumn amountColumn = new DataGridTextColumn();
            DataGridTextColumn descriptionColumn = new DataGridTextColumn();

            datagrid_Entries.Columns.Add(statusColumn);
            datagrid_Entries.Columns.Add(dateColumn);
            datagrid_Entries.Columns.Add(accountColumn);
            datagrid_Entries.Columns.Add(amountColumn);
            datagrid_Entries.Columns.Add(descriptionColumn);

            statusColumn.Binding = new Binding("Status");
            dateColumn.Binding = new Binding("Date");
            accountColumn.Binding = new Binding("Account");
            amountColumn.Binding = new Binding("Amount");
            descriptionColumn.Binding = new Binding("Description");

            statusColumn.Header = "Status";
            dateColumn.Header = "Date";
            accountColumn.Header = "Account";
            amountColumn.Header = "Amount";
            descriptionColumn.Header = "Description";
        }

        public void AddDataToGrid(List<string> entryArray)
        {
            foreach (string element in entryArray)
            {
                string[] elementArray = element.Split(',');
                datagrid_Entries.Items.Add(new EntryData { Status = elementArray[0], Date = elementArray[1], Account = elementArray[2], Amount = elementArray[3], Description = elementArray[4] });
            }
        }

        public void AutoRefresh()
        {
            var entrySource = Entry_Form.RefreshEntries();
            datagrid_Entries.Items.Clear();
            AddDataToGrid(entrySource);
        }

        private void btn_SubmitEntry_Click(object sender, RoutedEventArgs e)
        {
            Entry_Form.SubmitEntry(combobox_EntryStatus.Text.ToString() + "," + datepicker_Date.Text + "," + txtbox_Account.Text.ToString() + "," + txtbox_Amount.Text.ToString() + "," + txtbox_Description.Text.ToString());
            AutoRefresh();
        }

        private void btn_EditEntry_Click(object sender, RoutedEventArgs e)
        {
            String entryString = (combobox_EntryStatus.Text.ToString() + "," + datepicker_Date.Text + "," + txtbox_Account.Text.ToString() + "," + txtbox_Amount.Text.ToString() + "," + txtbox_Description.Text.ToString());
            int entrySelected = datagrid_Entries.SelectedIndex;
            String allEntriesWithEdit = "";

            datagrid_Entries.SelectedItem = -1; // clear entry slection so that it doesnt cause errorfergfff

            for (int i = 0; i < datagrid_Entries.Items.Count; i++)
            {
                if (i == entrySelected)
                {
                    allEntriesWithEdit += entryString;
                }
                else
                {
                    allEntriesWithEdit += GetEntrySelectedAsString(i);
                }
                allEntriesWithEdit += "\n";
            }

            Entry_Form.EditEntry(allEntriesWithEdit);
            AutoRefresh();
        }

        private void btn_DeleteEntry_Click(object sender, RoutedEventArgs e)
        {
            String entryString = (combobox_EntryStatus.Text.ToString() + "," + datepicker_Date.Text + "," + txtbox_Account.Text.ToString() + "," + txtbox_Amount.Text.ToString() + "," + txtbox_Description.Text.ToString());
            int entrySelected = datagrid_Entries.SelectedIndex;
            String allEntriesWithEdit = "";

            datagrid_Entries.SelectedItem = -1; // clear entry slection so that it doesnt cause errorfergfff

            for (int i = 0; i < datagrid_Entries.Items.Count; i++)
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
                        allEntriesWithEdit += GetEntrySelectedAsString(i);
                    }
                }
                else
                {
                    allEntriesWithEdit += GetEntrySelectedAsString(i);
                }
                allEntriesWithEdit += "\n";
            }

            Entry_Form.EditEntry(allEntriesWithEdit);
            AutoRefresh();
        }

        private void datagrid_Entries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (datagrid_Entries.SelectedIndex != -1)
                {
                    object item = datagrid_Entries.SelectedItem;
                    combobox_EntryStatus.Text = (datagrid_Entries.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                    datepicker_Date.Text = (datagrid_Entries.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                    txtbox_Account.Text = (datagrid_Entries.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                    txtbox_Amount.Text = (datagrid_Entries.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                    txtbox_Description.Text = (datagrid_Entries.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }

        }

        public string GetEntrySelectedAsString(int entryIndex)
        {
            string entryString = "";
            object item = datagrid_Entries.Items[entryIndex];
            entryString += ((datagrid_Entries.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            entryString += ("," + (datagrid_Entries.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text);
            entryString += ("," + (datagrid_Entries.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text);
            entryString += ("," + (datagrid_Entries.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text);
            entryString += ("," + (datagrid_Entries.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);

            return entryString;
        }
    }
}
