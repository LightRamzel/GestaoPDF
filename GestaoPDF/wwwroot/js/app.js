function GerarURL(dotNetHelper, indexFile) {

    var input = document.getElementById("fileInput");

    var tmppath = URL.createObjectURL(input.files[indexFile]);

    return dotNetHelper.invokeMethodAsync('ExibirPDF', tmppath);
}

function ViewPdfFile(indexFile) {

    var input = document.getElementById("fileInput");

    var tmppath = URL.createObjectURL(input.files[indexFile]);

    var preview = document.getElementById("embed-pdf");

    preview.src = tmppath;
}
