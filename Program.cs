LinqQueries queries = new LinqQueries();
//Console.WriteLine("{0, -60} {1, 15} {2, 15}\n", "TITULO", "PAGINAS", "PUBLICACION");
//queries.ImprimirBooks();

//Func<Book, bool> filtro = x => x.PublishedDate.Year >= 2000 && x.PageCount > 300;
Func<Book, bool> filtro = x => x.PageCount > 250
                               && x.Title != null && x.Title.Contains("in Action");
var filtrado = queries.GetCustomFilter(filtro);

Console.WriteLine("{0, -60} {1, 15} {2, 15}\n", "TITULO", "PAGINAS", "PUBLICACION");
foreach (var i in filtrado)
    Console.WriteLine(i.ToString());
