using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileIOProject;
using FileIOProject.Controllers;
using FileIOProject.Interfaces;
using FileIOProject.Repository;
using System.Data;
using FileIOProject.Models;

namespace FileIOProject.Tests.Controllers
{
    [TestClass]
    public class FileUploadControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            FileUploadRepository fileUploadRepository = new FileUploadRepository();
            FileUploadController controller = new FileUploadController(fileUploadRepository);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetFrequencyViewModel()
        {
            // Arrange
            FileUploadRepository fileUploadRepository = new FileUploadRepository();
            FileUploadController controller = new FileUploadController(fileUploadRepository);

            // populate dummy DataTable
            DataTable dummyDT = makeDummeDataTable();

            List<NamesFrequencyViewModel> listNamesFrequencyViewModel = new List<Models.NamesFrequencyViewModel>();
            listNamesFrequencyViewModel = fileUploadRepository.GetNamesFrequency(dummyDT);
            

            // Do 1 or more Asserts here...
            Assert.AreEqual(9, listNamesFrequencyViewModel.Count);
            Assert.AreEqual("James", listNamesFrequencyViewModel.ToArray()[0].FirstOrLastName);
            Assert.AreEqual(3, listNamesFrequencyViewModel.ToArray()[0].Frequency);
        }

        [TestMethod]
        public void TestGetAddressSortedViewModel()
        {
            // Arrange
            FileUploadRepository fileUploadRepository = new FileUploadRepository();
            FileUploadController controller = new FileUploadController(fileUploadRepository);

            // populate dummy DataTable
            DataTable dummyDT = makeDummeDataTable();

            List<AddressSortedViewModel> listAddressSortedViewModel = new List<Models.AddressSortedViewModel>();
            listAddressSortedViewModel = fileUploadRepository.GetAddressSorted(dummyDT);

            // Do 1 or more Asserts here...
            Assert.AreEqual(8, listAddressSortedViewModel.Count);
            Assert.AreEqual("65, Ambling Way", listAddressSortedViewModel.ToArray()[0].StreetName);
        }

        private DataTable makeDummeDataTable()
        {
            DataTable dummyDT = new DataTable();
            dummyDT.Columns.Add("FirstName");
            dummyDT.Columns.Add("LastName");
            dummyDT.Columns.Add("Address");
            dummyDT.Columns.Add("PhoneNumber");

            DataRow row1 = dummyDT.NewRow();
            row1["FirstName"] = "Jimmy";
            row1["LastName"] = "Smith";
            row1["Address"] = "102 Long Lane";
            row1["PhoneNumber"] = "64645366";

            DataRow row2 = dummyDT.NewRow();
            row2["FirstName"] = "Clive";
            row2["LastName"] = "Owen";
            row2["Address"] = "65 Ambling Way";
            row2["PhoneNumber"] = "64645366";

            DataRow row3 = dummyDT.NewRow();
            row3["FirstName"] = "James";
            row3["LastName"] = "Brown";
            row3["Address"] = "82 Stewart St";
            row3["PhoneNumber"] = "64645366";

            DataRow row4 = dummyDT.NewRow();
            row4["FirstName"] = "Graham";
            row4["LastName"] = "Howe";
            row4["Address"] = "12 Howard St";
            row4["PhoneNumber"] = "64645366";

            DataRow row5 = dummyDT.NewRow();
            row5["FirstName"] = "John";
            row5["LastName"] = "Howe";
            row5["Address"] = "78 Short Lane";
            row5["PhoneNumber"] = "64645366";

            DataRow row6 = dummyDT.NewRow();
            row6["FirstName"] = "Clive";
            row6["LastName"] = "Smith";
            row6["Address"] = "49 Sutherland St";
            row6["PhoneNumber"] = "64645366";

            DataRow row7 = dummyDT.NewRow();
            row7["FirstName"] = "James";
            row7["LastName"] = "Owen";
            row7["Address"] = "8 Crimson Road";
            row7["PhoneNumber"] = "64645366";

            DataRow row8 = dummyDT.NewRow();
            row8["FirstName"] = "James";
            row8["LastName"] = "Brown";
            row8["Address"] = "94 Roland St";
            row8["PhoneNumber"] = "64645366";


            dummyDT.Rows.Add(row1);
            dummyDT.Rows.Add(row2);
            dummyDT.Rows.Add(row3);
            dummyDT.Rows.Add(row4);
            dummyDT.Rows.Add(row5);
            dummyDT.Rows.Add(row6);
            dummyDT.Rows.Add(row7);
            dummyDT.Rows.Add(row8);

            return dummyDT;
        }
    }
}
