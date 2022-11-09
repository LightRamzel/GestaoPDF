function GerarURL(dotNetHelper, indexFile)
{
    var input = document.getElementById("fileInput");
    var tmppath = URL.createObjectURL(input.files[indexFile]);

    dotNetHelper.invokeMethodAsync("ExibirPDF", tmppath);
}

function ExibirPDF(url)
{
    var preview = document.getElementById("embed-pdf");
    preview.src = url;
}

function RemoverFile(indexFile) {

    var input = document.getElementById("fileInput");
    var item = input.files[indexFile];
    


}
