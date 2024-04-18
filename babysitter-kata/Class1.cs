namespace babysitter_kata;

public class Family
{
    private int[] rates;

    public Family(int[] rates)
    {
        if (rates.Length != 24)
            throw new ArgumentException("Rates must be specified for exactly 24 hours");
        this.rates = rates;
    }

    public int CalculatePay(int startHour, int endHour)
    {
        if (startHour > 3 && startHour < 17)
            throw new ArgumentException("Start Hour must be 5:00 PM or after");
        if (endHour < 18 && endHour > 4)
            throw new ArgumentException("End Hour must be 4:00 AM or earlier");
        if (startHour > endHour && startHour - endHour < 13)
            throw new ArgumentException("Start Time cannot be after End Time");

        int totalPay = 0;
        for (int i = startHour; i < endHour || i - endHour > 12; i++)
        {
            if (i == 24)
                i = 0;
            totalPay += rates[i];
        }
        return totalPay;
    }
}