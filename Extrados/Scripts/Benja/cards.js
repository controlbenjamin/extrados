$.get("/Producto/ListarCards/", function (data) {

    var contenido = "";


    //inicio grupo de cards
    contenido += "<div class='card-columns' style='width:90%;margin:auto;''>";



    for (var i = 0; i < data.length; i++) {

        contenido += " <br />";
        //card individual
        contenido += "<div class='card' style='max-width:30rem;margin:auto;'>";
        contenido += "<img class='card-img-top' src='" + data[i].UBICACION + "' width='200' height='200' alt='A 200x200 image'>";
        contenido += "<div class='card-body text-center' >";
        contenido += "<h5 class='card-title' >" + data[i].NOMBRE + " - " + data[i].MARCA + "</h5>";
        contenido += "<p class='card-text' >" + data[i].DESCRIPCION + "</p>";
        contenido += "<h4 class='card-title'>" + parsearMoneda(data[i].PRECIO_UNITARIO) + "</h4>";
        contenido += "<a href='#' class='btn btn-outline-primary btn-block'>Agregar</a>";
        contenido += "</div>";
        contenido += "</div>";
        contenido += " <br />";

    }



    //fin grupo de cards

    contenido += "</div>";




    document.getElementById("cards-productos").innerHTML = contenido;


});

function parsearMoneda(decimal) {

    return new Intl.NumberFormat("es-AR", { style: "currency", currency: "ARS" }).format(decimal);
};
