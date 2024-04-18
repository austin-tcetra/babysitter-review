using babysitter_kata;

namespace babysitter_kata_tests;

public class Tests
{
    private readonly Family famA = new Family(new int[] { 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 15, 15, 15, 15, 15, 20 });
    private readonly Family famB = new Family(new int[] { 16, 16, 16, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 12, 12, 12, 12, 8, 8 });
    private readonly Family famC = new Family(new int[] { 15, 15, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 21, 21, 21, 21, 15, 15, 15 });

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TheBabysitterStartsNoEarlierThanFivePM()
    {
        var bs = new Babysitter();
        var ex = Assert.Throws<ArgumentException>(() => bs.CalculatePay(15, 19, famA));
        Assert.That(ex.Message, Is.EqualTo("Start Hour must be 5:00 PM or after"));
    }

    [Test]
    public void TheBabysitterEndsNoLaterThanFourAM()
    {
        var bs = new Babysitter();
        var ex = Assert.Throws<ArgumentException>(() => bs.CalculatePay(20, 5, famA));
        Assert.That(ex.Message, Is.EqualTo("End Hour must be 4:00 AM or earlier"));
    }

    [Test]
    public void TheBabysitterCanStartInPMAndEndInAM()
    {
        var bs = new Babysitter();
        Assert.DoesNotThrow(() => bs.CalculatePay(17, 4, famA));
    }

    [Test]
    public void StartTimeCannotBeAfterEndTime()
    {
        var bs = new Babysitter();
        var ex = Assert.Throws<ArgumentException>(() => bs.CalculatePay(20, 18, famA));
        Assert.That(ex.Message, Is.EqualTo("Start Time cannot be after End Time"));
    }

    [Test]
    public void RatesMustBeSpecifiedForExactly24Hours()
    {
        var ex = Assert.Throws<ArgumentException>(() => new Family(new int[] { 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 15, 15, 15, 15, 20 }));
        Assert.That(ex.Message, Is.EqualTo("Rates must be specified for exactly 24 hours"));
    }

    [Test]
    public void FamilyAPays190ForAFullNightShift()
    {
        var bs = new Babysitter();
        var pay = bs.CalculatePay(17, 4, famA);
        Assert.That(pay, Is.EqualTo(190));
    }

    [Test]
    public void FamilyBPays140ForAFullNightShift()
    {
        var bs = new Babysitter();
        var pay = bs.CalculatePay(17, 4, famB);
        Assert.That(pay, Is.EqualTo(140));
    }

    [Test]
    public void FamilyCPays189ForAFullNightShift()
    {
        var bs = new Babysitter();
        var pay = bs.CalculatePay(17, 4, famC);
        Assert.That(pay, Is.EqualTo(189));
    }
}