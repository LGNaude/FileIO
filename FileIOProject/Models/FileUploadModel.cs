namespace FileIOProject.Models
{
    /// <summary>
    /// Class to represent each field in the uploaded file
    /// </summary>
    public class FileUploadModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}