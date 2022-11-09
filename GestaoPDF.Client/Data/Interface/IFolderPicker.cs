namespace GestaoPDF.Client.Data.Interface;

public interface IFolderPicker
{
    Task<string> PickFolder();
}
