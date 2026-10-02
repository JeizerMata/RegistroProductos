using SQLite;

namespace RegistroProductos.Models;

[Table("Productos")]
public class Producto
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    [NotNull, MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;
    [NotNull, MaxLength(500)]
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public DateTime FechaRegistro { get; set; }
}
