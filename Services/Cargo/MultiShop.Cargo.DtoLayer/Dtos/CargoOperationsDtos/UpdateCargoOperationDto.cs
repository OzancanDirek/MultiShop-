namespace MultiShop.Cargo.DtoLayer.Dtos.CargoOperationsDtos
{
    public class UpdateCargoOperationDto
    {
        public int CargoOperationId { get; set; }
        public string Barcode { get; set; }
        public int Description { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
