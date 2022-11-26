using GestaoPDF.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Client.Wpf.Services
{
	public class FolderPicker : IFolderPicker
	{
		public async Task<string> PickFolder()
		{
			string retorno = string.Empty;

			using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
			{
				System.Windows.Forms.DialogResult result = dialog.ShowDialog();

				if (result == System.Windows.Forms.DialogResult.OK)
					retorno = dialog.SelectedPath;
			}

			return await Task.FromResult(retorno);
		}
	}
}
