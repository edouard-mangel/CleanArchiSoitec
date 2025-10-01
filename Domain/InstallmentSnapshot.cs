using System.Drawing;

namespace Domain;

public class InstallmentSnapshot
{
    public decimal Total { get; set; }
    public decimal Interest { get; set; }
    public decimal Principal { get; set; }
    public int Number { get; set; }
    public DateTime DateInvest { get; set; }

    public InstallmentSnapshot(Installment installment)
    {
        Total = installment.Total;
        Interest = installment.Interest;
        Principal = installment.Principal;
        Number = installment.Number;
        DateInvest = installment.DateInvest;
    }

    public InstallmentSnapshot(int number, decimal total, decimal interest, decimal principal,  DateTime dateInvest)
    {
        Total = total;
        Interest = interest;
        Principal = principal;
        Number = number;
        DateInvest = dateInvest;
    }
}