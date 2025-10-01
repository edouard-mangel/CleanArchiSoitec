using Domain;

public class Installment
{
    public decimal Total { get; init; }
    public decimal Interest { get; init; }
    public decimal Principal { get; init; }
    public int Number { get; init; }
    public DateTime DateInvest { get; init; }

    public Installment(InstallmentSnapshot snapshot)
    {
        Total = snapshot.Total;
        Interest = snapshot.Interest;
        Principal = snapshot.Principal;
        Number = snapshot.Number;
        DateInvest = snapshot.DateInvest;
    }

    public Installment()
    {
    }
}