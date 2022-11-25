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
    public bool GetFilterAny(Func<Book, bool> filtro) => this._librosLt.Any(filtro);

    //... Obtiene un listado de libros filtrado por una expresion lambda "x => x.Field"
    public IEnumerable<Book> GetCustomFilter(Func<Book, bool> filtro) => this._librosLt.Where(filtro);

    public IEnumerable<Book> GetCustomFilter(Func<Book, bool> filtro, Func<Book, object> orden) => this._librosLt.Where(filtro).OrderBy(orden);

    public IEnumerable<Book> GetCustomFilter(
        Func<Book, bool> filtro,
        Func<Book, object> orden,
        int take,
        int skip)
    {
        return this._librosLt
            .Where(filtro)
            .OrderBy(orden)
            .Skip(skip);
    }

    public IEnumerable<dynamic> GetCustomBook(
        Func<Book, dynamic> select,
        Func<Book, bool> where,
        Func<Book, object> order,
        int take,
        int skip = 0)
    {
        return this._librosLt
            .Where(where)
            .OrderBy(order)
            .Take(take)
            .Skip(skip)
            .Select(select);
    }

    //public T GetMinusValue<T>(Func<T, object> min) => this._librosLt.Min(min);

    public int GetSumaPaginas(Func<Book, bool> where)
    {
        int total = this._librosLt
            .Where(where)
            .Sum(x => x.PageCount);
        return total;
    }

    public string TitulosSeparadosGuion(Func<Book, bool> where)
        => string.Join(" - ", this._librosLt.Where(where).Select(x => x.Title));

    public string TitulosSeparadosGuionV2(Func<Book, bool> where)
    {
        return this._librosLt.Where(where).Aggregate("",
            (acum, next) => acum += (!string.IsNullOrEmpty(acum) ? $" - {next.Title}" : next.Title));
    }

    public dynamic AgruparPorAnio(Func<Book, bool> where)
    {
        return this._librosLt.Where(where)
            .GroupBy(x => x.PublishedDate);
    }

    public ILookup<char, Book> DiccionarioPorLetra()
    {
        return this._librosLt.ToLookup(p => p.Title[0], p => p);
    }



}
