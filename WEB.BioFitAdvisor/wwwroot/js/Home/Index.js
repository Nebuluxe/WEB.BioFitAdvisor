var chart1 = echarts.init(document.getElementById('chart1'));
var chart2 = echarts.init(document.getElementById('chart2'));
var chart3 = echarts.init(document.getElementById('chart3'));
var chart4 = echarts.init(document.getElementById('chart4'));
var chart5 = echarts.init(document.getElementById('chart5'));

var option1 = {
    title: {
        text: 'Monthly Workout Frequency',
        left: 'center',
        textStyle: {
            color: '#fff'
        }
    },
    xAxis: {
        type: 'category',
        data: ['Week 1', 'Week 2', 'Week 3', 'Week 4'],
        axisLine: {
            lineStyle: {
                color: '#fff'
            }
        }
    },
    yAxis: {
        type: 'value',
        axisLine: {
            lineStyle: {
                color: '#fff'
            }
        }
    },
    series: [{
        data: [20, 25, 30, 35],
        type: 'bar',
        color: '#6b73ff'
    }]
};

var option2 = {
    title: {
        text: 'Calories Burned Per Week',
        left: 'center',
        textStyle: {
            color: '#fff'
        }
    },
    xAxis: {
        type: 'category',
        data: ['Week 1', 'Week 2', 'Week 3', 'Week 4'],
        axisLine: {
            lineStyle: {
                color: '#fff'
            }
        }
    },
    yAxis: {
        type: 'value',
        axisLine: {
            lineStyle: {
                color: '#fff'
            }
        }
    },
    series: [{
        data: [800, 900, 950, 850],
        type: 'line',
        smooth: true,
        color: '#00e676'
    }]
};

var option3 = {
    title: {
        text: 'Body Weight Progress',
        left: 'center',
        textStyle: {
            color: '#fff'
        }
    },
    xAxis: {
        type: 'category',
        data: ['Week 1', 'Week 2', 'Week 3', 'Week 4'],
        axisLine: {
            lineStyle: {
                color: '#fff'
            }
        }
    },
    yAxis: {
        type: 'value',
        axisLine: {
            lineStyle: {
                color: '#fff'
            }
        }
    },
    series: [{
        data: [75, 74.5, 74, 73.5],
        type: 'line',
        smooth: true,
        color: '#ff6b6b'
    }]
};

var option4 = {
    title: {
        text: 'Macronutrient Intake',
        left: 'center',
        textStyle: {
            color: '#fff'
        }
    },
    tooltip: {
        trigger: 'item'
    },
    legend: {
        top: 'bottom',
        textStyle: {
            color: '#fff'
        }
    },
    series: [
        {
            name: 'Macronutrients',
            type: 'pie',
            radius: '50%',
            data: [
                { value: 1048, name: 'Carbohydrates' },
                { value: 735, name: 'Proteins' },
                { value: 580, name: 'Fats' }
            ],
            emphasis: {
                itemStyle: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
            }
        }
    ]
};

var option5 = {
    title: {
        text: 'Hydration Level',
        left: 'center',
        textStyle: {
            color: '#fff'
        }
    },
    xAxis: {
        type: 'category',
        data: ['Week 1', 'Week 2', 'Week 3', 'Week 4'],
        axisLine: {
            lineStyle: {
                color: '#fff'
            }
        }
    },
    yAxis: {
        type: 'value',
        axisLine: {
            lineStyle: {
                color: '#fff'
            }
        }
    },
    series: [{
        data: [2.5, 3.0, 3.2, 2.8],
        type: 'bar',
        color: '#00bcd4'
    }]
};

chart1.setOption(option1);
chart2.setOption(option2);
chart3.setOption(option3);
chart4.setOption(option4);
chart5.setOption(option5);