// agGridConfig.js
function getDefaultGridOptions(rowData, columnDefs) {
    return {
        columnDefs: columnDefs,
        rowData: rowData,
        pagination: true,
        paginationPageSize: 10,
        localeText: localeText,  // Puedes personalizar los textos aquí
        defaultColDef: {
            resizable: true,
            sortable: true,
            filter: true,
            tooltipComponent: 'agTooltipComponent'
        },
        rowSelection: 'multiple',
        onGridReady: function (params) {
            params.api.sizeColumnsToFit(); // Ajustar columnas al tamaño del contenedor
        }
    };
}

// Definir localeText en español
var localeText = {
    // Paginación
    page: 'Página',
    more: 'Más',
    to: 'a',
    of: 'de',
    next: 'Siguiente',
    last: 'Último',
    first: 'Primero',
    previous: 'Anterior',
    loadingOoo: 'Cargando...',

    // Filtros
    selectAll: 'Seleccionar todo',
    searchOoo: 'Buscar...',
    blanks: 'En blanco',
    filterOoo: 'Filtrar...',
    applyFilter: 'Aplicar Filtro...',
    equals: 'Igual',
    notEqual: 'Distinto',
    lessThan: 'Menor que',
    greaterThan: 'Mayor que',
    lessThanOrEqual: 'Menor o igual',
    greaterThanOrEqual: 'Mayor o igual',
    inRange: 'En rango',
    contains: 'Contiene',
    notContains: 'No contiene',
    startsWith: 'Empieza con',
    endsWith: 'Termina con',

    // Selección
    noRowsToShow: 'No hay filas para mostrar',
    loading: 'Cargando...',

    // Grupo de columnas
    group: 'Grupo',

    // Exportación
    export: 'Exportar',
    csvExport: 'Exportar CSV',
    excelExport: 'Exportar Excel',

    // Columna
    pinColumn: 'Fijar Columna',
    valueAggregation: 'Agregación de Valor',
    autosizeThiscolumn: 'Ajustar Esta Columna',
    autosizeAllColumns: 'Ajustar Todas las Columnas',
    groupBy: 'Agrupar por',
    ungroupBy: 'Desagrupar por',
    resetColumns: 'Restablecer Columnas',
    expandAll: 'Expandir todo',
    collapseAll: 'Contraer todo',
    toolPanel: 'Panel de Herramientas',
    pinLeft: 'Fijar a la Izquierda',
    pinRight: 'Fijar a la Derecha',
    noPin: 'Sin Fijar',
    sum: 'Suma',
    min: 'Mínimo',
    max: 'Máximo',
    none: 'Ninguno',
    count: 'Contar',
    avg: 'Promedio',
    filteredRows: 'Filas Filtradas',
    selectedRows: 'Filas Seleccionadas',
    totalRows: 'Filas Totales',
    totalAndFilteredRows: 'Filas Filtradas y Totales',
    copy: 'Copiar',
    copyWithHeaders: 'Copiar con Encabezados',
    ctrlC: 'Ctrl+C',
    paste: 'Pegar',
    ctrlV: 'Ctrl+V'
};

// Función para formatear el estado
function statusFormatter(params) {
    return params.value === 'Activo' ? `<span class="badge bg-success">Activo</span>` : `<span class="badge bg-danger">Inactivo</span>`;
}

// Función para formatear el teléfono
function phoneFormatter(params) {
    return `<span>${params.value.replace(/(\d{3})(\d{3})(\d{4})/, '($1) $2-$3')}</span>`;
}

// Función para renderizar las acciones de la tabla
function actionsFormatter(params) {
    return `
        <a href="/User/Details/${params.value}" class="btn btn-info me-1">Detalles</a>
        <a href="/User/CreateOrEdit/${params.value}" class="btn btn-warning me-1">Editar</a>
        <a href="/User/Delete/${params.value}" class="btn btn-danger">Eliminar</a>
    `;
}

