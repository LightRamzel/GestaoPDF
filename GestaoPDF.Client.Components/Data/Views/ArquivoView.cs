﻿using GestaoPDF.Domain.Entities;

namespace GestaoPDF.Client.Components.Data.Views;

public class ArquivoView : Documento
{
    public ArquivoView(string caminhoArquivoLeitura) : base(caminhoArquivoLeitura)
    { }
    
    public Guid FileId { get; set; }
   
    public MudBlazor.Color GetAssinadoColor() =>
        this.AssinadoCertificado ? MudBlazor.Color.Success : MudBlazor.Color.Error;

    public MudBlazor.Color GetOcrColor() =>
        this.Ocr ? MudBlazor.Color.Success : MudBlazor.Color.Error;

    public string GetAssinado() =>
        this.AssinadoCertificado ? "Sim" : "Não";

    public string GetOcr() =>
        this.Ocr ? "Sim" : "Não";

    public Documento GetBase() => this;
}
