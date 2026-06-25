namespace SGIP.Domain.Validation
{
    public static class LoanValidatorExtensions
    {
        public static void EnsureValidLimits( decimal amount, int term)
        {
            if (amount < 500m || amount > 50_000m)
                throw new InvalidOperationException("El monto debe estar entre 500 y 50000.");

            if (term < 6 || term > 60)
                throw new InvalidOperationException("El plazo debe ser entre 6 y 60 meses");
        }
    }
}
