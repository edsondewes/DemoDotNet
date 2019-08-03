namespace SwaggerDocs
{
    public class SwaggerConfig
    {
        public SwaggerEndpoint[] Endpoints { get; set; }
    }

    public class SwaggerEndpoint
    {
        public string Nome { get; set; }
        public string Url { get; set; }
    }
}
