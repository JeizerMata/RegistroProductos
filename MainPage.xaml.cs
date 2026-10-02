using RegistroProductos.Data;
using RegistroProductos.Validation;

namespace RegistroProductos;

public partial class MainPage : ContentPage
{
    private readonly ProductoDatabase _database;
    private bool _guardando;

    public MainPage(ProductoDatabase database)
    {
        InitializeComponent();
        _database = database;
    }

    private async void OnGuardarProductoClicked(object? sender, EventArgs e)
    {
        if (_guardando) return;
        if (!ProductoValidator.TryCrear(NombreEntry.Text, DescripcionEditor.Text,
            PrecioEntry.Text, CantidadEntry.Text, out var producto, out var error))
        {
            await DisplayAlertAsync("Datos inválidos", error, "Aceptar");
            return;
        }

        _guardando = true;
        GuardarButton.IsEnabled = false;
        GuardandoIndicator.IsVisible = GuardandoIndicator.IsRunning = true;
        try
        {
            await _database.GuardarProductoAsync(producto!);
            NombreEntry.Text = DescripcionEditor.Text = PrecioEntry.Text = CantidadEntry.Text = string.Empty;
            UltimoRegistroLabel.Text = $"Último producto: {producto!.Nombre} · ID {producto.ID}";
            await DisplayAlertAsync("Registro exitoso",
                $"El producto se registró correctamente.\nID: {producto.ID}\nFecha: {producto.FechaRegistro:g}", "Aceptar");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            await DisplayAlertAsync("Error al guardar",
                "No se pudo guardar el producto en la base de datos local. Los datos se conservaron para volver a intentar.", "Aceptar");
        }
        finally
        {
            _guardando = false;
            GuardarButton.IsEnabled = true;
            GuardandoIndicator.IsVisible = GuardandoIndicator.IsRunning = false;
        }
    }
}
