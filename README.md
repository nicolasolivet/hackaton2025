# hackaton2025

## APIhackaton - Entity Framework Core con SQLite

### Configuración

Este proyecto utiliza Entity Framework Core 8.0.11 con SQLite como base de datos.

### Paquetes Instalados

- `Microsoft.EntityFrameworkCore.Sqlite` (8.0.11)
- `Microsoft.EntityFrameworkCore.Design` (8.0.11)

### Configuración de la Base de Datos

La cadena de conexión está configurada en `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=hackaton.db"
}
```

### Uso de Entity Framework Core

#### Crear Migraciones

Para crear una nueva migración después de modificar los modelos:

```bash
cd APIhackaton
dotnet ef migrations add NombreDeLaMigracion
```

#### Aplicar Migraciones

Para aplicar las migraciones a la base de datos:

```bash
cd APIhackaton
dotnet ef database update
```

#### Revertir Migraciones

Para revertir a una migración anterior:

```bash
cd APIhackaton
dotnet ef database update NombreDeLaMigracionAnterior
```

### ApplicationDbContext

El contexto de la base de datos está definido en `APIhackaton/Data/ApplicationDbContext.cs` e incluye los siguientes DbSets:

- `Alumnos`
- `Docentes`
- `Tutores`
- `Materias`
- `Cursos`

### Ejemplo de Uso en un Controller

```csharp
[ApiController]
[Route("api/[controller]")]
public class AlumnosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AlumnosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
    {
        return await _context.Alumnos.ToListAsync();
    }
}
```

### Ejecutar el Proyecto

```bash
cd APIhackaton
dotnet run
```

La API estará disponible en `http://localhost:5294` (o el puerto configurado en launchSettings.json).