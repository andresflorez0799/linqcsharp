LinqQueries queries = new LinqQueries();

var selectCustom = queries.GetCustomBook(select: x => new Book { Title = x.Title, PageCount = x.PageCount },
                                         where: x => x.PageCount > 400,
                                         order: x => x.PublishedDate,
                                         take: 4,
                                         skip: 0);

foreach (var i in selectCustom)
    Console.WriteLine(string.Format("{0, -60} {1, 15}\n", i.Title, i.PageCount));


Func<Book, bool> filtroAnioAndPages = x => x.PublishedDate.Year >= 2000 && x.PageCount > 300;
Func<Book, bool> filtroPagesAndTitle = x => x.PageCount > 250 && x.Title != null && x.Title.Contains("in Action");
Func<Book, bool> filtroTieneStatus = x => x.Status != string.Empty;
Func<Book, bool> filtroPublicado2005 = x => x.PublishedDate.Year == 2005;
Func<Book, bool> filtroPython = x => x.Categories != null && x.Categories.Contains("Python");
Func<Book, bool> filtroJava = x => x.Categories != null && x.Categories.Contains("Java");

var filtradoAny = queries.GetFilterAny(filtroPublicado2005);
var datosFiltrado = queries.GetCustomFilter(x => x.PageCount > 400, x => x.PublishedDate, 4, 2);


//Console.WriteLine("{0, -60} {1, 15} {2, 15}\n", "TITULO", "PAGINAS", "PUBLICACION");
//foreach (var i in datosFiltrado) Console.WriteLine(i.ToString());

//Console.WriteLine($"Algun libro publicado en 2005: {filtradoAny}");
