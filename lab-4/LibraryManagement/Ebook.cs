namespace LibraryManagement;

public class EBook : Book
{
    public string FileFormat { get; }

    public EBook(int id, string title, string author, string fileFormat) : base(id, title, author)
    {
        if (string.IsNullOrWhiteSpace(fileFormat))
        {
            throw new ArgumentException("File format cannot be empty!", nameof(fileFormat));
        }

        FileFormat = fileFormat;
    }

    public override string DisplayInfo()
    {
        return $"{base.DisplayInfo()} | FileFormat: {FileFormat}";
    }
}