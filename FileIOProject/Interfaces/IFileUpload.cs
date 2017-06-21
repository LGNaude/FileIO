using FileIOProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FileIOProject.Interfaces
{
    public interface IFileUploadRepository: IDisposable
    {
        List<NamesFrequencyViewModel> GetNamesFrequency(DataTable csvTable);

        List<AddressSortedViewModel> GetAddressSorted(DataTable csvTable);
    }
}