function Alert(titulo, mensaje, típoAlerta, duracionAlerta = 5000) {
    toast({
        title: titulo,
        message: mensaje,
        type: típoAlerta,
        duration: duracionAlerta
    });
}
function toast({ title = "", message = "", type = "success", duration = 3000 }) {
    const main = document.getElementById("toast");
    if (main) {
        const toast = document.createElement("div");

        // Auto remove toast
        const autoRemoveId = setTimeout(function () {
            main.removeChild(toast);
        }, duration + 1000);

        // Remove toast when clicked
        toast.onclick = function (e) {
            if (e.target.closest(".toast__close")) {
                main.removeChild(toast);
                $("#toast").removeClass("show");
                clearTimeout(autoRemoveId);
            }
        };

        //if (title && title === "") {
        //    switch (type) {
        //        case "success":
        //            title = "\u00C9xito!"
        //            break;
        //        case "info":
        //            title = "Informaci\u00F3n!";
        //            break;
        //        case "error":
        //            title = "Error!";
        //            break;
        //        case "warning":
        //            title = "Cuidado!";
        //            break;
        //        default:
        //            break;
        //    }
        //}

        const icons = {
            success: `<svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="currentColor" class="bi bi-check-circle-fill" viewBox="0 0 30 30">
                         <g transform="scale(1.7)">
                            <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0m-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z"/>
                         </g>
                    </svg>`,

            info: `<svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="currentColor" class="bi bi-exclamation-circle-fill" viewBox="0 0 30 30">
                     <g transform="scale(1.7)">
                        <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0M8 4a.905.905 0 0 0-.9.995l.35 3.507a.552.552 0 0 0 1.1 0l.35-3.507A.905.905 0 0 0 8 4m.002 6a1 1 0 1 0 0 2 1 1 0 0 0 0-2"/>
                     </g>
                   </svg>`,

            warning: `<svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="currentColor" class="bi bi-exclamation-triangle-fill" viewBox="0 0 30 30">
                          <g transform="scale(1.7)">
                            <path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5m.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2"/>
                          </g>
                    </svg>`,

            error: `<svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="currentColor" class="bi bi-x-circle-fill" viewBox="0 0 30 30">
                      <g transform="scale(1.7)">
                        <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0M5.354 4.646a.5.5 0 1 0-.708.708L7.293 8l-2.647 2.646a.5.5 0 0 0 .708.708L8 8.707l2.646 2.647a.5.5 0 0 0 .708-.708L8.707 8l2.647-2.646a.5.5 0 0 0-.708-.708L8 7.293z"/>
                      </g>
                    </svg>`
        };
        const icon = icons[type];
        const delay = (duration / 1000).toFixed(2);

        toast.classList.add("toast", `toast--${type}`, "show");
        toast.style.animation = `slideInLeft ease .3s, fadeOut linear 1s ${delay}s forwards`;

        toast.innerHTML = `
                    <div class="toast__icon">
                        <i>${icon}</i>
                    </div>
                    <div class="toast__body">
                        <h3 class="toast__title">${title}</h3>
                        <p class="toast__msg">${message}</p>
                    </div>
                    <div class="toast__close">
                        <span class="icon delete-icon"></span>
                    </div>
                `;
        main.appendChild(toast);
    }
}

function setkeepSession(keepSession) {
    localStorage.setItem("keepSession", keepSession);
}

function getkeepSession() {
    var jsonBase64 = localStorage.getItem("keepSession");

    return json;
}
function setUserData(json) {
    var jsonBase64 = btoa(JSON.stringify(json));

    localStorage.setItem("UserData", jsonBase64);
}

function limitarLongitud(input, maxLength) {
    if (input.value.length > maxLength) {
        input.value = input.value.slice(0, maxLength);
    }
}
function getUserData() {
    var jsonBase64 = localStorage.getItem("UserData");

    if (jsonBase64 == null) {
        return null;
    }

    if (!validateJson(jsonBase64)) {
        return null;
    }

    var json = JSON.parse(atob(jsonBase64));

    if (json == null) {
        return null;
    }

    if (json.token === undefined) {
        return null;
    }

    return json;
}

function ClearForms() {
    $('input[type="text"]').val('');
    $('select').val('');
}

function validateJson(jsonBase64) {
    try {
        var json = JSON.parse(atob(jsonBase64));
        return true;
    } catch (e) {
        return false;
    }
}

function RefreshSession() {
    var UserData = getUserData();

    if (UserData != null && UserData.keepSesion) {
        $(document).find("#btn-user").removeClass("hide");
        //$(document).find("#btnLogin").addClass("hide");

        $.ajax({
            url: '/Login/ReLogin',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(UserData),
            success: function (response) {
                if (response.success) {
                    setUserData(response.userData);
                }
            },
            error: function (xhr, status, error) {
                console.error(error);
            }
        });
    }
}

function textoABase64(texto) {
    try {
        var textoCodificado = btoa(unescape(encodeURIComponent(texto)));
        return textoCodificado;
    } catch (e) {
        console.error('Error al codificar en base64:', e);
        return texto;
    }
}

function base64ATexto(base64) {
    try {
        var textoDecodificado = decodeURIComponent(escape(atob(base64)));
        return textoDecodificado;
    } catch (e) {
        console.error('Error al decodificar base64:', e);
        return base64;
    }
}


function truncateText(texto, longitudMaxima) {
    // Verifica si el texto es más largo que la longitud máxima
    if (texto.length > longitudMaxima) {
        // Corta el texto al número de caracteres deseado y agrega "..."
        return texto.substring(0, longitudMaxima) + '...';
    } else {
        // Si el texto es más corto o igual a la longitud máxima, lo devuelve tal cual
        return texto;
    }
}