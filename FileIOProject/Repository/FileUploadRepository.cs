using FileIOProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using FileIOProject.Models;
using System.Data;

namespace FileIOProject.Repository
{
    /// <summary>
    /// This repository class is used to demonstrate a bit of Sepration of Concerns.
    /// It takes the load and complexity off from the Controller and do the main processing here.
    /// Typically this is where the call to the DAL will go out for data persistence
    /// It's good practice to implement MemeoryCache provider here as some data can be cached for use throughout the 
    /// application thus resulting in unneccessary DB calls.
    /// Data is returned to the Controller for rendering the HTML
    /// </summary>
    public class FileUploadRepository : IFileUploadRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<AddressSortedViewModel> GetAddressSorted(DataTable csvTable)
        {
            // load the datatable in to a List of 'FileUploadModel' class
            List<FileUploadModel> listFileUpload = new List<Models.FileUploadModel>();
            foreach (DataRow row in csvTable.Rows)
            {
                listFileUpload.Add(new FileUploadModel
                {
                    FirstName = row.Field<string>("FirstName"),
                    LastName = row.Field<string>("LastName"),
                    Address = row.Field<string>("Address"),
                    PhoneNumber = row.Field<string>("PhoneNumber")
                });
            }

            Dictionary<int, string> streetNameDict = new Dictionary<int, string>();
            foreach (var item in listFileUpload)
            {
                string[] address = item.Address.Split(' ');
                streetNameDict.Add(Convert.ToInt32(address[0]), $"{address[1]} {address[2]}");
            }

            var streetNames = from pair in streetNameDict
                              orderby pair.Value ascending
                              select pair;

            List<AddressSortedViewModel> addressSorted = new List<AddressSortedViewModel>();
            foreach (var streetName in streetNames)
            {
                addressSorted.Add(new AddressSortedViewModel
                {
                    StreetName = $"{streetName.Key}, {streetName.Value}"
                });
            }
            return addressSorted;
        }

        public List<NamesFrequencyViewModel> GetNamesFrequency(DataTable csvTable)
        {
            // load the datatable in to a List of 'FileUploadModel' class
            List<FileUploadModel> listFileUpload = new List<Models.FileUploadModel>();
            foreach (DataRow row in csvTable.Rows)
            {
                listFileUpload.Add(new FileUploadModel
                {
                    FirstName = row.Field<string>("FirstName"),
                    LastName = row.Field<string>("LastName"),
                    Address = row.Field<string>("Address"),
                    PhoneNumber = row.Field<string>("PhoneNumber")
                });
            }

            // Populate a string list with firstnames and lastnames combined
            List<string> namesList = new List<string>();
            namesList.AddRange(listFileUpload.Select(x => x.FirstName));
            namesList.AddRange(listFileUpload.Select(x => x.LastName));

            // Loop through string list and populate dictionary with names and surnames with their frequencies
            Dictionary<string, int> namesDictionary = new Dictionary<string, int>();
            foreach (string name in namesList)
            {
                if (namesDictionary.ContainsKey(name))
                {
                    int freq = namesDictionary[name];
                    namesDictionary[name] = freq + 1;
                }
                else
                {
                    namesDictionary.Add(name, 1);
                }
            }            

            var items = from pair in namesDictionary                        
                        orderby pair.Key ascending
                        orderby pair.Value descending
                        select pair;

            List<NamesFrequencyViewModel> namesFrequencyViewModel = new List<NamesFrequencyViewModel>();

            foreach (var item in items)
            {
                namesFrequencyViewModel.Add(new NamesFrequencyViewModel
                {
                    FirstOrLastName = item.Key,
                    Frequency = item.Value
                });
            }            

            return namesFrequencyViewModel;
        }
    }
}