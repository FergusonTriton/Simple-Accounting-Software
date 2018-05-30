using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Accounting_Software
{
    class Entry_Form
    {
            // method that reads the entry_Form.csv file and returns that informtation as a String[] array
        public static List<string> RefreshEntries ()
        {
            StreamReader reader = new StreamReader(File.OpenRead(@"Entry_Form.csv"));
            List<string> entryList = new List<String>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!String.IsNullOrWhiteSpace(line))
                {
                    entryList.Add(line);
                }
            }
            reader.Close();
            return entryList;
        }

        public static void SubmitEntry(string currentEntry)
        {
            using (StreamWriter file = File.AppendText(@"Entry_Form.csv"))
            {
                file.WriteLine(currentEntry);
                file.Close();
            }
        }
            
            //Method that takes the listbox_entry items as a string[] array and writes over the 'entry_form.csv' entry file
        public static void EditEntry (String listbox_Entries)
        {
            using (StreamWriter file = new StreamWriter(@"Entry_Form.csv"))
            {
                file.WriteLine(listbox_Entries);
            }
                
        }

        public static void SaveEntryForm()
        {

        }

    }
}
