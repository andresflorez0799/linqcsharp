using System.Text.Json;
public class LinqQueries
{
    private readonly List<Book> _librosLt;
    private const string FILE_PATH = "books.json";

    //... Constructor, se inicializa el contenido del listado de Books con validaciones
    public LinqQueries()
    {
        _librosLt = new List<Book>();
        if (File.Exists(FILE_PATH))
            using (StreamReader reader = new StreamReader(FILE_PATH))
            {
                string json = reader.ReadToEnd();
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                List<Book>? books = JsonSerializer.Deserialize<List<Book>>(json, options);
                if (books != null && books.Any())
                    this._librosLt = books;
            }
    }

    //... Obtiene todos los libros
    public IEnumerable<Book> GetAllBooks() => this._librosLt;

    //... Muestra por consola el titulo, paginas y fecha de publicacion
    public void ImprimirBooks() => this._librosLt.ForEach(x => Console.WriteLine(x.ToString()));

    //... Obtiene un listado de libros filtrado por una expresion lambda "x => x.Field"
    public IEnumerable<Book> GetCustomFilter(Func<Book, bool> filtro)
        => this._librosLt.Where(filtro).OrderBy(x => x.PublishedDate);

}
