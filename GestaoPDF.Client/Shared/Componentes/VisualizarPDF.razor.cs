using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Client.Shared.Componentes
{
    public class VisualizarPDFBase : ComponentBase
    {
        [Inject]
        private IJSRuntime JS { get; set; }

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public string URL { get; set; }

        protected string style { get; set; }

        protected void Fechar() =>
            MudDialog.Cancel();

        public VisualizarPDFBase()
        {
            style = "width: 500px;height: 500px;";
        }

        protected override async Task OnInitializedAsync()
        {
            //await JS.InvokeVoidAsync("ExibirPDF", URL);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Task.Delay(500);

                await JS.InvokeVoidAsync("ExibirPDF", URL);

                style = "width: 501px; height: 501px;";

                StateHasChanged();
            }
        }
    }
}
