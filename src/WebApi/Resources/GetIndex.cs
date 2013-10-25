using Simple.Web.Behaviors;

namespace Simple.Web.Windsor.Owin.Resources
{
    [UriTemplate("/resources/home")]
    public class GetIndex : IGet, IOutput<RawHtml>
    {
        readonly DataService _dataService;

        public GetIndex(DataService dataService)
        {
            _dataService = dataService;
        }
        public Status Get()
        {
            this.Output = "<p>Hello!</p>";

            return 200;
        }

        public RawHtml Output { get; set; }
    }

    public class DataService
    {
    }


    /// <summary>
    /// link to orders /customers/{id}/orders
    /// 
    /// { 
    ///     customer 
    ///     {
    ///         links: {
    ///             rel: 'orders', href=/customers/123/orders
    ///         },
    ///         actiib
    ///     }
    /// 
    /// }
    /// 
    /// 
    /// </summary>
 
    public class Customer
    {
        public int Id { get; set; }

    }

}