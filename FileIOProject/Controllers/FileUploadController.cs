using FileIOProject.Interfaces;
using FileIOProject.Models;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace FileIOProject.Controllers
{
    /// <summary>
    /// This is the Controller tasked with dealing with the file upload
    /// </summary>
    public class FileUploadController : Controller
    {
        IFileUploadRepository m_FileUploadRepository;

        /// <summary>
        /// This is to demonstrate the use of Inversion of Control by using Ninject to inject the classes that will be used upon
        /// the controller's Construction. By specifying the Interface, the developer can code against this interface, knowing that whatever will be passed in here,
        /// must implement the specific Interface.
        /// </summary>
        /// <param name="fileUpload"></param>
        public FileUploadController(IFileUploadRepository fileUpload)
        {
            m_FileUploadRepository = fileUpload;
        }

        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        List<NamesFrequencyViewModel> listNamesFrequencyViewModel = new List<Models.NamesFrequencyViewModel>();
                        List<AddressSortedViewModel> listAddressSortedViewModel = new List<Models.AddressSortedViewModel>();

                        Stream stream = upload.InputStream;
                        DataTable csvTable = new DataTable();
                        using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                            listNamesFrequencyViewModel = m_FileUploadRepository.GetNamesFrequency(csvTable);
                            listAddressSortedViewModel = m_FileUploadRepository.GetAddressSorted(csvTable);

                            // write Names Frequency to File
                            bool resultNamesFreq = writeNamesFreqToFile(listNamesFrequencyViewModel);
                            // write Streetnames sorted to File
                            bool resutAddressSort = writeAddressSortedToFile(listAddressSortedViewModel);
                        }

                        // THis is just extra to show how you pass extra data to the view via the ViewBag; and then use partial views
                        // to render the output for NamesFrequency and AddressSorted.
                        ViewBag.NamesFreq = listNamesFrequencyViewModel;
                        ViewBag.AddressSorted = listAddressSortedViewModel;
                        return View(csvTable);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }

        private bool writeNamesFreqToFile(List<NamesFrequencyViewModel> namesFrequencyViewModel)
        {
            string filePath = HostingEnvironment.MapPath("~/App_Data/NamesFrequency.txt");

            try
            {
                using (System.IO.StreamWriter file = new StreamWriter(filePath))
                {
                    foreach (var nameFreq in namesFrequencyViewModel)
                    {
                        file.WriteLine($"{nameFreq.FirstOrLastName}, {nameFreq.Frequency}");
                    }
                }
            }
            catch (Exception ex)
            {
                // log exception...
                return false;
            }

            return true;
        }

        private bool writeAddressSortedToFile(List<AddressSortedViewModel> addressSortedViewModel)
        {
            string filePath = HostingEnvironment.MapPath("~/App_Data/AddressSorted.txt");

            try
            {
                using (System.IO.StreamWriter file = new StreamWriter(filePath))
                {
                    foreach (var address in addressSortedViewModel)
                    {
                        file.WriteLine($"{address.StreetName}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception...
                return false;
            }
            return true;
        }
    }
}
