namespace PetHealthcare.API.CustomExceptions
{
    public class AzureBlobStorageException : Exception
    {
        public AzureBlobStorageException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
