using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Shared.Componentes
{
    public class VisualizarPDFBase : ComponentBase
    {
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public string URL { get; set; }

        protected void Fechar() =>
            MudDialog.Cancel();
    }
}
