using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace GestaoPDF.Client.Components.Shared.Componentes
{
    public class VisualizarPDFBase : ComponentBase
    {
        [Inject]
        private IJSRuntime JS { get; set; } = null!;

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; } = null!;

        [Parameter]
        public string URL { get; set; } = string.Empty;

        protected string Style { get; set; }

        protected void Fechar() =>
            MudDialog.Cancel();

        public VisualizarPDFBase()
        {
            Style = "width: 500px;height: 500px;";
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Task.Delay(500);

                await JS.InvokeVoidAsync("ExibirPDF", URL);

                Style = "width: 501px; height: 501px;";

                StateHasChanged();
            }
        }
    }
}
