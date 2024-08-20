using System;
using System.Globalization;

namespace WEB.BioFitAdvisor.Core;

public static class Formatos
{
    public static string LimpiarRut(string rut)
    {
        string result = string.Empty;
        if (!string.IsNullOrEmpty(rut))
        {
            result = rut.Replace(".", string.Empty).Replace("-", string.Empty).Replace(",", string.Empty);
        }

        return result;
    }

    public static string FormatearRut(string rut)
    {
        int num = 0;
        if (rut.Length == 0)
        {
            return "";
        }

        rut = rut.Replace(".", "");
        rut = rut.Replace("-", "");
        string text = "-" + rut.Substring(rut.Length - 1);
        for (int num2 = rut.Length - 2; num2 >= 0; num2--)
        {
            text = rut.Substring(num2, 1) + text;
            num++;
            if (num == 3 && num2 != 0)
            {
                text = "." + text;
                num = 0;
            }
        }

        return text;
    }

    public static string FormatearMoneda(string simbolo, decimal Monto, int decimales)
    {
        string text = "";
        string text2 = "";
        if (decimales == 0)
        {
            return simbolo.Trim() + $"{Monto:###,##0.}";
        }

        for (int i = 1; i <= decimales; i++)
        {
            text2 += "0";
        }

        string format = "{0:0,0." + text2 + "}";
        return simbolo.Trim() + " " + string.Format(CultureInfo.InvariantCulture, format, Math.Round(Monto, decimales));
    }

    public static string FormatearMonedaSinSeparador(string simbolo, decimal Monto, int decimales)
    {
        string text = "";
        string text2 = "";
        if (decimales == 0)
        {
            return simbolo + string.Format(CultureInfo.InvariantCulture, "{0:00}", Monto);
        }

        for (int i = 1; i <= decimales; i++)
        {
            text2 += "0";
        }

        string format = "{0:00." + text2 + "}";
        return simbolo + string.Format(CultureInfo.InvariantCulture, format, Monto);
    }

    public static string FormatearPorcentaje(decimal Monto)
    {
        string text = "";
        decimal num = Monto;
        return num.ToString("P", CultureInfo.InvariantCulture);
    }

    public static string FormatearFechaAnioMesDia(string fecha)
    {
        try
        {
            string[] fechaSeparada = fecha.Split('-');
            return fechaSeparada[0] + "-" + fechaSeparada[1] + "-" + fechaSeparada[2];
        }
        catch (Exception)
        {

            throw;
        }
    }

    public static string FormatearFecha(DateTime fecha)
    {
        // Formatear la fecha en el formato deseado
        string fechaFormateada = fecha.ToString("yyyy-MM-dd");

        return fechaFormateada;
    }

    public static string FormatearMes(int Mes)
    {
        DateTimeFormatInfo dateTimeFormat = new CultureInfo("es-ES", useUserOverride: false).DateTimeFormat;
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dateTimeFormat.GetMonthName(Mes));
    }

    public static string FormatearTelefonos(string Telefono)
    {
        return "";
    } 
}