namespace MultiShop.RapidApiWebUI.Models
{
    public class Rootobject
    {
        public string status { get; set; }
        public string request_id { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public Product[] products { get; set; }
    }

    public class Product
    {
        public string product_title { get; set; }
        public string product_page_url { get; set; }
        public string[] product_photos { get; set; }
        public float product_rating { get; set; }
        public Offer offer { get; set; }
    }

    public class Offer
    {
        public string price { get; set; }
        public string original_price { get; set; }
        public string offer_page_url { get; set; }
    }
}