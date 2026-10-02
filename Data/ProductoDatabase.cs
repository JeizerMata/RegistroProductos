using RegistroProductos.Models;
using SQLite;

namespace RegistroProductos.Data;

public class ProductoDatabase
{
    private readonly SQLiteAsyncConnection _connection;
    private readonly SemaphoreSlim _initializationLock = new(1, 1);
    private bool _initialized;

    public ProductoDatabase(string databasePath)
    {
        _connection = new SQLiteAsyncConnection(databasePath,
            SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
    }

    public async Task InicializarAsync()
    {
        await _initializationLock.WaitAsync();
        try
        {
            if (_initialized) return;
            await _connection.CreateTableAsync<Producto>();
            _initialized = true;
        }
        finally { _initializationLock.Release(); }
    }

    public async Task GuardarProductoAsync(Producto producto)
    {
        ArgumentNullException.ThrowIfNull(producto);
        if (producto.ID != 0 || string.IsNullOrWhiteSpace(producto.Nombre)
            || string.IsNullOrWhiteSpace(producto.Descripcion)
            || producto.Nombre.Length > 100 || producto.Descripcion.Length > 500
            || producto.Precio <= 0 || producto.Cantidad < 0)
            throw new ArgumentException("El producto debe ser nuevo y contener datos válidos.");

        await InicializarAsync();
        producto.FechaRegistro = DateTime.Now;
        var filas = await _connection.InsertAsync(producto);
        if (filas != 1) throw new InvalidOperationException("No se insertó el producto.");
    }

    public async Task<List<Producto>> ObtenerProductosAsync()
    {
        await InicializarAsync();
        return await _connection.Table<Producto>().OrderByDescending(p => p.ID).ToListAsync();
    }
}
