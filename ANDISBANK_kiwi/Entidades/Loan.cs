namespace ANDISBANCKAPI.Entidades
{
// Estados
// 0 = Pendiente
// 1 = Pagado
// 2 = Vencido
// 3 = Cancelado

    public class Loan
    {
        public int id { get; set; }
        public string Descripcion { get; set; }
        public string Name { get; set; }
        public LoanType Tipo { get; set; }
        public double Monto { get; set; }
        public double Tasa { get; set; }
        public DateTime Plazo { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }

        public int Estado { get; set; }

        public Loan()
        {
        }
    }
}
