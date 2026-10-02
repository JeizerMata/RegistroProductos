using System.Globalization;
using RegistroProductos.Models;

namespace RegistroProductos.Validation;

public static class ProductoValidator
{
    public static bool TryCrear(string? nombre, string? descripcion, string? precioTexto,
        string? cantidadTexto, out Producto? producto, out string error)
    {
        producto = null;
        error = string.Empty;
        nombre = nombre?.Trim() ?? string.Empty;
        descripcion = descripcion?.Trim() ?? string.Empty;
        if (nombre.Length == 0 || nombre.Length > 100)
            error = "Ingresa un nombre de producto de 1 a 100 caracteres.";
        else if (descripcion.Length == 0 || descripcion.Length > 500)
            error = "Ingresa una descripción de 1 a 500 caracteres.";

        var normalizado = (precioTexto ?? string.Empty).Trim().Replace(',', '.');
        if (!decimal.TryParse(normalizado, NumberStyles.AllowDecimalPoint,
            CultureInfo.InvariantCulture, out var precio) || precio <= 0
            || decimal.Round(precio, 2) != precio)
        {
            if (error.Length == 0) error = "Ingresa un precio mayor que cero con un máximo de dos decimales, sin separadores de miles.";
        }
        if (!int.TryParse(cantidadTexto?.Trim(), NumberStyles.None,
            CultureInfo.InvariantCulture, out var cantidad) || cantidad < 0)
        {
            if (error.Length == 0) error = "Ingresa una cantidad entera mayor o igual que cero.";
        }
        if (error.Length > 0) return false;
        producto = new Producto { Nombre = nombre, Descripcion = descripcion, Precio = precio, Cantidad = cantidad };
        return true;
    }
}
