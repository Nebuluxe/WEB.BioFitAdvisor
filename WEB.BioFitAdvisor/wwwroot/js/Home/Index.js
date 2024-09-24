
document.addEventListener('DOMContentLoaded', function () {
    // Datos de ejemplo para la tabla de suscripciones
    var rowData = [
        { userName: 'Juan Pérez', subscriptionType: 'Premium', startDate: '2023-01-10', endDate: '2024-01-10', status: 'Activo' },
        { userName: 'Ana López', subscriptionType: 'Standard', startDate: '2023-05-15', endDate: '2024-05-15', status: 'Activo' },
        { userName: 'Carlos Rodríguez', subscriptionType: 'Basic', startDate: '2022-10-01', endDate: '2023-10-01', status: 'Inactivo' },
        // Añade más datos según sea necesario
    ];

    // Definición de las columnas
    var columnDefs = [
        { headerName: 'Nombre del Usuario', field: 'userName', sortable: true, filter: true, checkboxSelection: true },
        { headerName: 'Tipo de Suscripción', field: 'subscriptionType', sortable: true, filter: true },
        { headerName: 'Fecha de Inicio', field: 'startDate', sortable: true, filter: 'agDateColumnFilter' },
        { headerName: 'Fecha de Expiración', field: 'endDate', sortable: true, filter: 'agDateColumnFilter' },
        { headerName: 'Estado', field: 'status', sortable: true, filter: true, cellRenderer: statusFormatter }
    ];

    // Configuración de AG-Grid
    var gridOptions = getDefaultGridOptions(rowData, columnDefs);

    var gridDiv = document.querySelector('#subscriptionTable');
    new agGrid.Grid(gridDiv, gridOptions);

    // Formateador de estado para aplicar estilo a los estados
    function statusFormatter(params) {
        return params.value === 'Activo' ? `<span class="badge bg-success">Activo</span>` : `<span class="badge bg-danger">Inactivo</span>`;
    }
});
$(function () {

    // Inicializa los gráficos de eCharts
    var myChart = echarts.init(document.getElementById('chartContainer'));
    var myChart2 = echarts.init(document.getElementById('chartContainer2'));
    var myChart3 = echarts.init(document.getElementById('chartContainer3'));
    var heatmapChart = echarts.init(document.getElementById('chartContainer4'));
    var worldMapChart = echarts.init(document.getElementById('worldMapChart'));

    // Paleta de colores morada/púrpura
    const purplePalette = ['#7B1FA2', '#8E24AA', '#9C27B0', '#AB47BC', '#BA68C8', '#CE93D8', '#E1BEE7', '#F3E5F5'];

    var option = {
        color: purplePalette,
        title: {
            text: 'Número de Suscriptores Activos',
            textStyle: { color: '#FFFFFF' } // Título en blanco
        },
        tooltip: { trigger: 'axis' },
        xAxis: {
            type: 'category',
            data: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
        },
        yAxis: { type: 'value' },
        series: [{
            name: 'Suscriptores Activos',
            type: 'line',
            smooth: true,
            data: [820, 932, 901, 934, 1290, 1330, 1320, 1400, 1500, 1600, 1700, 1800]
        }]
    };

    var option2 = {
        color: purplePalette,
        title: {
            text: 'Nuevas Suscripciones por Mes',
            textStyle: { color: '#FFFFFF' } // Título en blanco
        },
        tooltip: { trigger: 'axis' },
        xAxis: {
            type: 'category',
            data: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
        },
        yAxis: { type: 'value' },
        series: [{
            name: 'Nuevas Suscripciones',
            type: 'bar',
            data: [120, 200, 150, 80, 70, 110, 130, 180, 210, 230, 190, 240]
        }]
    };

    var option3 = {
        color: purplePalette,
        title: {
            text: 'Tasa de Cancelación de Suscripciones',
            textStyle: { color: '#FFFFFF' } // Título en blanco
        },
        tooltip: { trigger: 'axis' },
        xAxis: {
            type: 'category',
            data: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
        },
        yAxis: { type: 'value' },
        series: [{
            name: 'Cancelaciones',
            type: 'line',
            smooth: true,
            data: [10, 15, 20, 15, 12, 8, 6, 7, 8, 10, 5, 9]
        }]
    };

    var heatmapOption = {
        color: purplePalette, // Aplicar paleta de colores
        title: {
            text: 'Actividad de Usuario por Día',
            textStyle: { color: '#FFFFFF' } // Título en blanco
        },
        tooltip: { trigger: 'axis' },
        xAxis: {
            type: 'category',
            data: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday']
        },
        yAxis: {
            type: 'value',
            axisLabel: {
                color: '#FFFFFF' // Ejes en blanco
            },
            splitLine: {
                lineStyle: {
                    color: 'rgba(255, 255, 255, 0.2)' // Líneas de división tenues
                }
            }
        },
        series: [{
            name: 'Número de Actividades',
            type: 'bar',
            data: [50, 70, 90, 40, 80, 100, 60], // Datos de ejemplo; reemplázalos con los tuyos
            barWidth: '50%', // Ancho de las barras
        }]
    };

    function generateData() {
        var data = [];
        for (var i = 0; i < 7; i++) {
            for (var j = 0; j < 4; j++) {
                data.push([i, j, Math.floor(Math.random() * 100)]);
            }
        }
        return data;
    }

    myChart.setOption(option);
    myChart2.setOption(option2);
    myChart3.setOption(option3);
    heatmapChart.setOption(heatmapOption);

    // Fetch data function for dynamic updates
    function fetchDataAndUpdateCharts(startDate, endDate) {
        $.ajax({
            url: '/api/subscriptions/stats',
            method: 'GET',
            data: { startDate: startDate, endDate: endDate },
            success: function (data) {
                myChart.setOption({ series: [{ data: data.activeSubscribers }] });
                myChart2.setOption({ series: [{ data: data.newSubscriptions }] });
                myChart3.setOption({ series: [{ data: data.cancellations }] });
                heatmapChart.setOption({ series: [{ data: generateData() }] });

                var table = $('#subscriptionTable').DataTable();
                table.clear().rows.add(data.subscriptionDetails).draw();

                checkThresholds(data);
            }
        });
    }

    function checkThresholds(data) {
        if (data.cancellations.some(cancellation => cancellation > 20)) {
            alert('¡Alerta! La tasa de cancelación ha superado el 20% en uno o más meses.');
        }
    }

    $('#filterButton').click(function () {
        var startDate = $('#startDate').val();
        var endDate = $('#endDate').val();
        fetchDataAndUpdateCharts(startDate, endDate);
    });

    fetchDataAndUpdateCharts();
    setInterval(fetchDataAndUpdateCharts, 30000);

    worldMapChart.showLoading();

    $.get('/lib/echarts/world.json', function (worldJson) {
        worldMapChart.hideLoading();
        echarts.registerMap('World', worldJson);

        var data = [
            { name: 'United States', value: 331002651 },
            { name: 'China', value: 1439323776 },
            { name: 'India', value: 1380004385 },
            { name: 'Indonesia', value: 273523615 },
            { name: 'Pakistan', value: 220892340 },
            { name: 'Brazil', value: 212559417 },
            { name: 'Nigeria', value: 206139589 },
            { name: 'Bangladesh', value: 164689383 },
            { name: 'Russia', value: 145934462 },
            { name: 'Mexico', value: 128932753 }
        ];

        const worldMapOption = {
            color: purplePalette,
            title: {
                text: 'Usuarios a Nivel Global',
                textStyle: { color: '#FFFFFF' } // Título en blanco
            },
            visualMap: {
                left: 'right',
                min: 0,
                max: 1500000000,
                inRange: {
                    color: ['#E1BEE7', '#BA68C8', '#8E24AA', '#7B1FA2']
                },
                text: ['Alto', 'Bajo'],
                calculable: true
            },
            series: [{
                name: 'Población',
                type: 'map',
                roam: true,
                map: 'World',
                data: data
            }]
        };

        worldMapChart.setOption(worldMapOption);
    });
});
