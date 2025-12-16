namespace MultiShop.Cargo.DtoLayer.Dtos.CargoOperationsDtos
{
    public class CreateCargoOperationDto
    {
        public string Barcode { get; set; }
        public int Description { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
