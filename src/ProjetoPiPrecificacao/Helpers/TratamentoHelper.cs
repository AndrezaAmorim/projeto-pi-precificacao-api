namespace ProjetoPiPrecificacao.Helpers
{
    public static class TratamentoHelper
    {
        public static DateTime GetHoraBrasil() =>
        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, 
        TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}
