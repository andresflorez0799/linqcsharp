public class Book
{
    public string? Title { get; set; }
    public int PageCount { get; set; }
    public string? Status { get; set; }
    public DateTime PublishedDate { get; set; }
    public string[]? Authors { get; set; }
    public string[]? Categories { get; set; }

    //... override del metodo ToString() para retornar un string normalizado propio
    public override string ToString()
    {
        return string.Format("{0, -60} {1, 15} {2, 15}\n",
            this.Title,
            this.PageCount,
            this.PublishedDate.ToString("yyyy-MM-dd"));
    }
}
