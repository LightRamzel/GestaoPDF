namespace GestaoPDF.Data.Interface;

public interface IFolderPicker
{
    Task<string> PickFolder();
}
