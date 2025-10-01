namespace Infrastructure;

public class InstallmentEntity
{
    public int Number { get; set; }
    public decimal Principal { get; set; }
    public decimal Interest { get; set; }
    public decimal Total { get; set; }
    public DateTime Date { get; set; }

    public InstallmentEntity(Installment installment)
    {
        Number = installment.Number;
        Principal = installment.Principal;
        Interest = installment.Interest;
        Total = installment.Total;
        Date = installment.DateInvest;
    }
}