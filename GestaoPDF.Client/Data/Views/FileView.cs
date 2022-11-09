using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Client.Data.Views
{
    public class FileView
    {
        public FileView() =>
            Id = Guid.NewGuid();

        public FileView(IBrowserFile File) : this() =>
            this.File = File;

        public Guid Id { get; set; }
        public IBrowserFile File { get; set; }
    }
}
